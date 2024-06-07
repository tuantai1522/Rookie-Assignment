using MediatR;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Addresses.Commands.CreateAddressCommand
{
    public class CreateAddressCommand : IRequest<Result<ApplicationUserAddressVm>>
    {
        public string UserName { get; set; }
        public Address Address { get; set; }
    }
}