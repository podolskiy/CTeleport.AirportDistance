using System;
using System.Threading.Tasks;
using CTeleport.AirportDistance.Api.Models;
using CTeleport.AirportDistance.Services;
using Microsoft.AspNetCore.Mvc;

namespace CTeleport.AirportDistance.Api.Controllers
{
    public class AirportController : CTeleportBaseController
    {

        private readonly Lazy<IAirportService> _airportService;

        public AirportController(Lazy<IAirportService> airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        [ResponseCache(Duration = 60 * 60 /*1h*/, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { nameof(DistanceRequest.Src), nameof(DistanceRequest.Dst) })]
        public async Task<IActionResult> Distance([FromQuery] DistanceRequest model)
        {
            return Ok(await _airportService.Value.CalculateDistance(model));
        }
    }
}
