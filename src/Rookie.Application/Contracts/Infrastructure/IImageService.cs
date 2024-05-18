using Microsoft.AspNetCore.Http;
using Rookie.Application.Images.ViewModels;

namespace Rookie.Application.Contracts.Infrastructure
{
    public interface IImageService
    {
        Task<ImageVm> AddPhoto(IFormFile file);
        Task<int> DeletePhoto(string publicId);
    }
}