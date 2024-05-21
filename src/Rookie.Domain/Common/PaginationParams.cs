namespace Rookie.Domain.Common
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        //Mỗi page có 6 record
        private int _pageSize = 4;

        //Nếu value lớn hơn 50 thì set là 50 không thì set là value
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}