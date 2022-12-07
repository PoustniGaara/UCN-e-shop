using LoggerService;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApiClient;
using WebApiClient.Interfaces;
using WebApiClient.RestSharp_Client_Implementation;
using WebApiClient.RestSharpClientImplementation;
using WebAppMVC;
using WebAppMVC.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Register Api Clients with the URL to contact
//...
//Product client 
string productUrl = "https://localhost:44346/api/v1/products";
IProductClient productClient = new ProductClient(productUrl);
builder.Services.AddScoped<IProductClient>((cs) => productClient);

//Order client 
string orderUrl = "https://localhost:44346/api/v1/orders";
IOrderClient orderClient = new OrderClient(orderUrl);
builder.Services.AddScoped<IOrderClient>((cs) => orderClient);

//User client 
string userUrl = "https://localhost:44346/api/v1/users";
IUserClient userClient = new UserClient(userUrl);
builder.Services.AddScoped<IUserClient>((cs) => userClient);

//Category client 
string categoryUrl = "https://localhost:44346/api/v1/categories";
ICategoryClient categoryClient = new CategoryClient(categoryUrl);
builder.Services.AddScoped<ICategoryClient>((cs) => categoryClient);

//Authentication client 
string authUrl = "https://localhost:44346/api/v1/authentication";
IAuthenticationClient authenticationClient = new AuthenticationClient(authUrl);
builder.Services.AddScoped<IAuthenticationClient>((cs) => authenticationClient);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, 
            options =>
            {
                options.LoginPath = "/Authentication/Login";
                options.LogoutPath = "/Authentication/Logout";

            });


//AutoMapper config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Logger manager config
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

//Register global filters -- out of service righ now, we use scoped filters
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ExceptionFilter>();
//});

//Register scoped filters
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddScoped<ValidationFilter>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600); //3600
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
