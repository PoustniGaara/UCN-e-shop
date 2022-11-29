using DataAccessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Model;
using LoggerService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using WebApi.ActionFilters;
using WebApi.DTOs;
using NLog;
using WebApi.DtoProfiles;
using WebApi.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

//Logger manager config
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//Data acces
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<IProductDataAccess>(connectionString));
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<IOrderDataAccess>(connectionString));
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<IUserDataAccess>(connectionString));

//AutoMapper config
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Logger manager config
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add controllers with option with global filters
builder.Services.AddControllers(options =>
{
    //options.Filters.Add<ExceptionFilter>();
});

//Register scoped filters
builder.Services.AddScoped<ValidationFilter>();

//Surppress default validation filters
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
