using IBM.Infrastructure.ExternalProviders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IBM.Infrastructure.Test
{
    public class ExternalComunicationsTests
    {
        [Fact]
        public async Task ShouldOkComunicationTransactionTest()
        {
            var mock = new Mock<ILogger<ExternalProviderRepository>>();
            ILogger<ExternalProviderRepository> logger = mock.Object;

            //or use this short equivalent 
            logger = Mock.Of<ILogger<ExternalProviderRepository>>();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://quiet-stone-2094.herokuapp.com");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            // Github requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            var provider = new ExternalProviderRepository(client, logger);

            var result=await provider.ReadExternalApiAsync("/transactions.json");

            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);


        }

        [Fact]
        public async Task ShouldOkComunicationRatesTest()
        {
            var mock = new Mock<ILogger<ExternalProviderRepository>>();
            ILogger<ExternalProviderRepository> logger = mock.Object;

            //or use this short equivalent 
            logger = Mock.Of<ILogger<ExternalProviderRepository>>();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://quiet-stone-2094.herokuapp.com");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            // Github requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            var provider = new ExternalProviderRepository(client, logger);

            var result = await provider.ReadExternalApiAsync("/rates.json");

            Assert.NotNull(result);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);


        }
    }
}
