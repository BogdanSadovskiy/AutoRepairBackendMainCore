using AutoRepairMainCore.DTO;
using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                List<Brand> brands = _generalCarsService.GetBrands();
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
                List<Model> models = _generalCarsService.GetModels();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving models: {ex.Message}");
            }
        }

        [HttpGet("engines")]
        public async Task<IActionResult> GetEngines([FromHeader(Name = "Authorization")] string token)
        {
            try
            {
                List<Engine> engines = _generalCarsService.GetEngines();
                return Ok(engines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving engines: {ex.Message}");
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("addCar")]
        public async Task<IActionResult> AddCarToLibrary(
            [FromBody] CarDto newCar)
        {
            _generalCarsService.AddCar(newCar);
            return Ok("Car added successfully.");
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("openAICarValidation")]
        public async Task<IActionResult> GPTValidation (
            [FromBody] CarDto car)
        {
            CarDto validatedCar = await _generalCarsService.OpenAICarValidation(car);
            return Ok(new {data = validatedCar, message = "OpenAI response" });
        }
    }
}
