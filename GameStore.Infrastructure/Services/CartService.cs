using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CartService(IUnitOfWork unitOfWork,
                       ICurrentUserService currentUserService,
                       IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<CartModel> GetCurrentCartAsync()
    {
        var cart = await GetCurrentCartEntityAsync();

        return _mapper.Map<CartModel>(cart);
    }

    public async Task AddToCartAsync(CreateOrderItemModel itemModel)
    {
        var cart = await GetCurrentCartEntityAsync();

        if (cart is null)
        {
            var userName = _currentUserService.GetUsername();
            var currentUser = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

            cart = new Cart();
            currentUser.Carts.Add(cart);

            _unitOfWork.UserRepository.Update(currentUser);
        }

        var itemToAdd = _mapper.Map<OrderItem>(itemModel);
        var item = cart.Items.FirstOrDefault(i => i.GameId == itemToAdd.GameId);

        if (item is null)
        {
            cart.Items.Add(itemToAdd);
            
        }

        else
        {
            item.Amount += itemToAdd.Amount;
            _unitOfWork.CartRepository.Update(cart);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int itemId)
    {
        var cart = await GetCurrentCartEntityAsync();

        var itemToRemove = cart.Items.FirstOrDefault(i => i.Id == itemId);

        if (itemToRemove is not null)
        {
            cart.Items.Remove(itemToRemove);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCountAsync(int itemId, int count)
    {
        var cart = await GetCurrentCartEntityAsync();

        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            throw new NotFoundException("Specified item was not found in the cart.");
        }
        
        item.Amount += count;

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Cart> GetCurrentCartEntityAsync()
    {
        var userName = _currentUserService.GetUsername();
        var cart = await _unitOfWork.CartRepository.GetCurrentCartAsync(userName);

        return cart;
    }
}