using FastFood.Domain.Enums;

namespace FastFood.Domain.Dto.Products.Request
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public int Stock { get; set; }    }
}
