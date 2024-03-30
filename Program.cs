using AspNetCoreRateLimit;
using HotelListing.Data.Configurations;
using HotelListing.Extensions;
using Microsoft.AspNetCore.Mvc;
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
                app.UseSwaggerUI();
            }
            app.UseSerilogRequestLogging();
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

