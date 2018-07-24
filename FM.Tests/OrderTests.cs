using System;
using System.Collections.Generic;
using FM.BLL;
using FM.Models;
using FM.Models.Responses;
using NUnit.Framework;

namespace FM.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void counter()
        {
            OrderManager manager = OrderFactory.Create();
            Order order = new Order();
            int test = manager.Counter(order);
            Assert.IsNotNull(test);
            Assert.AreEqual(test, 1);
        }
        [Test]
        public void CheckAdd()
        {
            OrderManager manager = OrderFactory.Create();
            Order order = new Order
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
            manager.AddOrder(order);
            OrderLookupResponse response = manager.LookupOrder(order.OrderNumber, order.OrderDate);

            Assert.IsNotNull(response.Order);
            Assert.AreEqual(response.Order.OrderNumber, order.OrderNumber);
            Assert.AreEqual(response.Order.CustomerName, order.CustomerName);
            Assert.AreEqual(response.Order.State, order.State);
            Assert.AreEqual(response.Order.TaxRate, order.TaxRate);
            Assert.AreEqual(response.Order.ProductType, order.ProductType);
            Assert.AreEqual(response.Order.Area, order.Area);
            Assert.AreEqual(response.Order.CostPerSquareFoot, order.CostPerSquareFoot);
            Assert.AreEqual(response.Order.LaborCostPerSquareFoot, order.LaborCostPerSquareFoot);
            Assert.AreEqual(response.Order.OrderDate, order.OrderDate);
        }
        [Test]
        public void CheckEdit()
        {
            OrderManager manager = OrderFactory.Create();
            Order order = new Order
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
            manager.EditOrder(order);
            OrderLookupResponse response = manager.LookupOrder(order.OrderNumber, order.OrderDate);

            Assert.IsNotNull(response.Order);
            Assert.AreEqual(response.Order.CustomerName, order.CustomerName);
            Assert.AreEqual(response.Order.State, order.State);
            Assert.AreEqual(response.Order.TaxRate, order.TaxRate);
            Assert.AreEqual(response.Order.ProductType, order.ProductType);
            Assert.AreEqual(response.Order.Area, order.Area);
            Assert.AreEqual(response.Order.CostPerSquareFoot, order.CostPerSquareFoot);
            Assert.AreEqual(response.Order.LaborCostPerSquareFoot, order.LaborCostPerSquareFoot);
            Assert.AreEqual(response.Order.OrderDate, order.OrderDate);

        }
        [Test]
        public void CheckRemove()
        {
            OrderManager manager = OrderFactory.Create();
            Order order = new Order
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
            manager.RemoveOrder(order);
            OrderLookupResponse response = manager.LookupOrder(order.OrderNumber, order.OrderDate);

            Assert.AreEqual(response.Success, false);
        }
        [Test]
        public void CheckOrder()
        {
            OrderManager manager = OrderFactory.Create();
            Order order = new Order
            {
                OrderNumber = 113,
                CustomerName = "Philip Chang",
                State = "New York",
                TaxRate = 8,
                ProductType = "Wood",
                Area = 900,
                CostPerSquareFoot = 5,
                LaborCostPerSquareFoot = 10,
                OrderDate = new DateTime(2018, 05, 22)
            };
            OrderLookupResponse response = manager.LookupOrder(order.OrderNumber,order.OrderDate);
            Assert.AreEqual(response.Success, true);
            Assert.IsNotNull(response.Order);
            Assert.AreEqual(response.Order.CustomerName, order.CustomerName);
            Assert.AreEqual(response.Order.State, order.State);
            Assert.AreEqual(response.Order.TaxRate, order.TaxRate);
            Assert.AreEqual(response.Order.ProductType, order.ProductType);
            Assert.AreEqual(response.Order.Area, order.Area);
            Assert.AreEqual(response.Order.CostPerSquareFoot, order.CostPerSquareFoot);
            Assert.AreEqual(response.Order.LaborCostPerSquareFoot, order.LaborCostPerSquareFoot);
            Assert.AreEqual(response.Order.OrderDate, order.OrderDate);

        }
        [Test]
        public void CheckOrders()
        {
            OrderManager manager = OrderFactory.Create();
            Order order = new Order
            {
                OrderNumber = 113,
                CustomerName = "Philip Chang",
                State = "New York",
                TaxRate = 8,
                ProductType = "Wood",
                Area = 900,
                CostPerSquareFoot = 5,
                LaborCostPerSquareFoot = 10,
                OrderDate = new DateTime(2018, 05, 22)
            };

            OrdersLookupResponse responses = manager.LookupOrders(order.OrderDate);

            Assert.AreEqual(responses.Orders.Count, 1);
            Assert.AreEqual(responses.Orders[0].OrderNumber, order.OrderNumber);
            Assert.AreEqual(responses.Orders[0].CustomerName, order.CustomerName);
            Assert.AreEqual(responses.Orders[0].State, order.State);
            Assert.AreEqual(responses.Orders[0].TaxRate, order.TaxRate);
            Assert.AreEqual(responses.Orders[0].ProductType, order.ProductType);
            Assert.AreEqual(responses.Orders[0].Area, order.Area);
            Assert.AreEqual(responses.Orders[0].CostPerSquareFoot, order.CostPerSquareFoot);
            Assert.AreEqual(responses.Orders[0].LaborCostPerSquareFoot, order.LaborCostPerSquareFoot);
            Assert.AreEqual(responses.Orders[0].OrderDate, order.OrderDate);

        }
    }
}
