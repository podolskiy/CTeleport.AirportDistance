using System.Diagnostics;

namespace CTeleport.AirportDistance.Services.Models
{
    [DebuggerDisplay("{Lat}, {Lon}")]
    public class Location
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}