using IBM.Core.Interfaces;

namespace IBM.Core.ObjectValues
{
    public class RateOperationWith
    {
        private IRounder rounder;

        public RateOperationWith(IRounder rounder)
        {
            this.rounder = rounder;
        }
        public decimal ConverTo(string from, string rate)
        {

            decimal result,auxConvertedFrom,auxConvertedRate;

            decimal.TryParse(from, out auxConvertedFrom);
            decimal.TryParse(rate, out auxConvertedRate);
            decimal fromConverted = auxConvertedFrom;
            decimal rateConverted = auxConvertedRate; 
            
            result = fromConverted / rateConverted;

            return  rounder.RoundValue(result);
        }

        public decimal ConvertInverseTo(string from, string rate)
        {

            decimal result, auxConvertedFrom, auxConvertedRate;

            decimal.TryParse(from, out auxConvertedFrom);
            decimal.TryParse(rate, out auxConvertedRate);
            decimal fromConverted = auxConvertedFrom;
            decimal rateConverted = auxConvertedRate;

            result = fromConverted * rateConverted;

            return rounder.RoundValue(result);
        }

    }

}
