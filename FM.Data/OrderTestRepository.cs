using FM.Models;
using FM.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static List<Order> _orders = new List<Order>{
            new Order{
            OrderNumber = 111,
            CustomerName = "Philip Chang",
            State = "New York",
            TaxRate = 8,
            ProductType = "Wood",
            Area = 900,
            CostPerSquareFoot = 5,
            LaborCostPerSquareFoot = 10,
            OrderDate = new DateTime(2018, 05, 20)
            },
            new Order{
            OrderNumber = 112,
            CustomerName = "Philip Chang",
            State = "New York",
            TaxRate = 8,
            ProductType = "Wood",
            Area = 900,
            CostPerSquareFoot = 5,
            LaborCostPerSquareFoot = 10,
            OrderDate = new DateTime(2018, 05, 21)
            },
            new Order{
            OrderNumber = 113,
            CustomerName = "Philip Chang",
            State = "New York",
            TaxRate = 8,
            ProductType = "Wood",
            Area = 900,
            CostPerSquareFoot = 5,
            LaborCostPerSquareFoot = 10,
            OrderDate = new DateTime(2018, 05, 22)
            },
            new Order{
            OrderNumber = 114,
            CustomerName = "Philip Chang",
            State = "New York",
            TaxRate = 8,
            ProductType = "Wood",
            Area = 900,
            CostPerSquareFoot = 5,
            LaborCostPerSquareFoot = 10,
            OrderDate = new DateTime(2018, 05, 23)
            },
        };
        private static Order _order1 = new Order
        {
            OrderNumber = 211,
            CustomerName = "Swan Johnson",
            State = "New York",
            TaxRate = 8,
            ProductType = "Wood",
            Area = 900,
            CostPerSquareFoot = 5,
            LaborCostPerSquareFoot = 10,
            OrderDate = new DateTime(2018, 05, 24)
        };

        public void AddOrder(Order order)
        {
            _orders.Add(_order1);
            //return 1;
        }

        public int Counter(Order order)
        {
            return 1;
        }

        public void EditOrder(Order order)
        {
            Order order2 = new Order
            {
                OrderNumber = 212,
                CustomerName = "Duck Duck",
                State = "New York",
                TaxRate = 8,
                ProductType = "Wood",
                Area = 900,
                CostPerSquareFoot = 5,
                LaborCostPerSquareFoot = 10,
                OrderDate = new DateTime(2018, 05, 25)
            };
            _orders[_orders.Count-1] = order2;
        }

        public Order GetOrder(int orderNumber, DateTime date)
        {
            return _orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        public List<Order> GetOrders(DateTime date)
        {
            return _orders.Where(o=>o.OrderDate == date).ToList();
        }

        public void RemoveOrder(Order order)
        {
            _orders.Remove(_order1);
        }
    }
}
