using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM.BLL;
using FM.Models;
using FM.Models.Responses;

namespace FM.UI.Workflows
{
    class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderFactory.Create();
            Console.Clear();
            Console.WriteLine("Edit an Order");
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
            if (response.Success)//if it successful then we can edit it
            {
                ConsoleIO.DisplayOrderDetails(response.Order);
                ConsoleIO.PressAnyKey();
                Console.Clear();

                AskName(response);
                ConsoleIO.PressAnyKey();
                Console.Clear();

                AskState(response);
                ConsoleIO.PressAnyKey();
                Console.Clear();

                AskProduct(response);
                ConsoleIO.PressAnyKey();
                Console.Clear();

                AskArea(response);
                ConsoleIO.PressAnyKey();
                Console.Clear();

                Console.WriteLine("Updated Order");
                ConsoleIO.DisplayOrderDetails(response.Order);
                bool change = AskConfirmation();
                if (change){
                    manager.EditOrder(response.Order);
                }
                else{
                    Console.WriteLine("No Changes Will Be Made");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(response.Message);
                ConsoleIO.PressAnyKey();
                return;
            }
            //ConsoleIO.DisplayOrderDetails(response.Order);

        }

        public bool AskConfirmation()
        {
            while(true){
                Console.Write("Confirm Changes? Y or N? ");
                string input = (Console.ReadLine()).ToUpper();
                if (input == "Y")
                {
                    return true;
                }
                else if (input == "N"){
                    return false;
                }
                else{
                    Console.WriteLine("Not a valid choice. Please Enter Y or N.");
                    ConsoleIO.PressAnyKey();
                    continue;
                }

            }

        }

        public void AskArea(OrderLookupResponse response)
        {
            while(true){
                Console.Write($"Enter Area ({response.Order.Area}): ");
                string number = Console.ReadLine();
                decimal actual;//hold the out
                if(decimal.TryParse(number, out actual)){
                    if(actual >= 100){
                        response.Order.Area = actual;
                        ConsoleIO.DisplayOrderDetails(response.Order);
                        break;
                    }
                    else{
                        Console.WriteLine("Area Must Be Positive and >= 100");
                        ConsoleIO.PressAnyKey();
                        Console.Clear();
                        continue;
                    }
                }
                else if(String.IsNullOrEmpty(number)){
                    ConsoleIO.DisplayOrderDetails(response.Order);
                    break;
                }
                else{
                    Console.WriteLine("Not a Valid Number");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    continue;
                }
            }
        }

        public void AskProduct(OrderLookupResponse response)
        {
            ProductManager productManager = ProductFactory.Create();

            while(true){
                Console.Write($"Enter Product ({response.Order.ProductType}): ");
                string product = Console.ReadLine();
                ProductResponse productResponse = productManager.ProductInfo(product);
                if (String.IsNullOrEmpty(product))
                {
                    ConsoleIO.DisplayOrderDetails(response.Order);
                    return;
                }
                else{
                    if(productResponse.Success){
                        response.Order.ProductType = productResponse.Product.ProductType;
                        response.Order.LaborCostPerSquareFoot = productResponse.Product.LaborCostPerSquareFoot;
                        response.Order.CostPerSquareFoot = productResponse.Product.CostPerSquareFoot;
                        ConsoleIO.DisplayOrderDetails(response.Order);
                        break;
                    }
                    else{
                        Console.WriteLine(productResponse.Message);
                        ConsoleIO.PressAnyKey();
                        continue;
                    }
                }
            }
        }

        public void AskState(OrderLookupResponse response)
        {
            TaxManager taxManager = TaxFactory.Create();

            while (true)
            {
                Console.Clear();
                Console.Write($"Enter State ({response.Order.State}): ");
                string state = (Console.ReadLine()).ToUpper();
                TaxResponse taxResponse = taxManager.TaxRate(state);
                if (string.IsNullOrEmpty(state))
                {
                    ConsoleIO.DisplayOrderDetails(response.Order);
                    return;
                }
                else
                {
                    if (taxResponse.Success)
                    {
                        response.Order.State = taxResponse.Tax.StateAbbreviation;//set state
                        response.Order.TaxRate = taxResponse.Tax.TaxRate;//make sure the new tax goes through
                        ConsoleIO.DisplayOrderDetails(response.Order);
                        break;
                    }
                    else
                    {
                        Console.WriteLine(taxResponse.Message);
                        ConsoleIO.PressAnyKey();
                        continue;
                        //Console.Clear();
                    }
                }
            }

        }
        //will change name if good, will change name if nothing or null
        public void AskName(OrderLookupResponse response)
        {
            Console.Write($"Enter customer name ({response.Order.CustomerName}): ");
            string input = Console.ReadLine();
            if (!String.IsNullOrEmpty(input))
            {
                response.Order.CustomerName = input;
                ConsoleIO.DisplayOrderDetails(response.Order);
            }
            else
            {
                ConsoleIO.DisplayOrderDetails(response.Order);
                //ConsoleIO.PressAnyKey();
            }

        }
    }
}
