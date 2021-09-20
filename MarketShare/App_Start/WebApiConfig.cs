
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using System.Web.Http;
using MarketShare.Authentication;

namespace MarketShare
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new JwtAuthenticationAttribute());
            config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();



            // Web-API-Routen

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
