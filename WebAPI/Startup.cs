using DataAccessLayer;
using LoggerService;
using Microsoft.OpenApi.Models;
using NLog;
using WebApi.ActionFilters;

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

            //Register Filters
            //services.AddScoped<ExceptionFilter>();

            //Logger manager config
            services.AddSingleton<ILoggerManager, LoggerManager>();

            //services.AddControllers();

            // register filters
            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });

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
