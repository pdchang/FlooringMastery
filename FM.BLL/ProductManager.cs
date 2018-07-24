using FM.Models;
using FM.Models.Interfaces;
using FM.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.BLL
{
    public class ProductManager
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductResponse ProductInfo(string productType)
        {
            ProductResponse response = new ProductResponse();
            response.Product = _productRepository.GetProduct(productType);
            if (response.Product == null)
            {
                response.Success = false;
                response.Message = $"{productType} is an invalid product type.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            return _productRepository.GetProducts();
        }
    }
}
