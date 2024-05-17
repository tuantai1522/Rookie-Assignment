using Microsoft.AspNetCore.Http;
using Rookie.Application.Products.ViewModels;

namespace Rookie.Application.Contracts.Infrastructure
{
    public interface IImageService
    {
        Task<ImageDto> AddPhoto(IFormFile file);
        Task<int> DeletePhoto(string publicId);
    }
}