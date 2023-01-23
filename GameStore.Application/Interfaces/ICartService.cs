using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface ICartService
{
    Task<CartModel> GetCurrentCartAsync();

    Task AddToCartAsync(CreateOrderItemModel itemModel);

    Task UpdateCountAsync(int itemId, int count);

    Task RemoveFromCartAsync(int itemId);
}