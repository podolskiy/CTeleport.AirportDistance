using CTeleport.AirportDistance.Services.Contracts;

namespace CTeleport.AirportDistance.Api.Models
{
    public class AppConfiguration : IAppConfiguration
    {
        public string AirportApiUrl { get; set; }
    }
}