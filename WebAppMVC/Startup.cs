using LoggerService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using WebApiClient;
using WebApiClient.Interfaces;
using WebAppMVC.ActionFilters;

//namespace WebAppMVC
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            //Registers the Dependency Injection code
//            //for providing an implementation of IBlogSharpApiClient whenever needed
//            services.AddScoped<IProductClient>((cs) => new ApiClient(Configuration["WebApiURI"]));

//            //AutoMapper config
//            services.AddAutoMapper(typeof(Startup));

//            //Logger manager config
//            services.AddSingleton<ILoggerManager, LoggerManager>();

//            //Register global filters
//            services.AddControllers(options =>
//            {
//                options.Filters.Add<ExceptionFilter>();
//            });
//            services.AddScoped<ExceptionFilter>();

//            //Adds the cookie authentication scheme
//            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//            options =>
//            {
//                //sets the paths to send users, because we like the plural 's' on Accounts
//                //and the default paths are /accoun/login, /account/accessdenied, and /account/logout
//                options.LoginPath = "/Accounts/Login";
//                options.AccessDeniedPath = "/Accounts/AccessDenied";
//                options.LogoutPath = "/Accounts/LogOut";
//            });

//            services.AddControllersWithViews();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();
//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
//            });
//        }
//    }
//}
