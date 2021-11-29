using IBM.API.Controllers;
using IBM.Core.Entities;
using IBM.Infrastructure.ExternalComunication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IBM.API.Test
{
    public class TestsForApi
    {
        private readonly HttpClient client;
        private const string ApiBaseAddress = "/api/Transactions/";

        public TestsForApi()
        {
            var provider = new TestProvider();
            client = provider.client;
        }
        [Fact]
        public async Task TestTransactionIsOK()
        {
            var response = await client.GetAsync(ApiBaseAddress);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

          
        }
    }
}
