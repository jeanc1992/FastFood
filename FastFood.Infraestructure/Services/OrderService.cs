using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Orders.Request;
using FastFood.Domain.Dto.Orders.Response;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FastFood.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IAppDataService _appDataService;
        private readonly IProductService _productService;
        public OrderService(ILogger<OrderService> logger, IAppDataService appDataService, IProductService productService)
        {
            _logger = logger;
            _appDataService = appDataService;
            _productService = productService;
        }
        public async Task<ApiResponseDto<OrderResponseDto>> ChangeStatus(long id, OrderStatusType status)
        {
            _logger.LogInformation($"{nameof(ChangeStatus)}: Get order");
            var order =await  _appDataService.Orders.FirstOrDefaultAsync(r => r.Id == id);
            if(order == null)
            {
                throw new InvalidRequestException($"{nameof(ChangeStatus)}: The order id: {id} not found");
            }

            if(order.Status == OrderStatusType.Canceled)
            {
                throw new InvalidRequestException($"{nameof(ChangeStatus)}: The order status has not been changed");
            }

            order.Status = status;

            _appDataService.Orders.Update(order);
            await _appDataService.SaveChangesAsync();

            return await GetOrder(order.Id);
        }

        public async Task<ApiResponseDto<OrderResponseDto>> CreateOrder(OrderRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateOrder)}: validating product Stock");
             await ValidateProductStock(dto.Products);
       

            _logger.LogInformation($"{nameof(CreateOrder)}: Create new order");

            var order = new Order
            {
                Description = dto.Description,
                Status = OrderStatusType.Pending,
                OrderProduct = dto.Products.Select(r => new OrderProduct { ProductId = r.ProductId, Quantity = r.Quantity }).ToList(),
            };
            _appDataService.Orders.Add(order);
            foreach (var item in dto.Products)
            {
                var product = await _appDataService.Products.FirstOrDefaultAsync(r => r.Id == item.ProductId);
                 if(product== null)
                {
                    throw new InvalidRequestException($"{nameof(GetOrder)}: The product id: {item.ProductId} not found");
                }

                product.Stock -= item.Quantity;
                _appDataService.Products.Update(product);
            }
            await _appDataService.SaveChangesAsync();

            _logger.LogInformation($"{nameof(CreateOrder)}: order has been saved");

            return await GetOrder(order.Id);
        }

        public async Task<ApiListResponseDto<OrderResponseDto>> GetAllOrders(OrderStatusType? status)
        {
            _logger.LogInformation($"{nameof(GetAllOrders)}: Getting all orders by status: {status}");

            var orders = await _appDataService.Orders.GetAllOrders(status);

            var response = orders.Select(r => new OrderResponseDto
            {
                Id = r.Id,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt,
                Description = r.Description,
                OrderNumber = r.OrderNumber,
                Status = r.Status,
                Products = r.OrderProduct.Select(s => new OrderProductResponseDto
                {
                    Id = s.Id,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt =s.UpdatedAt,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList()
            }).ToList();

            _logger.LogInformation($"{nameof(GetAllOrders)}: Got orders: {response.Count()}");

            return new ApiListResponseDto<OrderResponseDto>(response);
        }

        public async Task<ApiResponseDto<OrderResponseDto>> GetOrder(long id)
        {
            _logger.LogInformation($"{nameof(GetOrder)}: Getting order whith id: {id}");
            var result = await _appDataService.Orders.GetOrder(id);
            if(result == null)
                throw new InvalidRequestException($"{nameof(GetOrder)}: The order id: {id} not found");

            var order = new OrderResponseDto
            {
                Id = result.Id,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
                Description = result.Description,
                OrderNumber = result.OrderNumber,
                Status = result.Status,
                Products = result.OrderProduct.Select(s => new OrderProductResponseDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                    UpdatedAt = s.UpdatedAt,
                    CreatedAt = s.CreatedAt
                }).ToList()
            };
            _logger.LogInformation($"{nameof(GetOrder)}: Got order whith id: {id}");
            return new ApiResponseDto<OrderResponseDto>(order);
        }

        private async Task ValidateProductStock(List<OrderProductRequestDto> orderProduct)
        {
            foreach (var item in orderProduct)
            {
                var product = await _productService.GetProduct(item.ProductId);

                if (product.Result.Stock >= item.Quantity)
                {
                    return;
                }
                throw new InvalidRequestException($"{nameof(ValidateProductStock)}: The stock for product id: {item.ProductId} is not avaliable", AppMessageType.ApiNotFound);
            }
         
        }
    }
}
