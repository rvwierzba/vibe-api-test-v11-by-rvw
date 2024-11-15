using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VibeApiTestV11ByRvw.Models;
using VibeApiTestV11ByRvw.Services.Interfaces;

namespace VibeApiTestV11ByRvw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacemarksController : ControllerBase
    {
        private readonly IKmlService _kmlService;
        private readonly IFilterValidator _filterValidator;

        public PlacemarksController(IKmlService kmlService, IFilterValidator filterValidator)
        {
            _kmlService = kmlService;
            _filterValidator = filterValidator;
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportKml([FromBody] PlacemarkFilter filter)
        {
            var availableFilters = await _kmlService.GetAvailableFiltersAsync();
            var validationResult = _filterValidator.Validate(filter, availableFilters);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var kmlBytes = await _kmlService.ExportFilteredKmlAsync(filter);
            return File(kmlBytes, "application/vnd.google-earth.kml+xml", "filtered.kml");
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacemarks([FromQuery] PlacemarkFilter filter)
        {
            var availableFilters = await _kmlService.GetAvailableFiltersAsync();
            var validationResult = _filterValidator.Validate(filter, availableFilters);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var placemarks = await _kmlService.GetFilteredPlacemarksAsync(filter);
            return Ok(placemarks);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetAvailableFilters()
        {
            var filters = await _kmlService.GetAvailableFiltersAsync();
            return Ok(filters);
        }
    }
}
