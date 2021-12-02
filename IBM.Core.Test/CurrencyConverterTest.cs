using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace IBM.Core.Test
{
    public class CurrencyConverterTest
    {
        private readonly List<Rate> rates;
        private Mock<IRateOperationWith> mockCalculator;

        public CurrencyConverterTest()
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
                },
                new Rate(){
                    from= "EUR",
                    to= "USD",
                    rate= 1.14m
                },
                new Rate(){
                    from= "USD",
                    to= "EUR",
                    rate= 0.80m
                }
                ,
                new Rate(){
                    from= "ARS",
                    to= "USD",
                    rate= 0.01m
                },
                new Rate(){
                    from= "USD",
                    to= "ARS",
                    rate= 106.00m
                }
            };
            mockCalculator = new Mock<IRateOperationWith>();
        }

        [Theory]
        [InlineData("AUD", "EUR", 0.99)]
        [InlineData("USD", "EUR", 0.80)]
        public void ShouldSearchDirectTest(string from, string to, decimal rate)
        {
            var subject = new CurrencyConverter(mockCalculator.Object);
            var result = subject.SearchDirect(from, to, rates);
            Assert.NotNull(result);
            Assert.Equal(from, result.from);
            Assert.Equal(to, result.to);
            Assert.Equal(rate, result.rate);
        }

        [Theory]
        [InlineData("ARS", 250, 2)]
        public void ShouldCascadeSearchTEst(string from, decimal amoutToConvert, decimal rateResult)
        {
            var rounder = new BankersRounding();
            var calc = new RateOperationWith(rounder);
            var subject = new CurrencyConverter(calc);
            var transaction = new Transaction();
            transaction.amount = amoutToConvert; //250 pesos arg = basura...
            transaction.currency = from;
            var result = subject.CascadeSearch(transaction, rates);
            Assert.NotNull(result);
            Assert.Equal(rateResult, result.amount);
        }
    }
}
