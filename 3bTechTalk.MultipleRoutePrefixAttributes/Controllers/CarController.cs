using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _3bTechTalk.MultipleRoutePrefixAttributes.Controllers
{
    [_3bTechTalkRoutePrefix("api/Car", Order = 1)]
    [_3bTechTalkRoutePrefix("{CountryCode}/api/Car", Order = 2)]
    public class CarController : ApiController
    {
        [Route("Get")]
        public IHttpActionResult Get()
        {
            return Ok(new { Id = 1, Name = "Honda Accord" });
        }
    }
}
