using IBM.Core.Interfaces;

namespace IBM.Core.ObjectValues
{
    public class RateOperationWith : IRateOperationWith
    {
        private IRounder rounder;

        public RateOperationWith(IRounder rounder)
        {
            this.rounder = rounder;
        }
        public decimal CalculateNewAmount(decimal from, decimal rate)
        {
            decimal result;
            result = from * rate;
            return rounder.RoundValue(result);
        }

        public decimal CalculateNewAmountInverse(decimal from, decimal rate)
        {

            decimal result;
            result = from / rate;
            return rounder.RoundValue(result);
        }

    }

}
