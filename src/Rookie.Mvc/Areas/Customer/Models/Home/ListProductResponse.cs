namespace Rookie.Mvc.Areas.Customer.Models.Home
{
    public class ListProductResponse
    {
        public IEnumerable<ProductVm> ProductVms { get; set; }
        public int CurPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
    }
}