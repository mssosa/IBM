using IBM.Core.ObjectValues;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IBM.Core.Test
{
    public class RoundingTest
    {
        [Theory]
        [InlineData(0.035, 0.04)]
        [InlineData(1.55,1.55)]
        [InlineData(1.91,1.91)]
        [InlineData(2.500,2.50)]
        [InlineData(2.999,3)]
        [InlineData(10.158,10.16)]
        public void SholdBeOKCasesRounderBank(decimal valueIN, decimal valueOUT)
        {
            var rouderer = new BankersRounding();
            Assert.Equal(rouderer.RoundValue(valueIN), valueOUT);
        }
    }
}
