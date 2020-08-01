using Store.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Store.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProducts()
        {
            return x => x.Active;
        }

        public static Expression<Func<Product, bool>> GetInactiveProducts()
        {
            return x => !x.Active;
        }
    }
}
