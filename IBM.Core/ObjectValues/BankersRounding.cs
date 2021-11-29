using IBM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.ObjectValues
{
    public class BankersRounding : IRounder
    {
        public double RoundValue(double valueToRound)
        {
            return Math.Round(valueToRound, 2); ;
        }
    }
}
