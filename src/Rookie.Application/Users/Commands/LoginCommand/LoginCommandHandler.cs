using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Commands.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UserLoginVm>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            //can not find user
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure<UserLoginVm>(UserErrors.NotCorrectInfo);

            var UserLoginVm = _mapper.Map<UserLoginVm>(user);
            UserLoginVm.Token = "New Token";

            return UserLoginVm;
        }
    }
}