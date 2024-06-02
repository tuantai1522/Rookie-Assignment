using AutoMapper;
using MediatR;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Orders.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<PagedList<OrderVm>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }
        public async Task<Result<PagedList<OrderVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetListQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<PagedList<OrderVm>>(OrderErrors.QueryOrderInvalidData);

            var orders = await _orderRepository.GetAll(request.OrderParams, includeProperties: "ApplicationUser,OrderItems");

            var productVms = _mapper.Map<PagedList<OrderVm>>(orders);

            return Result.Success(productVms);
        }
    }
}