using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NLog;
using WebApi.ActionFilters;
using static WebApi.ActionFilters.ValidationFilter;

//namespace WebApi
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
            

//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            //Data access 
//            services.AddScoped((sc) => DataAccessFactory.CreateRepository<IProductDataAccess>(Configuration.GetConnectionString("DefaultConnection")));

//            //AutoMapper config
//            services.AddAutoMapper(typeof(Startup));

//            //Logger manager config
//            services.AddSingleton<ILoggerManager, LoggerManager>();

//            //Surppress default validation filters
//            services.Configure<ApiBehaviorOptions>(options =>
//            {
//                options.SuppressModelStateInvalidFilter = true;
//            });

//            //Register global filters
//            services.AddControllers(options =>
//            {
//                options.Filters.Add<ExceptionFilter>();
//                //options.Filters.Add(new ValidationFilterAttribute()); 
//            });

//            //Register scoped filters
//            services.AddScoped<ValidationFilter>();

//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
//            });
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseSwagger();
//                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
//            }

//            app.UseHttpsRedirection();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
