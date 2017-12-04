using System.Web.Http;
using WebActivatorEx;
using Insurance.Api;
using Swashbuckle.Application;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Insurance.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
            .EnableSwagger(c => 
            {
                c.SingleApiVersion("v1", "Insurance.Api");
                c.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\Insurance.Api.xml");
            })
            .EnableSwaggerUi();
        }
    }
}