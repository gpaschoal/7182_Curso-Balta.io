using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private readonly IList<Product> _products;

        public ProductQueriesTests()
        {
            _products = new List<Product>
            {
                new Product("Produto 01", 10, true),
                new Product("Produto 02", 20, true),
                new Product("Produto 03", 30, true),
                new Product("Produto 04", 40, false),
                new Product("Produto 05", 50, false),
            };
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.AreEqual(2, result.Count());
        }
    }
}
