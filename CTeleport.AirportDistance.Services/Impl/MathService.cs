using System;
using CTeleport.AirportDistance.Services.Models;
using FluentValidation;

namespace CTeleport.AirportDistance.Services.Contracts
{
    public class MathService : IMathService
    {
        private readonly IValidator<Location> _locationValidator;

        public MathService(IValidator<Location> locationValidator = null)
        {
            _locationValidator = locationValidator;
        }
        public double CalculateDistance(Location src, Location dst, DistanceMeasureType measureType = DistanceMeasureType.miles)
        {
            _locationValidator?.ValidateAndThrow(src);
            _locationValidator?.ValidateAndThrow(dst);

            var R = 6371e0;
            var φ1 = src.Lat.ToRadians();
            var φ2 = dst.Lat.ToRadians();
            var Δφ = (dst.Lat - src.Lat).ToRadians();
            var Δλ = (dst.Lon - src.Lon).ToRadians();

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = R * c;
            switch (measureType)
            {
                case DistanceMeasureType.km:
                    return distance;
                case DistanceMeasureType.miles:
                    return distance.KmToMiles();
                default:
                    throw new ArgumentOutOfRangeException(nameof(measureType), measureType, null);
            }
        }
    }
}