using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GeneralCarsController : Controller
    {
        private readonly IGeneralCarsService _generalCarsService;

        public GeneralCarsController(IGeneralCarsService generalCarsService)
        {
            _generalCarsService = generalCarsService;
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                List<Brand> brands = await _generalCarsService.GetBrandsAsync();
                return Ok(brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving brands: {ex.Message}");
            }
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetModels()
        {
            try
            {
                List<Model> models = await _generalCarsService.GetModelsAsync();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving models: {ex.Message}");
            }
        }

        [HttpGet("engines")]
        public async Task<IActionResult> GetEngines()
        {
            try
            {
                List<Engine> engines = await _generalCarsService.GetEnginesAsync();
                return Ok(engines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving engines: {ex.Message}");
            }
        }

        [HttpPost("addCar")]
        public async Task<IActionResult> AddCarToLibrary([FromBody] NewCarForGeneralLibraryDto addCarDto)
        {
            if (addCarDto == null)
            {
                return BadRequest("Invalid car data.");
            }

            try
            {
                var brand = await _generalCarsService.EnsureBrandExistsAsync(addCarDto.BrandName);
                var model = await _generalCarsService.EnsureModelExistsAsync(addCarDto.ModelName);
                var engine = await _generalCarsService.EnsureEngineExistsAsync(addCarDto.EngineDescription);

                // Step 4: Create the Car entity
                var car = new Car
                {
                    BrandId = brand.Id,
                    ModelId = model.Id,
                    EngineId = engine.Id
                };

                // Step 5: Add the Car to the library
                await _generalCarsService.AddCarAsync(car);

                return Ok("Car added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding car to the library: {ex.Message}");
            }
        }
    }
}
