using FM.Models;
using FM.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Data
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> _product = new List<Product>();
        private string _filepath;
        string filepath = @"..//Debug/Products.txt";

        public ProductRepository()
        {
            _filepath = filepath;

            try
            {
                using (StreamReader sr = new StreamReader(_filepath))
                {
                    string line;
                    sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        Product product = new Product();

                        string[] fields = line.Split(',');

                        product.ProductType = fields[0];


                        decimal cost;
                        decimal labor;
                        if (decimal.TryParse(fields[1], out cost))
                        {
                            product.CostPerSquareFoot = cost;
                        }
                        else
                        {
                            Console.WriteLine($"Error: Trying to Parse for cost rate of {product.ProductType}");
                            throw new Exception("Error, the cost rate is not valid");
                            //continue;
                        }
                        if (decimal.TryParse(fields[2], out labor))
                        {
                            product.LaborCostPerSquareFoot = labor;
                        }
                        else
                        {
                            Console.WriteLine($"Error: Trying to Parse for labor rate of {product.ProductType}");
                            throw new Exception("Error, the labor rate is not valid");
                            //continue;
                        }
                        _product.Add(product);

                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The File was not found, Would you like to create an empty one.");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    File.Create(_filepath).Close();
                }
                else
                {
                    throw new Exception("Error: Can't read file and didn't create one",ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an issue opening the file, so the program can't really continue", ex);

            }

        }
        public Product GetProduct(string productType)
        {
            return _product.FirstOrDefault(prod => prod.ProductType.ToUpper() == productType.ToUpper());
        }

        public List<Product> GetProducts()
        {
            return _product;
        }
    }
}
