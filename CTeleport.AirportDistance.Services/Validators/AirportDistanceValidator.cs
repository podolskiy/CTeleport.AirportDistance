using CTeleport.AirportDistance.Services.Models;
using FluentValidation;

namespace CTeleport.AirportDistance.Services.Validators
{
    public class AirportDistanceValidator<T> : AbstractValidator<T> where T : DistanceModel

    {
        public AirportDistanceValidator()
        {
            RuleFor(_ => _.Src).NotEmpty().Length(3).WithMessage("Src must be 3 letters IATA airport code");
            RuleFor(_ => _.Dst).NotEmpty().Length(3).WithMessage("Dst must be 3 letters IATA airport code");
        }
    }
}