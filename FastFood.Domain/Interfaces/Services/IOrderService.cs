using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Orders.Request;
using FastFood.Domain.Dto.Orders.Response;
using FastFood.Domain.Enums;

namespace FastFood.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ApiResponseDto<OrderResponseDto>> CreateOrder(OrderRequestDto dto);
        Task<ApiListResponseDto<OrderResponseDto>> GetAllOrders(OrderStatusType? status);
        Task<ApiResponseDto<OrderResponseDto>> GetOrder(long id);
        Task<ApiResponseDto<OrderResponseDto>> ChangeStatus(long id, OrderStatusType status);
    }
}
