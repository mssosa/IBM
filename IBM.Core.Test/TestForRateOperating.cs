using IBM.Core.ObjectValues;
using Xunit;

namespace IBM.Core.Test
{
    public class TestForRateOperating
    {
        [Theory]
        [InlineData(1      , 1.05 ,1.05)]
        [InlineData(10     , 1.05 ,10.5)]
        [InlineData(235.65 , 1.05 ,247.43)]
        [InlineData(354.58 , 0.88 ,312.03)]
        [InlineData(45     , 1.04 ,46.8)]
        [InlineData(1535   , 1.37 ,2102.95)]
        public void ShoudCalculateNewAmountOK(decimal numberToConvert, decimal rate,decimal expected)
        {
            var rounder = new BankersRounding();
            var ratesCalculator = new RateOperationWith(rounder);
            var result = ratesCalculator.CalculateNewAmount(numberToConvert, rate);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(1      , 1.05, 0.95)]
        [InlineData(10     , 1.05, 9.52)]
        [InlineData(235.65 , 1.05, 224.43)]
        [InlineData(354.58 , 0.88, 402.93)]
        [InlineData(45     , 1.04, 43.27)]
        [InlineData(1535   , 1.37, 1120.44)]
        public void ShouldConvertInverseToOK(decimal numberToConvert, decimal rate, decimal expected)
        {
            var rounder = new BankersRounding();
            var ratesCalculator = new RateOperationWith(rounder);
            var result = ratesCalculator.CalculateNewAmountInverse(numberToConvert, rate);
            Assert.Equal(result, expected);
        }
    }
}
