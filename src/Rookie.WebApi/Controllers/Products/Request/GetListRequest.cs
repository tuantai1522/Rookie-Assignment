using Rookie.Domain.ProductEntity;

namespace Rookie.WebApi.Controllers.Products.Request
{
    public sealed record GetListRequest
    {
        public string? OrderBy { get; set; }
        public string? KeyWord { get; set; }
        public string? CategoryType { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}