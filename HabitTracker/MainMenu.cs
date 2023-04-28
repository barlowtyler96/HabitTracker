using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTracker
{
    internal class MainMenu
    {
        public static void GetUserInput()
        {

            Console.Clear();
            var closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?\n");
                Console.WriteLine("===========================");
                Console.WriteLine("Type E to Exit");
                Console.WriteLine("Type V to View All Records");
                Console.WriteLine("Type 1 to Insert a Record");
                Console.WriteLine("Type 2 to Delete a Record");
                Console.WriteLine("Type 3 to Update a Record");
                Console.WriteLine("===========================\n");

                var commandInput = Console.ReadLine();

                switch (commandInput.ToLower())
                {
                    case "e":
                        Console.WriteLine("\nGoodbye!");
                        closeApp = true;
                        break;
                    case "v":
                        DbManager.ViewAllRecords();
                        break;
                    case "1":
                        DbManager.Insert();
                        break;
                    case "2":
                        DbManager.Delete();
                        break;
                    case "3":
                        DbManager.Update();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                        break;
                }
            }
        }
    }
}