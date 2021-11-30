using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.Entities
{
    public class Transaction : EntityBase
    {
        public string sku { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }


    }

}
