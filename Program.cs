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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

builder.Services.AddHttpClient("OpenAIMicroServiceClient", client =>
{
    client.BaseAddress =new Uri(OpenAiMicroServiceConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IGeneralCarsService, GeneralCarsService>();
builder.Services.AddScoped<ITokenValidationService, TokenValidationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
