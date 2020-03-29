using CTeleport.AirportDistance.Services.Models;
using FluentValidation;

namespace CTeleport.AirportDistance.Services.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(_ => _.Lat).GreaterThan(-90).LessThan(90);
            RuleFor(_ => _.Lon).GreaterThan(-180).LessThan(180);
        }
    }
}
