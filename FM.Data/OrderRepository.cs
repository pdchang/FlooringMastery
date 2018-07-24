using FM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FM.Models.Interfaces;

namespace FM.Data
{
    public class OrderRepository : IOrderRepository
    {
        public string _file = "";
        public List<Order> _order = new List<Order>();

        public OrderRepository(string file)
        {
            _file = file;
            //creates if it doesn't exist
            Directory.CreateDirectory(_file);
            //msdn.microsoft.com/en-us/library/07wt70x2(v=vs.110).aspx
            string[] orders = Directory.GetFiles(_file);


            foreach (string o in orders)
            {
                // Orders_MMDDYYYY.txt
                string dater = Path.GetFileName(o);
                if(dater.Contains(".DS_store")){
                    continue;
                }

                dater = dater.Substring(7);
                int month = int.Parse(dater.Substring(0, 2));
                int day = int.Parse(dater.Substring(2, 2));
                int year = int.Parse(dater.Substring(4, 4));
                DateTime dateFile = new DateTime(year, month, day);

                using (StreamReader sr = new StreamReader(o))
                {
                    string line;
                    sr.ReadLine();//skip header
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order order = new Order();
                        order.OrderDate = dateFile;
                        string[] fields = line.Split('%');

                        int orderNumber;

                        if (int.TryParse(fields[0], out orderNumber))
                        {
                            order.OrderNumber = orderNumber;
                        }
                        else
                        {
                            throw new Exception("Error, order number is not valid int");
                        }

                        order.CustomerName = (fields[1]);
                        order.State = (fields[2]);

                        decimal taxRate;
                        if (decimal.TryParse(fields[3], out taxRate))
                        {
                            order.TaxRate = taxRate;
                        }
                        else
                        {
                            throw new Exception("Error, tax rate is not valid decimal");
                        }

                        order.ProductType = fields[4];

                        decimal area;
                        if (decimal.TryParse(fields[5], out area))
                        {
                            order.Area = area;
                        }
                        else
                        {
                            throw new Exception("Error, area is not valid decimal");
                        }

                        decimal costPer;
                        if (decimal.TryParse(fields[6], out costPer))
                        {
                            order.CostPerSquareFoot = costPer;
                        }
                        else
                        {
                            throw new Exception("Error, cost per is not valid decimal");
                        }

                        decimal laborCostPer;
                        if (decimal.TryParse(fields[7], out laborCostPer))
                        {
                            order.LaborCostPerSquareFoot = laborCostPer;
                        }
                        else
                        {
                            throw new Exception("Error, labor cost per is not valid decimal");
                        }



                        _order.Add(order);
                    }
                }

            }
        }

        public void AddOrder(Order order)
        {
            List<Order> orders = GetOrders(order.OrderDate);

            if (orders == null)//make new list if there is nothing in the list
            {
                orders = new List<Order>();
            }

            int orderNumber1 = 1;
            if (orders.Count() > 0)
            {
                orderNumber1 = orders.Max(o => o.OrderNumber) + 1;
            }
            order.OrderNumber = orderNumber1;
            orders.Add(order);
            SaveOrder(orders, order.OrderDate);
            //return orderNumber1;
        }

        public int Counter(Order order)
        {
            List<Order> orders = GetOrders(order.OrderDate);

            if (orders == null)//make new list if there is nothing in the list
            {
                orders = new List<Order>();
            }

            int orderNumber1 = 1;
            if (orders.Count() > 0)
            {
                orderNumber1 = orders.Max(o => o.OrderNumber) + 1;
            }
            return orderNumber1;
        }

        public void EditOrder(Order order)
        {
            List<Order> orders = ((GetOrders(order.OrderDate)).Where(o => o.OrderNumber != order.OrderNumber)).ToList();
            if (orders == null)
            {
                return;
            }
            orders.Add(order);
            SaveOrder(orders, order.OrderDate);

        }

        public Order GetOrder(int orderNumber, DateTime date)
        {
            Order order = _order.FirstOrDefault(o => (o.OrderNumber == orderNumber) && (o.OrderDate == date));
            return order;
        }

        public List<Order> GetOrders(DateTime date)
        {
            List<Order> orders = _order.Where(o => o.OrderDate == date).ToList();
            return orders;
        }

        public void RemoveOrder(Order order)
        {
            Order orderCheck = GetOrder(order.OrderNumber, order.OrderDate);
            if (orderCheck == null)
            {
                return;
            }
            _order.Remove(order);
            List<Order> ordersUpdate = GetOrders(order.OrderDate);
            SaveOrder(ordersUpdate, order.OrderDate);
        }

        public void SaveOrder(List<Order> orderList, DateTime date)
        {
            string fileName = @"..//Debug/Orders/" + "Orders_" + date.ToString("MMddyyyy") + ".txt";
            string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";

            try
            {
                if (File.Exists(_file))
                    File.Delete(_file);
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(header);
                    foreach (Order o in orderList)
                    {
                        sw.WriteLine($"{o.OrderNumber}%{o.CustomerName}%{o.State}%{o.TaxRate}%{o.ProductType}%{o.Area}%{o.CostPerSquareFoot}%{o.LaborCostPerSquareFoot}%{o.MaterialCost}%{o.LaborCost}%{o.Tax}%{o.Total}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an issue writing out your orders - Data may have been lost");
            }
        }
    }
}
