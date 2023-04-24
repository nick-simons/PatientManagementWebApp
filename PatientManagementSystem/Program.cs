using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PatientManagementSystem.Interfaces.Manager;
using PatientManagementSystem.Interfaces.Models;
using PatientManagementSystem.Interfaces.Repository;
using PatientManagementSystem.Managers;
using service_listeningtools.Repository;

namespace PatientManagementSystem
{

    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddDbContext<PatientRecordContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PatientRecordContext")));

            builder.Services.AddScoped<IPatientRecordManager, PatientRecordManager>();
            builder.Services.AddScoped<IPatientRecordRepository, PatientRecordRepository>();
            builder.Services.AddScoped<IPatientRecordModel, PatientRecord>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

