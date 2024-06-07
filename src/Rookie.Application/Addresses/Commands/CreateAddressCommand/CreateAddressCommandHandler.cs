using AutoMapper;
using MediatR;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Addresses.Commands.CreateAddressCommand
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<ApplicationUserAddressVm>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(IUserRepository userRepository,
                                         IAddressRepository addressRepository,
                                         IMapper mapper)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Result<ApplicationUserAddressVm>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAddressCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<ApplicationUserAddressVm>(AddressError.CreateAddressInvalid);


            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<ApplicationUserAddressVm>(AddressError.NotFindUser);

            var AddressCreated = new ApplicationUserAddress()
            {
                UserId = user.Id,
                Address = request.Address,
            };

            _addressRepository.Add(AddressCreated);


            return Result.Success(_mapper.Map<ApplicationUserAddress, ApplicationUserAddressVm>(AddressCreated));
        }
    }
}