using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.Interfaces
{
    public interface IRounder
    {
        decimal RoundValue(decimal valueToRound);
    }
}
