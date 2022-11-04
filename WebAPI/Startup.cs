using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.OpenApi.Models;
using WebApi.DTOs;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDto>()
                    //ProductSize is a Complex type, so Map ProductSize to Simple type using For Member
                    .ForMember(dest => dest.Size, act => act.MapFrom(src => src.ProductSize.Size))
                    .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.ProductSize.Stock))
                    .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name))
                    .ReverseMap();
            });

            var mapper = new Mapper(config);
            return mapper;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped((sc) => DataAccessFactory.CreateRepository<IProductDataAccess>(Configuration.GetConnectionString("DefaultConnection")));


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
