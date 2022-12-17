using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendIntegrationTest
{
    public class TestOrderController : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public TestOrderController(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


    }
}
