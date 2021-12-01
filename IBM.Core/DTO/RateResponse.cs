using IBM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.DTO
{
    public class RateResponse
    {
        public RateResponse(Rate item)
        {
            from = item.from;
            to = item.to;
            rate = item.rate.ToString();
        }

        public string from { get; set; }
        public string to { get; set; }
        public string rate { get; set; }
    }
}
