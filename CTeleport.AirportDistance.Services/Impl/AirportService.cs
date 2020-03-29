using System;
using System.Threading.Tasks;
using CTeleport.AirportDistance.Services.Contracts;
using CTeleport.AirportDistance.Services.Models;
using FluentValidation;

namespace CTeleport.AirportDistance.Services
{
    public class AirportService : IAirportService
    {
        private readonly IExternalApiService _apiService;
        private readonly IMathService _mathService;
        private readonly IValidator<DistanceModel> _distanceModelValidator;

        public AirportService(IExternalApiService apiService, IMathService mathService, IValidator<DistanceModel> distanceModelValidator)
        {
            _apiService = apiService;
            _mathService = mathService;
            _distanceModelValidator = distanceModelValidator;
        }
        public async Task<double> CalculateDistance(DistanceModel distanceModel)
        {
            _distanceModelValidator.ValidateAndThrow(distanceModel);

            var airportSrc = await _apiService.GetAirportInfoAsync(distanceModel.Src);
            var airportDst = await _apiService.GetAirportInfoAsync(distanceModel.Dst);

            var distance =
                _mathService.CalculateDistance(airportSrc.Location, airportDst.Location, DistanceMeasureType.miles);

            return await Task.FromResult(distance);
        }
    }
}