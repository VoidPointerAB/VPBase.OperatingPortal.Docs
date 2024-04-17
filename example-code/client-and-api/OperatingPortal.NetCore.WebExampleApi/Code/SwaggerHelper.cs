using Microsoft.OpenApi.Models;

namespace OperatingPortal.NetCore.WebExampleApi.Code
{
    public class SwaggerHelper
    {
        public static OpenApiInfo GetOpenApiModuleInfo(string moduleName)
        {
            return new OpenApiInfo
            {
                Version = moduleName.ToLower(),
                Title = "VPBase5 " + moduleName + " API",
                Description = "VPBase5 " + moduleName + " ASP.NET Core Web API",
                Contact = new OpenApiContact
                {
                    Name = "VPBase Contact",
                    Email = "kontakt@voidpointer.se",
                    Url = new Uri("https://vpbase.com/kontakt"),
                },
            };
        }
    }
}
