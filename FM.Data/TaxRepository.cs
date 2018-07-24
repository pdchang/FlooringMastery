using FM.Models;
using FM.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FM.UI;

namespace FM.Data
{
    public class TaxRepository : ITaxRepository
    {
        public List<Tax> _tax = new List<Tax>();
        private string _filepath;
        string filepath = @"..//Debug/Taxes.txt";

        public TaxRepository()
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
                        Tax tax = new Tax();

                        string[] fields = line.Split(',');

                        tax.StateAbbreviation = fields[0].ToUpper();
                        tax.StateName = fields[1];

                        decimal taxCheck;
                        if (decimal.TryParse(fields[2], out taxCheck))
                        {
                            tax.TaxRate = taxCheck;
                        }
                        else
                        {
                            Console.WriteLine($"Error: Trying to Parse for tax rate state of {tax.StateName}");
                            throw new Exception("Error, the tax rate is not a valid number");
                            //continue;
                        }
                        _tax.Add(tax);
                    }
                }
                //ListTaxes(_tax);
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
                    throw new Exception("Error: Can't read file and didn't create one");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an issue opening the file, so the program can't really continue");

            }
        }
        public Tax GetTax(string state)
        {
            //Tax tax = new Tax();
            //tax.StateAbbreviation = "NY";
            //tax.StateName = "New York";
            //tax.TaxRate = (decimal)(7.00);
            //return tax;
            //ListTaxes(_tax);
            //foreach (Tax tax in _tax){
            //    if (tax.StateAbbreviation == state || tax.StateName == state){
            //        return tax;
            //    }
            //    else{
            //        return null;
            //    }
            //}
            //return null;
            return _tax.FirstOrDefault(t => t.StateAbbreviation == state);
        }

        public List<Tax> GetTaxes(){
            return _tax;
        }
    }
}
