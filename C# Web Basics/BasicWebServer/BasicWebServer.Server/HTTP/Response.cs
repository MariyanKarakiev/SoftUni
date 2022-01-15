﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.HTTP
{
    public class Response
    {
        public Response(StatusCode statusCode)
        {
            StatusCode = StatusCode;

            Headers.Add("Server", "My Web Server");
            Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }

        public StatusCode StatusCode { get; set; }
        public HeaderCollection Headers { get; } = new HeaderCollection();
        public string Body { get; set; }

    }
}
