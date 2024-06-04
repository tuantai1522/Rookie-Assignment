using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Orders.Queries.GetByIdQuery
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<PagedList<OrderVm>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;

        }
        public async Task<Result<PagedList<OrderVm>>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByIdQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<PagedList<OrderVm>>(OrderErrors.NotProvidingId);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName), "Orders");
            if (user == null)
                return Result.Failure<PagedList<OrderVm>>(OrderErrors.NotFindUser);

            var orders = await _orderRepository.GetListById(x => x.ApplicationUser.UserName.Equals(request.UserName),
                                                            request.OrderParams,
                                                            includeProperties: "ApplicationUser,OrderItems");

            var orderVms = _mapper.Map<PagedList<OrderVm>>(orders);

            if (orders != null)
                return Result.Success(orderVms);
            else
                return Result.Failure<PagedList<OrderVm>>(OrderErrors.NotFindOrder);
        }
    }
}