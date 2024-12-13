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
            generalCarsService = dropdownService;
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                var brands = await _generalCarsService.GetBrandsAsync();
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
                var models = await _generalCarsService.GetModelsAsync();
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
                var engines = await _generalCarsService.GetEnginesAsync();
                return Ok(engines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving engines: {ex.Message}");
            }
        }
    }
}
