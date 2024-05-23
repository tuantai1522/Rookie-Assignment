using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Queries.GetByEmailQuery
{
    public class GetByEmailQueryHandler : IRequestHandler<GetByEmailQuery, Result<UserLoginVm>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public GetByEmailQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<Result<UserLoginVm>> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByEmailQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<UserLoginVm>(UserErrors.NotEnoughInfo);

            var user = await _userManager.Users
                .Include(x => x.ApplicationUserAddresses)
                .FirstOrDefaultAsync(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null)
                return Result.Failure<UserLoginVm>(UserErrors.NotCorrectInfo);


            var UserLoginVm = _mapper.Map<UserLoginVm>(user);
            UserLoginVm.Token = await _tokenService.GenerateToken(user);

            return UserLoginVm;

        }
    }
}