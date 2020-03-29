using System;
using System.Threading.Tasks;
using CTeleport.AirportDistance.Services.Models;

namespace CTeleport.AirportDistance.Services
{
    public interface IAirportService
    {
        Task<double> CalculateDistance(DistanceModel distanceModel);
    }
}
