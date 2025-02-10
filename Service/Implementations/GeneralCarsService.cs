using AutoRepairMainCore.DTO;
using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Exceptions.GeneralCarsExceptions;
using AutoRepairMainCore.Infrastructure;
using AutoRepairMainCore.Service;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


public class GeneralCarsService : IGeneralCarsService
{
    private readonly MySqlContext _context;
    private readonly HttpClient _httpClient;

    public GeneralCarsService(MySqlContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient("OpenAIMicroServiceClient");
    }

    public List<Brand> GetBrands()
    {
        return _context.brands.ToList();
    }

    public List<Model> GetModels()
    {
        return _context.models.ToList();
    }

    public List<Engine> GetEngines()
    {
        return _context.engines.ToList();
    }

    public CarResults<Brand> AddBrand(string brand)
    {
        string formattedBrand = brand.Trim();
        var existingBrand = _context.brands.FirstOrDefault(b => b.BrandName == formattedBrand);
        if (existingBrand != null)
        {
            return new CarResults<Brand>(existingBrand, false);
        }

        var newBrand = new Brand { BrandName = formattedBrand };
        _context.brands.Add(newBrand);
        _context.SaveChanges();

        return new CarResults<Brand>(newBrand, true);
    }

    public CarResults<Model> AddModel(string model)
    {
        string formattedModel = model.Trim();
        var existingModel = _context.models.FirstOrDefault(m => m.ModelName == formattedModel);
        if (existingModel != null)
        {
            return new CarResults<Model>(existingModel, false);
        }

        var newModel = new Model { ModelName = formattedModel };
        _context.models.Add(newModel);
        _context.SaveChanges();

        return new CarResults<Model>(newModel, true);
    }

    public CarResults<Engine> AddEngine(string engineDescription)
    {
        string formattedEngine = engineDescription.Trim();
        var existingEngine = _context.engines.FirstOrDefault(e => e.EngineDescription == formattedEngine);
        if (existingEngine != null)
        {
            return new CarResults<Engine>(existingEngine, false);
        }

        var newEngine = new Engine { EngineDescription = formattedEngine };
        _context.engines.Add(newEngine);
        _context.SaveChanges();

        return new CarResults<Engine>(newEngine, true);
    }

    public Car AddCar(int brandId, int modelId, int engineId)
    {
        var newCar = new Car
        {
            BrandId = brandId,
            ModelId = modelId,
            EngineId = engineId
        };

        _context.cars.Add(newCar);
        _context.SaveChanges();
        return newCar;
    }

    public Car AddCar(CarDto newCar)
    {
        IsValidCarDTO(newCar);
        FormatCar(newCar);

        var existingCar = CheckExistingCar(newCar.Brand, newCar.Model, newCar.Engine);
        if (existingCar != null)
        {
            throw new CarAlreadyExistException($"The {newCar.Brand} {newCar.Model} {newCar.Engine}" +
                $" already exists");
        }

        var brandResult = AddBrand(newCar.Brand);
        var modelResult = AddModel(newCar.Model);
        var engineResult = AddEngine(newCar.Engine);

        var newEntityCar = new Car
        {
            BrandId = brandResult.Entity.Id,
            ModelId = modelResult.Entity.Id,
            EngineId = engineResult.Entity.Id
        };

        _context.cars.Add(newEntityCar);
        _context.SaveChanges();
        return newEntityCar;
    }

    private bool IsValidCarDTO(CarDto car)
    {
        if (car == null ||
            string.IsNullOrEmpty(car.Brand) ||
            string.IsNullOrEmpty(car.Model) ||
            string.IsNullOrEmpty(car.Engine))
        {
            throw new InvalidCarDataException("Fields must be not empty");
        }

        return true;
    }

    private Car CheckExistingCar(string brand, string model, string engine)
    {
        return _context.cars
            .Include(c => c.Brand)
            .Include(c => c.Model)
            .Include(c => c.Engine)
            .FirstOrDefault(c => c.Brand.BrandName == brand &&
                                 c.Model.ModelName == model &&
                                 c.Engine.EngineDescription == engine);
    }

    private CarDto FormatCar(CarDto car)
    {
        car.Brand.Trim();
        car.Model.Trim();
        car.Engine.Trim();

        return car;
    }

    public Brand GetBrand(string name)
    {
        throw new NotImplementedException();
    }

    public Model GetModel(string name)
    {
        throw new NotImplementedException();
    }

    public Engine GetEngine(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<CarDto> OpenAICarValidation(CarDto car)
    {
        IsValidCarDTO(car);
        try
        {
            string carJson = SerializeCarRequest(car);

            var content = new StringContent(carJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/v1/car/validate", content);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new OpenAIMicroServiceException(
                    $"Error from microservice: {response.StatusCode} - {errorContent}");
            }

            string responseJson = await response.Content.ReadAsStringAsync();
            return DeserializeCarResponse(responseJson);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string SerializeCarRequest(CarDto car)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        };

        return JsonSerializer.Serialize(car, options);
    }

    private CarDto DeserializeCarResponse(string responseJson)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<CarDto>(responseJson, options);
    }

    public CreatingECUDto AddBlock(List<string> newNames)
    {
        if (newNames == null || !newNames.Any())
        {
            throw new InvalidParameterException("No any ECUs to add.");
        }
        CreatingECUDto ecu = new CreatingECUDto()
        {
            ECUs = new List<Block>(),
            Logger = "Info about saving ECU\n"
        };
        
        foreach(string block in newNames)
        {
            Block? existingBlock = _context.blocks.FirstOrDefault(b => b.BlockName == block);
            if (existingBlock != null)
            {
                ecu.Logger += $"{block} already exists.\n";
                continue;
            }

            Block newBlock = new Block()
            {
                BlockName = block
            };
            _context.blocks.Add(newBlock);
            ecu.ECUs.Add(newBlock);
            ecu.Logger += $"{block} added.\n";
        }
        _context.SaveChanges();
        return ecu;
    }

    public ErrorCode? FindErrorCodeByName(string code)
    {
        return _context.errorCodes.FirstOrDefault(e => e.Code == code);
    }

    public ErrorCode? FindErrorCodeById(int id)
    {
        return _context.errorCodes.FirstOrDefault(e => e.Id == id);
    }

    public ErrorCode UpdateErrorCodeDescription(int errorCodeId, string description)
    {
        ErrorCode? errorCode = FindErrorCodeById(errorCodeId);
        if(errorCode == null)
        {
            throw new InvalidParameterException("Can not find errorCode");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new InvalidParameterException("Empty description");
        }
        ErrorCode? existingErrorCode = FindErrorCodeById(errorCodeId);
        if(existingErrorCode == null)
        {
            throw new InvalidParameterException("Cannot find errorCode in database");
        }
        existingErrorCode.Description = description;
        _context.errorCodes.Update(existingErrorCode);
        _context.SaveChanges();
        return existingErrorCode;
    }

    public ErrorCode AddErrorCode(int? blockId, string code, string? description)
    {
        if (string.IsNullOrWhiteSpace(code) || !blockId.HasValue)
        {
            throw new InvalidParameterException("Empty code or block fields");
        }
        ErrorCode? existingErrorCode = FindErrorCodeByName(code);

        if(existingErrorCode != null) 
        {
            throw new InvalidParameterException("Error code already exists");
        }

        ErrorCode errorCode = new ErrorCode()
        {
            BlockId = (int)blockId,
            Code = code,
            Description = description
        };

        _context.errorCodes.Add(errorCode);
        _context.SaveChanges();
        return errorCode;
    }
}
