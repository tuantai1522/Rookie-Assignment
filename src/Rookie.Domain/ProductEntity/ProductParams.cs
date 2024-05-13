using Rookie.Domain.Common;

namespace Rookie.Domain.ProductEntity
{
    public class ProductParams : PaginationParams
    {
        public string? OrderBy { get; set; }
        public string? KeyWord { get; set; }
        public string? CategoryType { get; set; }
    }
}