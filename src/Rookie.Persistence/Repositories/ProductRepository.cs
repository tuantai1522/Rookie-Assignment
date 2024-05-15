using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Products.Extensions;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Product>> GetAll(ProductParams productParams, string includeProperties = null)
        {
            //Sort, Search, and Filter
            var productList = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.KeyWord)
                .Filter(productParams.CategoryType)
                .Include("Category")
                .AsQueryable();

            //Pagination
            var products = await PagedList<Product>.ToPagedList(productList, productParams.PageNumber,
                                            productParams.PageSize);

            return products.ToList();
        }

        public async Task<Product> GetOne(Expression<Func<Product, bool>> filter, string includeProperties = null)
        {
            IQueryable<Product> query = this._context.Products;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Product entity)
        {
            Product prod = await this._context.Products.Where(x => x.Id.Equals(entity.Id)).FirstOrDefaultAsync();
            if (prod != null)
            {
                prod.ProductName = entity.ProductName;
                prod.Description = entity.Description;
                prod.Price = entity.Price;
                prod.CategoryId = entity.CategoryId;
                prod.Images = entity.Images;

                prod.UpdatedDate = DateTime.Now;


                await this._context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}