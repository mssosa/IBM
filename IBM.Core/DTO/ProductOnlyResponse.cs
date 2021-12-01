using IBM.Core.Entities;

namespace IBM.Core.DTO
{
    public class ProductOnlyResponse
    {

        public ProductOnlyResponse(Product item)
        {
            sku = item.sku;
        }

        public string sku { get; set; }
    }
}
