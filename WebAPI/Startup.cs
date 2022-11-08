using DataAccessLayer;
using DataAccessLayer.Model;
using LoggerService;
using Microsoft.OpenApi.Models;
using NLog;
using WebApi.DTOs;
using WebApi.Extensions;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Logger manager config
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped((sc) => DataAccessFactory.CreateRepository<IProductDataAccess>(Configuration.GetConnectionString("DefaultConnection")));

            //AutoMapper config
            services.AddAutoMapper(typeof(Startup));

            //Logger manager config
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            //Logger manager config
            app.ConfigureExceptionHandler(logger);
            //app.ConfigureCustomExceptionMiddleware(logger):

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
