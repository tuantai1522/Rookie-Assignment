using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Users.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<PagedList<UserInfoVm>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<Result<PagedList<UserInfoVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<PagedList<UserInfoVm>>(UserErrors.QueryUserInvalidData);

            var users = await _userRepository.GetAll(request.ApplicationUserParams, "Orders");

            var userVms = _mapper.Map<PagedList<UserInfoVm>>(users);

            return Result.Success(userVms);
        }
    }
}