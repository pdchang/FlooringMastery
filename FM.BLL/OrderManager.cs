using FM.Models;
using FM.Models.Interfaces;
using FM.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(int orderNumber, DateTime date)
        {
            OrderLookupResponse response = new OrderLookupResponse();
            response.Order = _orderRepository.GetOrder(orderNumber, date);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Order Number {orderNumber} and/or date ({date.ToString("MM-dd-yyyy")}) is invalid.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public OrdersLookupResponse LookupOrders(DateTime date)
        {
            OrdersLookupResponse response = new OrdersLookupResponse();
            response.Orders = _orderRepository.GetOrders(date);

            if (response.Orders == null)
            {
                response.Success = false;
                response.Message = $"Orders Date of ({ date.ToString("MM-dd-yyyy")}) is invalid.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        //Order AddOrder(Order order);
        //Order EditOrder(Order order);
        //Order RemoveOrder(Order order);
        public void AddOrder(Order order)
        {
            _orderRepository.AddOrder(order);
        }

        public void EditOrder(Order order)
        {
            _orderRepository.EditOrder(order);
        }

        public void RemoveOrder(Order order)
        {
            _orderRepository.RemoveOrder(order);
        }
        public int Counter(Order order){
            return _orderRepository.Counter(order);
        }
    }
}
