using System.ComponentModel.DataAnnotations.Schema;

namespace IBM.Core.Entities
{
    public class Rate : EntityBase
    {
        public string from { get; set; }
        public string to { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal rate { get; set; }
    }

}
