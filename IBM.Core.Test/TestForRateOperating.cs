using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Moq;
using Xunit;

namespace IBM.Core.Test
{
    public class TestForRateOperating
    {
        [Fact]
        public void ShoudCalculateNewAmountOK()
        {
            var rounder = new BankersRounding();
            var ratesCalculator = new RateOperationWith(rounder);
            var result = ratesCalculator.CalculateNewAmount(10, 1.01m);
            var expected = 9.90m;
            Assert.Equal(result, expected);

        }

        [Fact]
        public void ShouldConvertInverseToOK()
        {
            //var rounder = new BankersRounding();
            var rounder = new Mock<IRounder>();
            rounder.Setup(c => c.RoundValue(10.10m)).Returns(10.10m);
            var ratesCalculator = new RateOperationWith(rounder.Object);
            var result = ratesCalculator.CalculateNewAmountInverse(10, 1.01m);
            var expected = 10.10m;
            Assert.Equal(result, expected);
        }
    }
}
