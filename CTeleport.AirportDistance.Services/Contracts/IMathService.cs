using CTeleport.AirportDistance.Services.Models;

namespace CTeleport.AirportDistance.Services.Contracts
{
    public interface IMathService
    {
        double CalculateDistance(Location src, Location dst, DistanceMeasureType measureType = DistanceMeasureType.miles);
    }

    public enum DistanceMeasureType
    {
        km,miles
    }
}