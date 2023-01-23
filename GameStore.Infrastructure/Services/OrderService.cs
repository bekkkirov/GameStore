using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<OrderModel> GetByIdAsync(int id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdWithItemsAsync(id);

        return _mapper.Map<OrderModel>(order);
    }

    public async Task<OrderModel> AddOrderAsync(CreateOrderModel order)
    {
        var userName = _currentUserService.GetUsername();
        var currentUser = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

        var cart = await _unitOfWork.CartRepository.GetCurrentCartAsync(userName);

        var orderToAdd = _mapper.Map<Order>(order);
        orderToAdd.UserId = currentUser.Id;
        orderToAdd.CartId = cart.Id;

        cart.IsOrdered = true;

        _unitOfWork.OrderRepository.Add(orderToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<OrderModel>(orderToAdd);
    }
}