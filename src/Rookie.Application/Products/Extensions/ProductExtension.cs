using System.Linq.Expressions;
using System.Reflection;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string OrderBy)
        {
            if (string.IsNullOrWhiteSpace(OrderBy)) return query.OrderBy(p => p.ProductName);

            // Parse the orderBy string
            var SortType = OrderBy.EndsWith("asc", StringComparison.OrdinalIgnoreCase);

            string propertyName = string.Empty;

            if (SortType == true)
                propertyName = OrderBy[..^3];//get 3 last characters (asc)
            else
                propertyName = OrderBy[..^4];//get 4 last characters (desc)

            // Get the property to sort by
            var propertyInfo = typeof(Product).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
                return query;

            // Create the sorting expression
            var parameter = Expression.Parameter(typeof(Product), "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // Apply the sorting
            var methodName = SortType ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, [typeof(Product), propertyInfo.PropertyType],
                                                   query.Expression, Expression.Quote(orderByExpression));
            return query.Provider.CreateQuery<Product>(resultExpression);

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