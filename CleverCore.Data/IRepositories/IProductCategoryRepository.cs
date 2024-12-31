using CleverCore.Data.Entities;
using CleverCore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverCore.Data.IRepositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, int>
    {
        List<ProductCategory> GetByAlias(string alias);
    }
}