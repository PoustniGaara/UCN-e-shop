using DataAccessLayer.Interfaces;
using DataAccessLayer;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;
using NLog;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = "https://dotnetdetail.net",
         ValidIssuer = "https://dotnetdetail.net",
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
     };
 });

//builder.Services.AddAuthentication(opt => {
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "https://localhost:5001",
//            ValidAudience = "https://localhost:5001",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
//        };
//    });

//Logger manager config
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

//// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//Data acces
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<IProductDataAccess>(connectionString));
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<IOrderDataAccess>(connectionString));
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<IUserDataAccess>(connectionString));
builder.Services.AddScoped((sc) => DataAccessFactory.CreateRepository<ICategoryDataAccess>(connectionString));

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
    options.Filters.Add<ExceptionFilter>();
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
