using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Commands.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UserRegisterVm>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Result<UserRegisterVm>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<UserRegisterVm>(UserErrors.NotEnoughInfo);

            if (_userManager.Users.All(u => u.Email != request.Email) == false)
                //This email has already existed
                return Result.Failure<UserRegisterVm>(UserErrors.EmailExisted);

            //create new user
            if (_userManager.Users.All(u => u.UserName != request.UserName))
            {
                var user = new ApplicationUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.UserName,
                };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    string temp = string.Join(". ", result.Errors.Select(e => e.Description));
                    return Result.Failure<UserRegisterVm>(UserErrors.CreateCustomRegisterError(temp));
                }

                return _mapper.Map<UserRegisterVm>(user);
            }

            //This user name has already existed
            return Result.Failure<UserRegisterVm>(UserErrors.UserNameExisted);


        }
    }
}