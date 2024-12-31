using CleverCore.Data.Entities;
using CleverCore.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverCore.Data.EF.Repositories
{
    public class ProductCategoryRepository : EFRepository<ProductCategory, int>, IProductCategoryRepository
    {
        private AppDbContext _context;

        public ProductCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductCategory> GetByAlias(string alias)
        {
            return _context.ProductCategories.Where(x => x.SeoAlias == alias).ToList();
        }
    }
}