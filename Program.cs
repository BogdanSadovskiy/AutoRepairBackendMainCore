using Autofac.Extensions.DependencyInjection;
using AutoRepairMainCore.Exceptions;
using AutoRepairMainCore.Infrastructure;
using AutoRepairMainCore.Service;
using AutoRepairMainCore.Service.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string mySqlConnectionString = builder.Configuration.GetConnectionString("MySqlConnection");
string OpenAiMicroServiceConnectionString = builder.Configuration.GetConnectionString("OpenAIConnection");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddDbContext<MySqlContext>(options =>
options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString))
              .EnableSensitiveDataLogging()
              .LogTo(Console.WriteLine));

builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorizationPolicies();

builder.Services.AddHttpClient("OpenAIMicroServiceClient", client =>
{
    client.BaseAddress =new Uri(OpenAiMicroServiceConnectionString);
});

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IGeneralCarsService, GeneralCarsService>();
builder.Services.AddScoped<ITokenValidationService, TokenValidationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaService, MediaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionsHandlerMiddleware>();

app.MapControllers();

app.Run();
