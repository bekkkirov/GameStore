using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<OrderModel>> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        return Ok(order);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<OrderModel>> AddOrder(CreateOrderModel order)
    {
        var createdOrder = await _orderService.AddOrderAsync(order);

        return CreatedAtAction(nameof(GetById), new { Id = createdOrder.Id }, createdOrder);
    }
}