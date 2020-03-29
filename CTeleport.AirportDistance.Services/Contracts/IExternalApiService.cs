using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTeleport.AirportDistance.Services.Models;

namespace CTeleport.AirportDistance.Services.Contracts
{
    public interface IExternalApiService
    {
        Task<AirportInfo> GetAirportInfoAsync(string IATA);
    }
}
