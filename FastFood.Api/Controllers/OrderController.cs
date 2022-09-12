using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Orders.Request;
using FastFood.Domain.Dto.Orders.Response;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FastFood.Api.Controllers
{

    public class OrderController : BaseController<OrderController>
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService) : base(logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        /// <summary>
        /// Create new Order
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Returns the order created.</response>
        /// <response code="404">If the order does not exist</response>
        /// <returns>the created order</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAsync(OrderRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateAsync)}: save order");
            var response = await _orderService.CreateOrder(dto);
            _logger.LogInformation($"{nameof(CreateAsync)}: order saved");
            return Ok(response);
        }


        /// <summary>
        /// Get all orders
        /// </summary>
        /// <response code="200">Returns the all orders</response>
        /// <response code="404">If the order does not exist</response>
        /// <returns>the all orders</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderStatusType? status)
        {
            _logger.LogInformation($"{nameof(GetAllOrders)}: Getting all orders");
            var response = await _orderService.GetAllOrders(status);
            _logger.LogInformation($"{nameof(GetAllOrders)}: order saved");
            return Ok(response);
        }

        /// <summary>
        /// Change status order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusType"></param>
        /// <response code="200">Returns the update order</response>
        /// <response code="404">If the order does not exist</response>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ChangeStatus(long id,[FromBody] OrderStatusType statusType)
        {
            _logger.LogInformation($"{nameof(GetAllOrders)}: Changing status");
            var response = await _orderService.ChangeStatus(id,statusType);
            _logger.LogInformation($"{nameof(GetAllOrders)}: status has been changed");
            return Ok(response);
        }
    }
}
