using System;
using FM.Models;

namespace FM.UI
{
    public class ConsoleIO
    {
        public static void PressAnyKey()
        {
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
        //[OrderNumber] | [Order Date] 
        //[Customer Name] 
        //[State] 
        //Product : [ProductType] 
        //Materials: [MaterialCost] 
        //Labor: [LaborCost] 
        //Tax: [Tax] 
        //Total: [Total] 
        public static void DisplayOrderDetails(Order order)
        {
            string display = $"\n{order.OrderNumber} | {order.OrderDate.ToString("MM-dd-yyyy")}\n{order.CustomerName}\n{order.State}\nProduct : {order.ProductType}\nMaterials : {order.MaterialCost:c}\nLabor : {order.LaborCost:c}\nTax : {order.Tax:c}\nTotal : {order.Total:c}";

            Console.WriteLine(display);
        }
        public static void DisplayOrderDetailsOrderNumber(Order order, int orderNumber)
        {
            string display = $"\n{orderNumber} | {order.OrderDate.ToString("MM-dd-yyyy")}\n{order.CustomerName}\n{order.State}\nProduct : {order.ProductType}\nMaterials : {order.MaterialCost:c}\nLabor : {order.LaborCost:c}\nTax : {order.Tax:c}\nTotal : {order.Total:c}";

            Console.WriteLine(display);
        }
    }
}
