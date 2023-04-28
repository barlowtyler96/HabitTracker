using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTracker
{
    internal static class Helpers
    {
        public static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string numberInput = Console.ReadLine();

            if (numberInput == "0")
                MainMenu.GetUserInput();

            while (Int32.TryParse(numberInput, out _) == false || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("Invalid number. Try again.");
                numberInput = Console.ReadLine();
            }

            var finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }



        public static string GetDateInput()
        {
            Console.WriteLine("Please enter the date: (mm-dd-yy). Type 0 to return to the Main Menu.");
            var dateInput = Console.ReadLine();

            if (dateInput == "0")
                MainMenu.GetUserInput();

            while (!DateTime.TryParseExact(dateInput, "MM-dd-yy", new CultureInfo("en-us"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid date. Enter a date in the following format: (mm-dd-yy)" +
                                  " or type 0 to return to the Main Menu.");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }
    }
}