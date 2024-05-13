using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Categories.ViewModels;
using Rookie.Application.Interface;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    //to define fields or properperties needed to handle this GetByIdQuery
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CategoryVm>
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;
        public GetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CategoryVm> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByIdQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                throw new Exception("Error when fetching data");

            var category = await _context.Categories.Where(x => x.Id.Equals(request.Id)).FirstOrDefaultAsync();

            // map data from Course to CourseVm wich is defined in Mappers
            return _mapper.Map<Category, CategoryVm>(category);

        }
    }
}