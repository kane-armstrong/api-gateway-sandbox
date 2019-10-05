using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "Weather Forecast API";
                document.Title = "Weather Forecast API";
                document.Description = "This API returns weather forecasts";
                document.Version = "v1";
            });
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHealthChecks("/", new HealthCheckOptions());
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
