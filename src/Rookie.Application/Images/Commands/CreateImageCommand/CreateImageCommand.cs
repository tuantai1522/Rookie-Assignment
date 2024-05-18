using MediatR;
using Microsoft.AspNetCore.Http;
using Rookie.Domain.Common;
using Rookie.Domain.ImageEntity;

namespace Rookie.Application.Images.Commands.CreateImageCommand
{
    public class CreateImageCommand : IRequest<Result<ImageId>>
    {
        public string ProductId { get; set; }
        public IFormFile FileImage { get; set; }
    }
}