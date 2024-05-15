using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string OrderBy)
        {
            if (string.IsNullOrWhiteSpace(OrderBy)) return query.OrderBy(p => p.ProductName);

            query = OrderBy switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.ProductName)
            };

            return query;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> query, string KeyWord)
        {
            if (string.IsNullOrEmpty(KeyWord)) return query;

            //Chuẩn hóa chuỗi
            string lowerCaseSearchKeyWord = KeyWord.Trim().ToLower();

            query = query.Where(p => p.ProductName.ToLower().Contains(lowerCaseSearchKeyWord));

            return query;
        }

        public static IQueryable<Product> Filter(this IQueryable<Product> query, string CategoryName)
        {
            var CategoryTypeList = new List<string>();

            if (!string.IsNullOrEmpty(CategoryName))
                CategoryTypeList.AddRange(CategoryName.Split(",").ToList());

            query = query.Where(p => CategoryTypeList.Count == 0 || CategoryTypeList.Contains(p.Category.Name.ToString()));

            return query;
        }
    }
}