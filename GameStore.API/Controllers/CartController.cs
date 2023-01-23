using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<ActionResult<CartModel>> GetCart()
    {
        var cart = await _cartService.GetCurrentCartAsync();

        return Ok(cart);
    }

    [HttpPost("add")]
    public async Task<ActionResult<CartModel>> AddToCart(CreateOrderItemModel item)
    {
        await _cartService.AddToCartAsync(item);

        return Ok();
    }

    [HttpPut("{itemId}/increase")]
    public async Task<ActionResult> IncreaseCount(int itemId)
    {
        await _cartService.UpdateCountAsync(itemId, 1);

        return Ok();
    }
    
    [HttpPut("{itemId}/decrease")]
    public async Task<ActionResult> DecreaseCount(int itemId)
    {
        await _cartService.UpdateCountAsync(itemId, -1);

        return Ok();
    }

    [HttpDelete("delete/{itemId}")]
    public async Task<ActionResult> DeleteItem(int itemId)
    {
        await _cartService.RemoveFromCartAsync(itemId);

        return NoContent();
    }
}