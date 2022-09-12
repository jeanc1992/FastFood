using FastFood.Domain.Base;

namespace FastFood.Domain.Entities
{
    public class OrderProduct : EntityBase
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
