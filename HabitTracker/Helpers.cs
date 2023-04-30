using System.Globalization;

namespace HabitTracker
{
    internal static class Helpers
    {


  

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            var numberInput = Console.ReadLine();

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




        internal static string GetDateInput()
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




        internal static string GetActivityInput()
        {
            Console.WriteLine("Enter the activity you would like to track or type 0 to return to the Main Menu");
            var activityInput = Console.ReadLine();

            if(activityInput == "0")
            {
                MainMenu.GetUserInput();
            }
            else if (String.IsNullOrEmpty(activityInput))
            {
                Console.WriteLine("Invalid input.");
            }

            //Make sure input is string
            return activityInput;
        }



        internal static string GetUnitInput()
        {
            Console.WriteLine("Enter the unit of measurement you would like to use (ex: miles, cups, laps, hours)" +
                              " or type 0 to return to the Main Menu");
            var unitInput = Console.ReadLine();

            if (unitInput == "0")
            {
                MainMenu.GetUserInput();
            }
            else if (String.IsNullOrEmpty(unitInput))
            {
                Console.WriteLine("Invalid input.");
            }

            //Make sure input is string

            return unitInput;

        }

        internal static string GetViewType()
        {
            Console.WriteLine("Type 'all' to view all records, 'activity' to view by activity type, or 'date' to view by date.\n" +
                              "Type 0 to return to the Main Menu.\n\n");
            var viewInput = Console.ReadLine();

            if (viewInput == "0")
                MainMenu.GetUserInput();

            else if (viewInput != "all" && viewInput != "activity" && viewInput != "date")
            {
                Console.Clear();
                Console.WriteLine("\n\nInvalid command. Review the options and try again.\n");
                GetViewType();
            }

            return viewInput;
        }



        internal static string GetDateViewType()
        {
            Console.WriteLine("Type 'specific' to view a record from a specific date, or type range to view records within a date range\n" +
                              "Type 0 to return to the Main Menu\n\n");
            var dateViewType = Console.ReadLine();

            string date = "";
            string tableCmd = "";


            switch (dateViewType)
            {
                case "0":
                    MainMenu.GetUserInput();
                    break;

                case "specific":
                    date = Helpers.GetDateInput();
                    tableCmd = $"SELECT * FROM habits WHERE Date = '{date}'";
                    break;

                case "range":
                    Console.WriteLine("Enter a year to view all records from that year: (eg. 23)");
                    date = Console.ReadLine();
                    tableCmd = $"SELECT * FROM habits WHERE Date LIKE '%{date}%'";
                    break;

                default:
                    Console.WriteLine("\n\nInvalid command. Review the options and try again.\n");
                    break;
            }

            return tableCmd;
        }
    }
}