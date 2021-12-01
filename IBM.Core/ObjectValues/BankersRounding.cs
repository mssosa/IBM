using IBM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.ObjectValues
{
    public class BankersRounding : IRounder
    {
        public decimal RoundValue(decimal valueToRound)
        {
            return Math.Round(valueToRound, 2); ;
        }
    }
}
