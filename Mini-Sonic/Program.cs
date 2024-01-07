
using Microsoft.Data.SqlClient;
using Mini_Sonic.Service;
using System.Data;
using Mini_Sonic.Controllers;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Repositories;
using Microsoft.OpenApi.Models;
using System.Configuration;

namespace Mini_Sonic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
       
            //builder.Services.AddTransient<UserManager>();
            //builder.Services.AddTransient<UserService>();

            //builder.Services.AddTransient<CategoryManager>();
            //builder.Services.AddTransient<CategoryService>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepositry<>));
            //builder.Services.AddTransient<ItemService>();
            //builder.Services.AddTransient<ItemManager>();

            //builder.Services.AddTransient<OperationManager>();
            //builder.Services.AddTransient<OperationService>();



            builder.Services.AddControllers();



            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sonic", Version = "v1" });
            });

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sonic v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAngular");

            app.MapControllers();


            app.Run();
        }
    }
}
