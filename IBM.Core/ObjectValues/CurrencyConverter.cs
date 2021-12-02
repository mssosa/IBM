using IBM.Core.Entities;
using IBM.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IBM.Core.ObjectValues
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly IRateOperationWith calculator;

        public CurrencyConverter(IRateOperationWith calculator)
        {
            this.calculator = calculator;
        }
        public Transaction ConvertToEUR(Transaction item, IEnumerable<Rate> rates)
        {
            var returnValue = Factory.PrepareTransaction();
            var rateFinded = Factory.PrepareEmptyRate();

            rateFinded = SearchDirect(CurrencyConstants.EUR, item.currency, rates);

            if (rateFinded != null)
            {
                returnValue.Product = item.Product;
                returnValue.amount = calculator.CalculateNewAmount(item.amount, rateFinded.rate);
                returnValue.currency = CurrencyConstants.EUR;
                returnValue.sku = $"{item.sku}{CurrencyConstants.CalculatedValue}";
                return returnValue;
            }

            var result = CascadeSearch(item, rates);

            return result;
        }
        private Transaction CascadeSearch(Transaction transaction, IEnumerable<Rate> rates, Transaction previousTransaction = null)
        {
            var returnValue = Factory.PrepareTransaction();

            var listsOfPosibilities = rates
                .Where(x => x.from.ToUpper().Equals(transaction.currency.ToUpper())
                && x.to.ToUpper() != previousTransaction?.currency.ToUpper()
                ).ToList();

            foreach (var selectedRate in listsOfPosibilities)
            {
                var rateFromSearch = selectedRate.to;
                var rateFinded = Factory.PrepareEmptyRate();

                var firstTry = Factory.PrepareTransaction();
                firstTry.sku = $"{transaction.sku}";
                firstTry.currency = rateFromSearch;
                firstTry.amount = 1;

                rateFinded = SearchDirect(firstTry.currency, CurrencyConstants.EUR, rates);

                if (rateFinded != null)
                {
                    returnValue.Product = transaction.Product;
                    returnValue.amount = calculator.CalculateNewAmount(transaction.amount, rateFinded.rate);
                    returnValue.currency = CurrencyConstants.EUR;
                    returnValue.sku = $"{transaction.sku}{CurrencyConstants.CalculatedValue}";
                    return returnValue;

                }

                var transactionFROM = Factory.PrepareTransaction();
                transactionFROM.sku = "";
                transactionFROM.currency = rateFromSearch;
                transactionFROM.amount = calculator.CalculateNewAmount(transaction.amount, selectedRate.rate); 


                var transcationCalculated = CascadeSearch(transactionFROM, rates, transaction);
                if (transcationCalculated != null)
                    return transcationCalculated;
            }
            return null;
        }
        public Rate SearchDirect(string _from, string _to, IEnumerable<Rate> rates)
        {
            return rates.Where(x => x.from.ToUpper().Equals(_from.ToUpper())
                            && x.to.ToUpper().Equals(_to.ToUpper()
                            )).FirstOrDefault();
        }
    }
}
