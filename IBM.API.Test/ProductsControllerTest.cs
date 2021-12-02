using IBM.API.Controllers;
using IBM.Application.Interfaces;
using IBM.Core.DTO;
using IBM.Core.Entities;
using IBM.Core.ObjectValues;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IBM.API.Test
{
    public class ProductsControllerTest
    {
        private readonly ProductsController controller;
        private readonly Mock<IProductService> mockService;
        private Mock<ILogger<ProductsController>> mockLogger;

        public ProductsControllerTest()
        {
            mockService = new Mock<IProductService>();
            mockLogger = new Mock<ILogger<ProductsController>>();
            ILogger<ProductsController> logger = mockLogger.Object;

            logger = Mock.Of<ILogger<ProductsController>>();
            controller = new ProductsController(mockService.Object, logger);

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
            var listofProductsExpected = new List<ProductOnlyResponse>()
            {
                new ProductOnlyResponse(new Product("A123")),
                new ProductOnlyResponse(new Product("P123")),
                new ProductOnlyResponse(new Product("Z343")),
            };
            mockService.Setup(x => x.GetProductsAsync()).ReturnsAsync(listofProductsExpected);

            var controllerResult = await controller.GetAsync();
            var result = (OkObjectResult)controllerResult.Result;
            var listOfProducts = (IEnumerable<ProductOnlyResponse>)result.Value;

            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(listOfProducts.Count(), listofProductsExpected.Count());

        }

        [Fact]
        public async Task SouldGetAProduct()
        {
            var productRequest = new ProductRequest() { sku = "123456" };

            var transactionListMock = new List<Transaction>()
            {
                new Transaction() { sku = productRequest.sku, amount = 10.5m, currency = CurrencyConstants.EUR },
                new Transaction() { sku = productRequest.sku, amount = 17.5m, currency = CurrencyConstants.EUR },
                new Transaction() { sku = productRequest.sku, amount = 1000m, currency = CurrencyConstants.EUR },
                };

            var productResponse = new ProductResponse()
            {
                currency = CurrencyConstants.EUR,
                total = transactionListMock.Count,
                sum = transactionListMock.Sum(x => x.amount),
                transactions = transactionListMock
            };

            mockService.Setup(x => x.GetProductAsync(productRequest)).ReturnsAsync(productResponse);

            var controllerResult = await controller.GetAsync(productRequest);
            var result = (OkObjectResult)controllerResult.Result;
            var product = (ProductResponse)result.Value;
            int sumExpected = 1028;
            int totalExpected = 3;

            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(product);
            Assert.Equal(product.sum, sumExpected);
            Assert.Equal(product.total, totalExpected);
            Assert.Equal(product.transactions.Count, totalExpected);
        }

        [Fact]
        public async Task SouldNOTGetAProduct()
        {
            var productRequest = new ProductRequest() { sku = "123456" };
            var productResponse = new ProductResponse();
            mockService.Setup(x => x.GetProductAsync(productRequest)).ReturnsAsync(productResponse);
            var controllerResult = await controller.GetAsync(productRequest);
            var result = (NotFoundObjectResult)controllerResult.Result;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SouldGetAnControlledExceptionByEmptyParameter()
        {
            var productRequest = new ProductRequest() { sku = "" };
            mockService.Setup(x => x.GetProductAsync(productRequest)).ThrowsAsync(new ArgumentNullException());
            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.GetAsync(productRequest));

        }

    }
}
