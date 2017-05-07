# WebApiMultipleRoutePrefixAttribute
For details, visit https://3btechtalk.wordpress.com/2017/05/07/defining-multiple-route-prefix-in-asp-net-web-api-attribute-routing/

------

Create a new file in project and name it 3bTechTalkMultiplePrefixDirectRouteProvider.cs, you can copy the class code from https://github.com/asadmalik3/WebApiMultipleRoutePrefixAttribute/blob/master/3bTechTalk.MultipleRoutePrefixAttributes/3bTechTalkMultiplePrefixDirectRouteProvider.cs

Create another file and name it 3bTechTalkRoutePrefix.cs, you can copy the classe code from 
https://github.com/asadmalik3/WebApiMultipleRoutePrefixAttribute/blob/master/3bTechTalk.MultipleRoutePrefixAttributes/3bTechTalkRoutePrefix.cs

Now open WebApiConfig.cs and add this line 
config.MapHttpAttributeRoutes(new _3bTechTalkMultiplePrefixDirectRouteProvider());

That's it, Now you can add multiple route prefix attribute in your controller. Example below

``` C#

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

```
