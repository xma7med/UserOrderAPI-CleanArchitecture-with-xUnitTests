
using Application.Contracts.Services;
using Application.Services;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace UserAndOrderTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                     options.UseInMemoryDatabase("UserOrdersDb"));

            builder .Services.AddScoped<IUserService, UserService> ();
            builder .Services.AddScoped<IAuditService, AuditService> ();
            builder .Services.AddScoped<IOrderService, OrderService> ();

            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();
            app.MapControllers();   
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

       

            app.Run();
        }
    }
}
