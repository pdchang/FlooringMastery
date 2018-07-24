using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FM.Data;

namespace FM.BLL
{
    public static class TaxFactory
    {
        public static TaxManager Create()
        {

            //string filepath = @"..\\Debug\Accounts.txt"; 
            //for later
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (mode)
            {
                case "Test":
                    Console.WriteLine("****Test Mode Tax****");
                    return new TaxManager(new TaxTestRepository());
                case "File":
                    return new TaxManager(new TaxRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
