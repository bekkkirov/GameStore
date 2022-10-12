using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using Microsoft.AspNetCore.Http;

namespace GameStore.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
        _mapper = mapper;
    }

    public async Task<UserModel> GetUserInfoAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

        if (user is null)
        {
            throw new NotFoundException("User with specified username doesn't exist.");
        }

        return _mapper.Map<UserModel>(user);
    }

    public async Task<ImageModel> SetProfileImageAsync(string userName, IFormFile image)
    {
        var createdImage = await _imageService.AddAsync(image);

        var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

        if (user is null)
        {
            throw new NotFoundException("User with specified username doesn't exist.");
        }

        if (user.ProfileImage is not null)
        {
            await _imageService.DeleteAsync(user.ProfileImage.PublicId);
        }

        createdImage.UserId = user.Id;

        _unitOfWork.ImageRepository.Update(createdImage);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ImageModel>(createdImage);
    }
}