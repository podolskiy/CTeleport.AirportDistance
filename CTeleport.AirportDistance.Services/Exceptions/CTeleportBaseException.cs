using System;
using System.Net;

namespace CTeleport.AirportDistance.Services.Exceptions
{
    public class CTeleportBaseException : ApplicationException
    {
        public HttpStatusCode ResponseCode { get; set; }

        public CTeleportBaseException(string message, HttpStatusCode responseCode) : base(message)
        {
            ResponseCode = responseCode;
        }
    }
}
