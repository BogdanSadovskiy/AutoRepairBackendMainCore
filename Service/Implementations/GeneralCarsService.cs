using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Infrastructure;
using AutoRepairMainCore.Service;
using Microsoft.EntityFrameworkCore;
using AutoRepairMainCore.Exceptions.GeneralCarsExceptions;

public class GeneralCarsService : IGeneralCarsService
{
    private readonly MySqlContext _context;

    public GeneralCarsService(MySqlContext context)
    {
        _context = context;
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
        string formattedBrand = FormatName(brand);
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
        string formattedModel = FormatName(model);
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
   
        string formattedBrand = FormatName(newCar.Brand);
        string formattedModel = FormatName(newCar.Model);
        string formattedEngine = newCar.Engine.Trim();

        var existingCar = CheckExistingCar(formattedBrand, formattedModel, formattedEngine);
        if (existingCar != null)
        {
            throw new CarAlreadyExistException($"The {formattedBrand} {formattedModel} {formattedEngine}" +
                $" already exists");
        }

        var brandResult = AddBrand(formattedBrand);
        var modelResult = AddModel(formattedModel);
        var engineResult = AddEngine(formattedEngine);

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

    private string FormatName(string name)
    {
        name = name.Trim();
        return char.ToUpper(name[0]) + name.Substring(1).ToLower();
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
}
