
using Microsoft.EntityFrameworkCore;
using SendEmail_MailKit.Data;
using SendEmail_MailKit.Models;
using SendEmail_MailKit.Service;
using System;

namespace SendEmail_MailKit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerGen();

            //Configure EmailSettings 
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            //Register Email Services
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Configure connection String 
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
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


            app.MapControllers();

            app.Run();
        }
    }
}