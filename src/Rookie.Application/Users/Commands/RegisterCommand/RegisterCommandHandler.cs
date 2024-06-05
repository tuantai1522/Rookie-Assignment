using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Commands.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UserRegisterVm>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserRegisterVm>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //data is not valid
            if (validationResult.IsValid == false)
                return Result.Failure<UserRegisterVm>(UserErrors.NotEnoughInfo);

            //This email has already existed
            if (_userRepository.CheckEmailExisted(request.Email))
                return Result.Failure<UserRegisterVm>(UserErrors.EmailExisted);

            //This user name has already existed
            if (_userRepository.CheckUserNameExisted(request.UserName))
                return Result.Failure<UserRegisterVm>(UserErrors.UserNameExisted);


            //add user
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
            };
            var result = await _userRepository.CreateUser(user, request.Password);

            if (!result.Succeeded)
            {
                string temp = string.Join(". ", result.Errors.Select(e => e.Description));
                return Result.Failure<UserRegisterVm>(UserErrors.CreateCustomRegisterError(temp));
            }

            //add role
            if (string.IsNullOrEmpty(request.Role))
                await _userRepository.AddToRole(user, "Customer");
            else
                await _userRepository.AddToRole(user, "Admin");

            return _mapper.Map<UserRegisterVm>(user);
        }
    }
}