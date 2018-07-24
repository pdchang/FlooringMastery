using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM.BLL;
using FM.Models.Responses;

namespace FM.UI.Workflows
{
    class RemoveOrderWorkflow
    {
        //For removing an order, the system should ask for the date and order number. If it 
        //exists, the system should display the order information and prompt the user 
        //if they are sure. If yes, the order should be removed from the file. If no, return 
        //the user to the main menu.
        public void Execute()
        {
            OrderManager manager = OrderFactory.Create();


            bool flag1 = true;
            int orderNum = 0;
            while (flag1)
            {
                Console.Clear();
                Console.WriteLine("Remove an Order");
                Console.WriteLine("-----------------------");
                
                Console.Write("Enter an order number: ");
                string number = Console.ReadLine();

                if (number == null || number == "")
                {
                    Console.WriteLine("Not a valid Order Number");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    Console.WriteLine("Remove an Order");
                    Console.WriteLine("-----------------------");
                    continue;
                }
                if (!int.TryParse(number, out orderNum))
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
                Console.Clear();
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
                ConsoleIO.PressAnyKey();
                return;
            }
            Console.Write("Would you like to remove this order? Y or N? ");
            string yesNo = Console.ReadLine();

            while (true)
            {
                if (yesNo.ToUpper() == "Y")
                {
                    manager.RemoveOrder(response.Order);
                    Console.WriteLine($"{response.Order.OrderNumber} has been removed");
                    ConsoleIO.PressAnyKey();
                    break;

                }
                else if (yesNo.ToUpper() == "N")
                {

                    return;
                }
            }

        }
    }
}
