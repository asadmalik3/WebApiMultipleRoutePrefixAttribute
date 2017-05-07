using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace _3bTechTalk.MultipleRoutePrefixAttributes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes(new _3bTechTalkMultiplePrefixDirectRouteProvider());
        }
    }
}
