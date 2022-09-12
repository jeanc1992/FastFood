using FastFood.Domain.Base;
using FastFood.Domain.Enums;

namespace FastFood.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public int Stock { get; set; }
        public StatusType Status { get; set; }
    }
}
