using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Models
{
    public class Order
    {
        public int OrderNumber
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public decimal TaxRate
        {
            get;
            set;
        }
        public string ProductType
        {
            get;
            set;
        }
        public decimal Area
        {
            get;
            set;
        }
        public decimal CostPerSquareFoot
        {
            get;
            set;
        }
        public decimal LaborCostPerSquareFoot
        {
            get;
            set;
        }
        //extra date time property for the dates
        public DateTime OrderDate
        {
            get;
            set;
        }
        //materialcost - decimal - area * cost per square foot
        public decimal MaterialCost
        {
            get => (Area * CostPerSquareFoot);
           // set => MaterialCost = value;
        }
        //laborcost - area * labor cost per square foot
        public decimal LaborCost
        {
            get => (Area * LaborCostPerSquareFoot);
            //set => LaborCost = value;
        }
        //tax = (material cost + labor cost) * (tax rate/  100) (tax rates stored as whole number)
        public decimal Tax
        {
            get => ((MaterialCost + LaborCost) * (TaxRate / 100));
            //set => Tax = value;
        }
        //total = materialcost + laborcost + tax
        public decimal Total
        {
            get => (MaterialCost + LaborCost + Tax);
            //set => Total = value;
        }
    }
}
