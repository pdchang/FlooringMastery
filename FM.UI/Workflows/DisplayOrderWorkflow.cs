using FM.BLL;
using FM.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.UI.Workflows
{
    class DisplayOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderFactory.Create();
            Console.Clear();
            Console.WriteLine("Lookup an Order");
            Console.WriteLine("-----------------------");

            bool flag1 = true;
            int orderNum = 0;
            while (flag1)
            {
                Console.Write("Enter an order number: ");
                string number = Console.ReadLine();

                if (number == null || number == "")
                {
                    Console.WriteLine("Not a valid Order Number");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    Console.WriteLine("Lookup an Order");
                    Console.WriteLine("-----------------------");
                    continue;
                }
                if(!int.TryParse(number, out orderNum))
                {
                    Console.WriteLine("Not a valid Order Number");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    Console.WriteLine("Lookup an Order");
                    Console.WriteLine("-----------------------");
                    continue;
                }
                else
                {
                    flag1 = false;
                }

            }
            bool flag2 = true;
            DateTime time = new DateTime();
            while (flag2)
            {
                Console.Write("Enter an order date(MM-dd-yyyy): ");
                string date = Console.ReadLine();

                if (date == null || date == "")
                {
                    Console.WriteLine("Not a Valid Date. Try again");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    continue;
                }

                if (!DateTime.TryParse(date, out time))
                {
                    Console.WriteLine("Not a valid Date. Try again");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    continue;
                }
                else
                {
                    flag2 = false;
                }
            }
            OrderLookupResponse response = manager.LookupOrder(orderNum, time);
            if (response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Order);
            }
            else
            {
                Console.WriteLine(response.Message);
            }
            ConsoleIO.PressAnyKey();
            Console.Clear();

            
        }
    }
}
