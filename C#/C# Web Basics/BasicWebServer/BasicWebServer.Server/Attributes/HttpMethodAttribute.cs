﻿using BasicWebServer.Server.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute 
    {
        public Method HttpMethod { get; }
        protected HttpMethodAttribute(Method httpMethod)
            => HttpMethod = httpMethod;
    }
}
