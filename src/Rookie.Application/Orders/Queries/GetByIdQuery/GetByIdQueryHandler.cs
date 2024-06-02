using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Queries.GetByIdQuery
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<OrderVm>>
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
        public async Task<Result<OrderVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByIdQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<OrderVm>(OrderErrors.NotProvidingId);

            var user = await _userRepository.GetOne(u => u.UserName.Equals(request.UserName), "Orders");
            if (user == null)
                return Result.Failure<OrderVm>(OrderErrors.NotFindUser);

            var order = await _orderRepository.GetOne(x => x.Id.Equals(new OrderId(request.OrderId)), includeProperties: "ApplicationUser,OrderItems");

            //check whether this order belongs to this user or not
            bool check = false;
            foreach (var item in user.Orders)
            {
                if (item.Id.Equals(order.Id))
                {
                    check = true;
                    break;
                }
            }

            if (order != null && check == true)
                return _mapper.Map<Order, OrderVm>(order);
            else
                return Result.Failure<OrderVm>(OrderErrors.NotFindOrder);
        }
    }
}