using IBM.API.Controllers;
using IBM.Application.Interfaces;
using IBM.Core.DTO;
using IBM.Core.Entities;
using IBM.Core.ObjectValues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IBM.API.Test
{
    public class TransactionsControllerTests
    {
        private readonly TransactionsController controller;
        private readonly Mock<ITransactionService> mockService;
        private Mock<ILogger<TransactionsController>> mockLogger;

        public TransactionsControllerTests()
        {
            mockService = new Mock<ITransactionService>();
            mockLogger = new Mock<ILogger<TransactionsController>>();
            ILogger<TransactionsController> logger = mockLogger.Object;
            logger = Mock.Of<ILogger<TransactionsController>>();
            controller = new TransactionsController(mockService.Object, logger);
        }

        [Fact]
        public async Task SouldGetMethodReturnNotFound()
        {
            var controllerResult = await controller.GetAsync();
            var result = (NotFoundObjectResult)controllerResult.Result;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldGetAListOfTransactions()
        {
            var listExpected = new List<TransactionResponse>()
            {
                new TransactionResponse(new Transaction(){sku="P1234",amount=15m,currency=CurrencyConstants.CAD}),
                new TransactionResponse(new Transaction(){sku="E9875",amount=100m,currency=CurrencyConstants.EUR}),
                new TransactionResponse(new Transaction(){sku="U1234",amount=487m,currency=CurrencyConstants.USD}),
                new TransactionResponse(new Transaction(){sku="E1235",amount=340m,currency=CurrencyConstants.EUR}),
            };
            mockService.Setup(x => x.GetTransactionsAsync(false)).ReturnsAsync(listExpected);
            var controllerResult = await controller.GetAsync();
            var result = (OkObjectResult)controllerResult.Result;
            var listOfProducts = (IEnumerable<TransactionResponse>)result.Value;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(listOfProducts.Count(), listExpected.Count());

        }

        [Fact]
        public async Task SouldGetMethodReturnNotFoundOfflineMode()
        {
            var controllerResult = await controller.GetOfflineAsync();
            var result = (NotFoundObjectResult)controllerResult.Result;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task SouldGetReturnListOfProductsOfflineMode()
        {
            var listExpected = new List<TransactionResponse>()
            {
               new TransactionResponse(new Transaction(){sku="P1234",amount=15m,currency=CurrencyConstants.CAD}),
                new TransactionResponse(new Transaction(){sku="E9875",amount=100m,currency=CurrencyConstants.EUR}),
                new TransactionResponse(new Transaction(){sku="U1234",amount=487m,currency=CurrencyConstants.USD}),
                new TransactionResponse(new Transaction(){sku="E1235",amount=340m,currency=CurrencyConstants.EUR}),
            };
            mockService.Setup(x => x.GetTransactionsAsync(true)).ReturnsAsync(listExpected);
            var controllerResult = await controller.GetOfflineAsync();
            var result = (OkObjectResult)controllerResult.Result;
            var listOfProducts = (IEnumerable<TransactionResponse>)result.Value;
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(listOfProducts.Count(), listExpected.Count());

        }
    }
}
