using System;
using FM.BLL;
using FM.Models;
using FM.Models.Responses;
using NUnit.Framework;

namespace FM.Tests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        [TestCase("Wood", 2.0, 1.0, true)]//true
        public void ProductGetTest(string productType, decimal labor, decimal cost, bool expected)
        {

            Product product = new Product
            {
                ProductType = productType,
                LaborCostPerSquareFoot = labor,
                CostPerSquareFoot = cost,
            };

            ProductManager manager = ProductFactory.Create();
            ProductResponse response = manager.ProductInfo(productType);

            Assert.AreEqual(response.Product.ProductType, productType);
            Assert.AreEqual(response.Product.LaborCostPerSquareFoot, labor);
            Assert.AreEqual(response.Product.CostPerSquareFoot, cost);

        }
    }
}
