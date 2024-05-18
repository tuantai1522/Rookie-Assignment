using MediatR;
using Rookie.Application.MainImages.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.MainImages.Commands.UpdateMainImageCommand
{
    public class UpdateMainImageCommand : IRequest<Result<MainImageVm>>
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }
    }
}