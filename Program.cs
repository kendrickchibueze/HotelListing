using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using HotelListing.Data.Configurations;
using HotelListing.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureServices(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            builder.Services.AddAutoMapper(typeof(MapperInitializer));
            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.ConfigureSwaggerDoc();
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Hotel Listing API");
                });
            }
            app.UseSerilogRequestLogging();
            app.ConfigureExceptionHandler();
            app.MapHealthChecks("/healthcheck", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.MapHealthChecksUI();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

