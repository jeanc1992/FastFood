using FastFood.Domain.Enums;

namespace FastFood.Domain.Dto.Orders.Response
{
    public class OrderResponseDto
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public List<OrderProductResponseDto> Products { get; set; }
        public OrderStatusType Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
