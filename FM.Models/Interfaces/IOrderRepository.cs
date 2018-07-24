using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(DateTime date);
        Order GetOrder(int orderNumber, DateTime date);
        void AddOrder(Order order);
        void EditOrder(Order order);
        void RemoveOrder(Order order);
        int Counter(Order order);
    }
}
