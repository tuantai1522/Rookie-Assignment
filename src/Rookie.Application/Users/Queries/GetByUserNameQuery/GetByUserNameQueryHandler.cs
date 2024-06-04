using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Queries.GetByUserNameQuery
{
    public class GetByUserNameQueryHandler : IRequestHandler<GetByUserNameQuery, Result<UserLoginVm>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        private readonly IMapper _mapper;
        public GetByUserNameQueryHandler(IUserRepository userRepository, IMapper mapper, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<Result<UserLoginVm>> Handle(GetByUserNameQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByUserNameQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<UserLoginVm>(UserErrors.NotEnoughInfo);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName), "ApplicationUserAddresses,Orders");

            //can not find user
            if (user is null)
                return Result.Failure<UserLoginVm>(UserErrors.NotCorrectInfo);

            var roles = await _userRepository.GetRoles(user);

            var UserLoginVm = _mapper.Map<UserLoginVm>(user);

            UserLoginVm.Token = _tokenGenerator.GenerateToken(
                user.Id,
                user.FirstName,
                user.LastName,
                user.UserName,
                user.Email,
                roles.ToList()
            );

            return UserLoginVm;
        }
    }
}