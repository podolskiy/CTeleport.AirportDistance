using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CTeleport.AirportDistance.Api.Models;
using CTeleport.AirportDistance.Services.Exceptions;
using CTeleport.AirportDistance.Services.Models;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CTeleport.AirportDistance.Services.Contracts
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly ILogger<IExternalApiService> _logger;
        private readonly IAppConfiguration _appConfiguration;
        private readonly IMemoryCache _memoryCache;

        public ExternalApiService(ILogger<ExternalApiService> logger, IAppConfiguration appConfiguration, IMemoryCache memoryCache)
        {
            _logger = logger;
            _appConfiguration = appConfiguration;
            _memoryCache = memoryCache;
        }
        public async Task<AirportInfo> GetAirportInfoAsync(string iataCode)
        {
            return  await _memoryCache.GetOrCreate($"airport_{iataCode}",async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                return await GetAirportInfoInternalAsync(iataCode);
            });
        }
        protected async Task<AirportInfo> GetAirportInfoInternalAsync(string iataCode)
        {
            var result = await _appConfiguration.AirportApiUrl
                .AllowAnyHttpStatus()
                .AppendPathSegment(iataCode)
                .GetAsync();

            if (!result.ResponseMessage.IsSuccessStatusCode)
            {
                throw new CTeleportBaseException($"External service error {result.StatusCode}", (HttpStatusCode)result.StatusCode);
            }

            return await result.GetJsonAsync<AirportInfo>();
        }
    }
}