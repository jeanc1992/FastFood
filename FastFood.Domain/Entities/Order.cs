using FastFood.Domain.Base;
using FastFood.Domain.Enums;

namespace FastFood.Domain.Entities
{
    public class Order : EntityBase
    {
        public string OrderNumber
        {
            get
            {
                return Id.ToString("D8");
            }
        }
        public string Description { get; set; }
        public List<OrderProduct> OrderProduct { get; set; }
        public OrderStatusType Status { get; set; }
    }
}
