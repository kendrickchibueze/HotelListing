using HotelListing.Data.Configurations;
using HotelListing.Extensions;
using Microsoft.OpenApi.Models;


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
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MapperInitializer));
            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.ConfigureSwaggerDoc();

            builder.Services.AddSwaggerGen(sw =>

               sw.SwaggerDoc("v1",
               new OpenApiInfo { Title = "HotelListing", Version = "1.0" }));

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }

}

