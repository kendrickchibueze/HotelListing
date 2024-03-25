using HotelListing.Data;
using HotelListing.Implementations;
using HotelListing.Interfaces;
using HotelListing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
          services.AddCors(options =>
          {
              options.AddPolicy("CorsPolicy", builder =>
              builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
          });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
          services.Configure<IISOptions>(options =>
          {

          });
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
        }
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var Connection = configuration.GetSection("ConnectionString")["DefaultConn"];

            services.AddDbContext<DatabaseContext>(options =>
            {

                options.UseSqlServer(Connection);
            });

            services.AddIdentity<ApiUser, IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
              .AddDefaultTokenProviders();
            services.AddMemoryCache();
            services.ConfigureAutoMapper();
            services.AddTransient<IUnitOfWork, UnitOfWork<DatabaseContext>>(); //creates a fresh copy of Iunitofwork when hit controller
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IHotelService, HotelService>();
        }

    }
}
