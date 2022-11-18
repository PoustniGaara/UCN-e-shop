//namespace WebAppMVC
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}


using LoggerService;
using WebApiClient;
using WebApiClient.Interfaces;
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


//builder.Services.AddScoped<IApiClient>((cs) => new ApiClient(Configuration["WebApiURI"]));

//AutoMapper config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Logger manager config
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

//Register global filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
