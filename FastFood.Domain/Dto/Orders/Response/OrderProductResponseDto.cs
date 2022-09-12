namespace FastFood.Domain.Dto.Orders.Response
{
    public class OrderProductResponseDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
