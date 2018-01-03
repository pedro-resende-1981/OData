using AspNet461Odata.Ioc;
using System.Web.Http;
using WebApi.StructureMap;

namespace AspNet461Odata
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.UseStructureMap<MyRegistry>();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
