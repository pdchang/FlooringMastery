using FM.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.BLL
{
    public static class ProductFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    Console.WriteLine("****Test Mode Product****");
                    return new ProductManager(new ProductTestRepository());
                case "File":
                    return new ProductManager(new ProductRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
