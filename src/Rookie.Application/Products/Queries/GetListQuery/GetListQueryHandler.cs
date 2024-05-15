using AutoMapper;
using MediatR;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, IEnumerable<ProductVm>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<ProductVm>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll(request.ProductParams, includeProperties: "Category");

            // map data from Course to CourseVm wich is defined in Mappers
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductVm>>(products);
        }


    }

}