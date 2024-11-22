using AutoRepairMainCore.Infrastructure;

using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddDbContext<MySqlContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
              .EnableSensitiveDataLogging()
              .LogTo(Console.WriteLine));

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
