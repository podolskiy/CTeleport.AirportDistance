using System;

namespace CTeleport.AirportDistance.Services.Contracts
{
    public static class MathExtensions
    {

        public static double ToDegrees(this double val) { return val * 180 / Math.PI; }
        public static double ToRadians(this double val) { return val * Math.PI / 180; }
        public static double KmToMiles(this double val) { return val * 0.62137; }
    }
}