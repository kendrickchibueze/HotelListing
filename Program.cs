using HotelListing.Core.IRepository;
using HotelListing.Core.Repository;
using HotelListing.Data;
using HotelListing.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddDbContext<DatabaseContext>(opts =>
            {


                var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];

                opts.UseSqlServer(defaultConn);

            });



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            builder.Services.AddAutoMapper(typeof(MapperInitializer));



            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddSwaggerGen(sw =>

               sw.SwaggerDoc("v1",
               new OpenApiInfo { Title = "HotelListing", Version = "1.0" }));




            var app = builder.Build();






            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("AllowAll");

            app.UseHttpsRedirection();




            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }




    }

}

