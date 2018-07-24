using System;
using FM.UI.Workflows;

namespace FM.UI
{
    public class Menu
    {
        public static void Start()
        {
            string menu = "****************************************************************************\n* Flooring Program\n*\n* 1.Display Orders\n* 2.Add an Order\n* 3.Edit an Order\n* 4.Remove an Order\n* 5.Quit\n*\n****************************************************************************";

            while (true)
            {
                Console.Clear();
                Console.WriteLine(menu);
                Console.Write("\nEnter Selection: ");

                string userinput = Console.ReadLine();
                int selection;

                if (!int.TryParse(userinput, out selection))
                {
                    Console.WriteLine("Error: You did not enter a valid number selection!");
                    ConsoleIO.PressAnyKey();
                    continue;
                }

                switch (selection)
                {
                    case 1://display orders
                        DisplayOrderWorkflow displayOrderWorkflow = new DisplayOrderWorkflow();
                        displayOrderWorkflow.Execute();
                        break;
                    case 2://add an order
                        AddOrderWorkflow addOrderWorkflow = new AddOrderWorkflow();
                        addOrderWorkflow.Execute();
                        break;
                    case 3://edit an order
                        EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
                        editOrderWorkflow.Execute();
                        break;
                    case 4://remove an order
                        RemoveOrderWorkflow removeOrderWorkflow = new RemoveOrderWorkflow();
                        removeOrderWorkflow.Execute();
                        break;
                    case 5://quit
                        return;
                    default:
                        Console.WriteLine("Error: You did not enter a valid selection!");
                        ConsoleIO.PressAnyKey();
                        break;

                }
            }

        }
    }
}
