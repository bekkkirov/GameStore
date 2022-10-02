using GameStore.Application.Models;
using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Interfaces;

public interface IImageService
{
    Task<Image> AddAsync(IFormFile file);

    Task DeleteAsync(string publicId);
}