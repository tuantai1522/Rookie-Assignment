using MediatR;
using Rookie.Domain.Common;

namespace Rookie.Application.Images.Commands.DeleteImageCommand
{
    public class DeleteImageCommand : IRequest<Result<int>>
    {
        public string ImageId { get; set; }
    }
}