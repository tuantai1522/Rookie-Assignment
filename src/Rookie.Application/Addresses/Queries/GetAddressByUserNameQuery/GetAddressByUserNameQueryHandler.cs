using AutoMapper;
using MediatR;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery
{
    public class GetAddressByUserNameQueryHandler : IRequestHandler<GetAddressByUserNameQuery, Result<IEnumerable<ApplicationUserAddressVm>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public GetAddressByUserNameQueryHandler(IUserRepository userRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<ApplicationUserAddressVm>>> Handle(GetAddressByUserNameQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAddressByUserNameValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<IEnumerable<ApplicationUserAddressVm>>(AddressError.NotEnoughInfo);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName), "ApplicationUserAddresses");

            //can not find user
            if (user is null)
                return Result.Failure<IEnumerable<ApplicationUserAddressVm>>(AddressError.NotFindUser);

            var applicationUserAddress = await _addressRepository.GetAll(x => x.ApplicationUser.UserName.Equals(request.UserName), "ApplicationUser");

            return Result.Success(_mapper.Map<IEnumerable<ApplicationUserAddress>, IEnumerable<ApplicationUserAddressVm>>(applicationUserAddress));

        }
    }
}