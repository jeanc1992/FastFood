namespace FastFood.Domain.Dto.Orders.Request
{
    public class OrderRequestDto
    {
        public string Description { get; set; }
        public List<OrderProductRequestDto> Products { get; set; }
    }
}
