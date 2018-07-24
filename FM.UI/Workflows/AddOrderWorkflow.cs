using System;
using System.Collections.Generic;
using FM.BLL;
using FM.Data;
using FM.Models;
using FM.Models.Responses;

namespace FM.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderFactory.Create();
            Console.Clear();
            Console.WriteLine("Add an Order");
            Console.WriteLine("-----------------------");

            Order order = new Order();


            SetOrderDate(order);
            SetCustomerName(order);
            SetState(order);
            SetProduct(order);
            SetArea(order);

            //display the order
            Console.WriteLine("This is the order you want to add.");
            OrdersLookupResponse responseO = manager.LookupOrders(order.OrderDate);
            int orderNumber1 = manager.Counter(order);


            ConsoleIO.DisplayOrderDetailsOrderNumber(order, orderNumber1);

            bool change = AskConfirmation();
            if (change)
            {
                manager.AddOrder(order);
            }
            else
            {
                Console.WriteLine("Potential order not ordered.");
                ConsoleIO.PressAnyKey();
                Console.Clear();
            }

        }

        public bool AskConfirmation()
        {
            while (true)
            {
                Console.Write("Do you want to add the order? Y or N? ");
                string input = (Console.ReadLine()).ToUpper();
                if (input == "Y")
                {
                    return true;
                }
                else if (input == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Not a valid choice. Please Enter Y or N.");
                    ConsoleIO.PressAnyKey();
                    continue;
                }

            }
        }

        public void SetArea(Order order)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"Enter Area: ");
                string number = Console.ReadLine();
                decimal actual;//hold the out
                if (decimal.TryParse(number, out actual))
                {
                    if (actual >= 100)
                    {
                        order.Area = actual;
                        //ConsoleIO.DisplayOrderDetails(response.Order);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Area Must Be Positive and >= 100");
                        ConsoleIO.PressAnyKey();
                        Console.Clear();
                        continue;
                    }
                }
                else if (String.IsNullOrEmpty(number))
                {
                    Console.WriteLine("You didn't enter anything");
                    ConsoleIO.PressAnyKey();
                    continue;
                }
                else
                {
                    Console.WriteLine("Not a Valid Number");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    continue;
                }
            }
        }

        public void SetProduct(Order order)
        {
            ProductManager productManager = ProductFactory.Create();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Product Choices: ");
                foreach (Product p in productManager.GetProducts())
                {
                    Console.WriteLine($"Type: {p.ProductType} | Cost Per sq ft: {p.CostPerSquareFoot} | Labor Per sq ft: {p.LaborCostPerSquareFoot}");
                }

                Console.Write($"Enter Product: ");
                string product = Console.ReadLine();
                ProductResponse productResponse = productManager.ProductInfo(product);
                if (String.IsNullOrEmpty(product))
                {
                    Console.WriteLine("You didn't enter a product type.");
                    ConsoleIO.PressAnyKey();
                    continue;
                }
                else
                {
                    if (productResponse.Success)
                    {
                        order.ProductType = productResponse.Product.ProductType;
                        order.LaborCostPerSquareFoot = productResponse.Product.LaborCostPerSquareFoot;
                        order.CostPerSquareFoot = productResponse.Product.CostPerSquareFoot;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(productResponse.Message);
                        ConsoleIO.PressAnyKey();
                        continue;
                    }
                }
            }
        }

        public void SetState(Order order)
        {
            TaxManager taxManager = TaxFactory.Create();

            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("State Choices: ");
                foreach(Tax t in taxManager.GetTaxes()){
                    Console.WriteLine($"{t.StateAbbreviation},{t.StateName},Tax Rate: {t.TaxRate}");
                }
                Console.Write($"Enter State: ");

                string state = (Console.ReadLine()).ToUpper();
                TaxResponse taxResponse = taxManager.TaxRate(state);
                if (string.IsNullOrEmpty(state))
                {
                    Console.WriteLine("The entry was null or empty");
                    ConsoleIO.PressAnyKey();
                    continue;
                }
                else
                {
                    if (taxResponse.Success)
                    {
                        order.State = taxResponse.Tax.StateAbbreviation;//set state
                        order.TaxRate = taxResponse.Tax.TaxRate;//make sure the new tax goes through
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

        public void SetCustomerName(Order order)
        {

            while (true)
            {
                Console.Clear();
                Console.Write("Enter your name: ");
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    order.CustomerName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("You Must Enter a Name!");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    continue;
                }
            }
        }

        public void SetOrderDate(Order order)
        {
            while (true)
            {
                Console.Write("Enter Order Date: ");
                string input = Console.ReadLine();
                DateTime date = new DateTime();

                if (!String.IsNullOrEmpty(input))
                {
                    if (DateTime.TryParse(input, out date))
                    {
                        if (DateTime.Now < date){
                            order.OrderDate = date;
                            break;
                        }
                        else{
                            Console.WriteLine("Date must be after current Date");
                            ConsoleIO.PressAnyKey();
                            Console.Clear();
                            continue;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Not a Valid Date");
                        ConsoleIO.PressAnyKey();
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Not a Valid Date");
                    ConsoleIO.PressAnyKey();
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}
