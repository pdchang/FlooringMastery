using FM.Models;
using FM.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Data
{
    public class ProductTestRepository : IProductRepository
    {
        private static Product _product = new Product
        {
            ProductType = "Wood",
            LaborCostPerSquareFoot = 2.0M,
            CostPerSquareFoot = 1.0M,
        };
        public Product GetProduct(string productType)
        {
            return _product;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(_product);
            return products;
        }
    }
}
