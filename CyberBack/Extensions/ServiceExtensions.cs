#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace CyberBack.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin() // allows requests from any source
                        .AllowAnyMethod() //allows all HTTP methods
                        .AllowAnyHeader()); // allow "accept","content-type"
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
    }
}