
using Microsoft.Data.SqlClient;
using Mini_Sonic.Service;
using System.Data;
using Mini_Sonic.Controllers;

namespace Mini_Sonic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<CategoryService>();
            builder.Services.AddTransient<ItemService>();

            //Console.ReadKey();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAngular");

            app.MapControllers();


            app.Run();
        }
    }
}
