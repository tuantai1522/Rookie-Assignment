using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Commands.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UserLoginVm>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager,
                                   IMapper mapper, IJwtTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<UserLoginVm>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<UserLoginVm>(UserErrors.NotEnoughInfo);


            var user = await _userManager.Users
                .Include(x => x.ApplicationUserAddresses)
                .FirstOrDefaultAsync(u => u.UserName.Equals(request.UserName));

            //can not find user
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure<UserLoginVm>(UserErrors.NotCorrectInfo);


            //find role of current user
            var roles = await _userManager.GetRolesAsync(user);

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