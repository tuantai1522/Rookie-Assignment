using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery
{
    public class GetAddressByUserNameQueryHandler : IRequestHandler<GetAddressByUserNameQuery, Result<ICollection<UserAddressVm>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAddressByUserNameQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<ICollection<UserAddressVm>>> Handle(GetAddressByUserNameQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAddressByUserNameValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<ICollection<UserAddressVm>>(UserErrors.NotEnoughInfo);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName), "ApplicationUserAddresses");

            //can not find user
            if (user is null)
                return Result.Failure<ICollection<UserAddressVm>>(UserErrors.NotCorrectInfo);

            var userAddressVms = _mapper.Map<ICollection<UserAddressVm>>(user.ApplicationUserAddresses);

            return Result.Success(userAddressVms);
        }
    }
}