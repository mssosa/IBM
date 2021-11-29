using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IBM.API.Test
{
    public class TestProvider
    {
        public readonly HttpClient client;

        public TestProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

    }
}
