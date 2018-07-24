using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Models.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(string productType);
        List<Product> GetProducts();
    }
}
