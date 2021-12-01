using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IBM.Core.Entities
{
    public class Transaction : EntityBase
    {
        public string sku { get; set; }
        public string currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal amount { get; set; }

        public Product Product { get; set; }
        public int ProductID { get; set; }


    }

}
