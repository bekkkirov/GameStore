using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Options;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GameStore.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Cloudinary _cloudinary;

    public ImageService(IUnitOfWork unitOfWork, IOptions<CloudinaryOptions> config)
    {
        _unitOfWork = unitOfWork;
        _cloudinary = new Cloudinary(new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret));
    }

    public async Task<Image> AddAsync(IFormFile file)
    {
        ImageUploadResult uploadResult;

        await using (var stream = file.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                AllowedFormats = new string[] { "png", "jpeg" },
                Transformation = new Transformation().Quality(80)
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        if (uploadResult.Error is not null)
        {
            throw new CloudinaryException($"An error occurred while uploading an image: {uploadResult.Error.Message}");
        }

        var imageToAdd = new Image()
        {
            Url = uploadResult.SecureUrl.AbsoluteUri,
            PublicId = uploadResult.PublicId
        };

        _unitOfWork.ImageRepository.Add(imageToAdd);
        await _unitOfWork.SaveChangesAsync();

        return imageToAdd;
    }

    public async Task DeleteAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var deletionResult = await _cloudinary.DestroyAsync(deleteParams);

        if (deletionResult.Error is not null)
        {
            throw new CloudinaryException($"An error occurred while deleting an image: {deletionResult.Error.Message}");
        }

        await _unitOfWork.ImageRepository.DeleteByPublicIdAsync(publicId);
        await _unitOfWork.SaveChangesAsync();
    }
}