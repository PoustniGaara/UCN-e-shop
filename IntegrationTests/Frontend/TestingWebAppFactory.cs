using DataAccessLayer.Interfaces;
using DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WebApiClient.RestSharpClientImplementation;
using WebApi.ActionFilters;

namespace IntegrationTests.Frontend
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DataAccessFactory));
                if (descriptor != null)
                    services.Remove(descriptor);

                //I do not add any other class for testing purposes
                //services.AddDbContext<EmployeeContext>(options =>
                //{
                //    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                //});

                //Idk what is this
                //var sp = services.BuildServiceProvider();
                //using (var scope = sp.CreateScope())
                //using (var appContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>())
                //{
                //    try
                //    {
                //        appContext.Database.EnsureCreated();
                //    }
                //    catch (Exception ex)
                //    {
                //        //Log errors or do anything you think it's needed
                //        throw;
                //    }
                //}
            });
        }
    }
}
