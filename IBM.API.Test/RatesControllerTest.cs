using IBM.API.Controllers;
using IBM.Application.Interfaces;
using IBM.Core.DTO;
using IBM.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IBM.API.Test
{
    public class RatesControllerTest
    {
        private readonly RatesController controller;
        private readonly Mock<IRateService> mockService;
        private Mock<ILogger<RatesController>> mockLogger;

        public RatesControllerTest()
        {
            mockService = new Mock<IRateService>();
            mockLogger = new Mock<ILogger<RatesController>>();
            ILogger<RatesController> logger = mockLogger.Object;
            logger = Mock.Of<ILogger<RatesController>>();
            controller = new RatesController(mockService.Object, logger);

        }

        [Fact]
        public async Task SouldGetReturnNotFound()
        {
            var controllerResult = await controller.GetAsync();
            var result = (NotFoundObjectResult)controllerResult.Result;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SouldGetReturnListOfProducts()
        {
            var listExpected = new List<RateResponse>()
            {
                new RateResponse(new Rate(){from="EUR",to="USD",rate =1.5m}),
                new RateResponse(new Rate(){from="USD",to="EUR",rate =0.5m}),
                new RateResponse(new Rate(){from="CAD",to="USD",rate =2.5m}),
                new RateResponse(new Rate(){from="USD",to="CAD",rate =0.5m}),
            };
            mockService.Setup(x => x.GetRatesAsync(false)).ReturnsAsync(listExpected);

            var controllerResult = await controller.GetAsync();
            var result = (OkObjectResult)controllerResult.Result;
            var listOfProducts = (IEnumerable<RateResponse>)result.Value;

            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(listOfProducts.Count(), listExpected.Count());

        }

        [Fact]
        public async Task SouldGetReturnNotFoundOfflineMode()
        {
            var controllerResult = await controller.GetOfflineAsync();
            var result = (NotFoundObjectResult)controllerResult.Result;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SouldGetReturnListOfProductsOfflineMode()
        {
            var listExpected = new List<RateResponse>()
            {
                new RateResponse(new Rate(){from="EUR",to="USD",rate =1.5m}),
                new RateResponse(new Rate(){from="USD",to="EUR",rate =0.5m}),
                new RateResponse(new Rate(){from="CAD",to="USD",rate =2.5m}),
                new RateResponse(new Rate(){from="USD",to="CAD",rate =0.5m}),
            };
            mockService.Setup(x => x.GetRatesAsync(true)).ReturnsAsync(listExpected);
            var controllerResult = await controller.GetOfflineAsync();
            var result = (OkObjectResult)controllerResult.Result;
            var listOfProducts = (IEnumerable<RateResponse>)result.Value;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(listOfProducts.Count(), listExpected.Count());

        }

    }
}
