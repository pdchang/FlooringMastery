using FM.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.BLL
{
    public static class OrderFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    Console.WriteLine("****Test Mode Order****");
                    return new OrderManager(new OrderTestRepository());
                case "File":
                    string file = ConfigurationManager.AppSettings["RepositoryLocation"].ToString();
                    return new OrderManager(new OrderRepository(file));
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
