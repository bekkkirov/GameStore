using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface IOrderService
{
    Task<OrderModel> GetByIdAsync(int id);

    Task<OrderModel> AddOrderAsync(CreateOrderModel order);
}