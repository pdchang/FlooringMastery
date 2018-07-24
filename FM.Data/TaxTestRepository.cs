using FM.Models;
using FM.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Data
{
    public class TaxTestRepository : ITaxRepository
    {
        private static Tax _tax = new Tax
        {
            StateName = "New York",
            StateAbbreviation = "NY",
            TaxRate = 8.0M,
        };
        public Tax GetTax(string state)
        {
            return _tax;
        }

        public List<Tax> GetTaxes()
        {
            List<Tax> taxes = new List<Tax>();
            taxes.Add(_tax);
            return taxes;
        }
    }
}
