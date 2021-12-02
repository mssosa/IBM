using Castle.Core.Logging;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace IBM.Core.Test
{
    public class ConvertionTest
    {
        private readonly List<Rate> rates;
        public ConvertionTest()
        {
            rates = new List<Rate>()
            {
                new Rate()
                 {
                    from= "EUR",
                    to= "AUD",
                    rate= 1.01m
                  },
                new Rate(){
                    from= "AUD",
                    to= "EUR",
                    rate= 0.99m
                },
                new Rate(){
                    from= "EUR",
                    to= "CAD",
                    rate= 1.14m
                },
                new Rate(){
                    from= "CAD",
                    to= "EUR",
                    rate= 0.88m
                },
                new Rate(){
                    from= "AUD",
                    to= "USD",
                    rate= 0.88m
                },
                new Rate(){
                    from= "USD",
                    to= "AUD",
                    rate= 1.14m
                }
            };


        }


        [Fact]
        public void ShouldConvertOKDirectlyAudToEur()
        {
            var rate = 1.01m;
            var calculator = new Mock<IRateOperationWith>();
            var transaction = new Transaction()
            {
                amount = 10,
                currency = "AUD",
                sku = "P1234"
            };
            calculator.Setup(c => c.CalculateNewAmount(transaction.amount, rate)).Returns(9.90m);

            var converted = new CurrencyConverter(calculator.Object);

            var result = converted.ConvertToEUR(transaction, rates);

            Assert.NotNull(result);
            Assert.Equal($"{transaction.sku}-[calculated]",result.sku);
            Assert.Equal("EUR",result.currency);
            var expectedValue = 9.90m;
            Assert.Equal(result.amount,expectedValue);
        }

        [Fact]
        public void ShouldConvertOKInderectlyUSDToEur()
        {
            var rate = 1.01m;
            var calculator = new Mock<IRateOperationWith>();
            var transaction = new Transaction()
            {
                amount = 10,
                currency = "USD",
                sku = "P1234"
            };
            calculator.Setup(c => c.CalculateNewAmount(transaction.amount, rate)).Returns(9.90m);

            var converted = new CurrencyConverter(calculator.Object);

            var result = converted.ConvertToEUR(transaction, rates);

            Assert.NotNull(result);
            Assert.Equal($"{transaction.sku}-[calculated]", result.sku);
            Assert.Equal("EUR", result.currency);
            var expectedValue = 9.90m;
            Assert.Equal(result.amount, expectedValue);
        }


    }
}
