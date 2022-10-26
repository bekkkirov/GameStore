using GameStore.Application.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Interfaces;

public interface IUserService
{
    Task<UserModel> GetUserInfoAsync(string userName);

    Task<ImageModel> SetProfileImageAsync(string userName, IFormFile image);
}