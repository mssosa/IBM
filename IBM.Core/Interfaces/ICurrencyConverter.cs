using IBM.Core.Entities;
using IBM.Core.Interfaces;
using System.Collections.Generic;

namespace IBM.Core.Interfaces
{
    public interface ICurrencyConverter
    {
        Transaction ConvertToEUR(Transaction item, IEnumerable<Rate> rates);
        Rate SearchDirect(string _from, string _to, IEnumerable<Rate> rates);
    }
}