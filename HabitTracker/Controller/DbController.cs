using HabitTracker.Helpers;
using HabitTracker.Models;
using HabitTracker.View;
using System.Data.SQLite;
using System.Globalization;

namespace HabitTracker.Controller;

internal class DbController
{
    public static string ConnectionString { get; private set; } = @"Data Source=habit-Tracker.db";
    public static void CreateTable()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var tableCmd = connection.CreateCommand())//Creates a command to send to DB
            {
                tableCmd.CommandText = //Defines the command string to create a table
                @"CREATE TABLE IF NOT EXISTS habits
                        (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Activity TEXT,
                        Unit TEXT,
                        Amount INTEGER
                        )";

                tableCmd.ExecuteNonQuery();//Executes the command without returning a value. Only telling it to create a table.
            } 
        }
    }
    public static void Insert()
    {

        string date = Helper.GetDateInput();

        string activity = Helper.GetActivityInput();

        string unit = Helper.GetUnitInput();

        int amount = Helper.GetNumberInput("Enter the amount as an integer: (1, 2, 3)." +
                                    "\nType 0 to return to the main menu");

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var tableCmd = connection.CreateCommand())
            {
                tableCmd.CommandText =
               $"INSERT INTO habits(Date, Activity, Unit, Amount) VALUES ('{date}', '{activity}', '{unit}', {amount})";

                tableCmd.ExecuteNonQuery();
            } 
        }
        Console.Clear();
    }

    public static void Update()
    {
        Console.Clear();

        ViewRecords();

        var recordId = Helper.GetNumberInput("\n\nPlease type the Id of the record you'd like to update " +
                                            "or 0 to return to the Main Menu");
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            using (var checkCmd = connection.CreateCommand())
            {
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM habits WHERE Id = {recordId})";
                var checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());//returns 0 for false 1 for true

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nThe following record Id doesnt exist: {recordId}\n\n");
                    connection.Close();
                    Update();
                }
            }
            string date = Helper.GetDateInput();

            string activity = Helper.GetActivityInput();

            string unit = Helper.GetUnitInput();

            var amount = Helper.GetNumberInput("Enter the amount as an integer: (1, 2, 3)." +
                                    "\nType 0 to return to the main menu");

            using (var tableCmd = connection.CreateCommand())
            {
                tableCmd.CommandText = $"UPDATE habits SET date = '{date}', Activity = '{activity}', Unit = '{unit}', Amount = {amount}" +
                                  $" WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();
            }
        }
    }

    public static void Delete()
    {
        Console.Clear();
        ViewRecords();

        var recordId = Helper.GetNumberInput("\n\nPlease type the Id of the record you'd like to delete " +
                                            "or 0 to return to the Main Menu");

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var tableCmd = connection.CreateCommand())
            {
                tableCmd.CommandText = $"DELETE from habits WHERE Id = '{recordId}'";

                var rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nThe record you entered does not exist: {recordId}. Press Enter to continue.\n\n");
                    Console.ReadLine();

                    Delete();
                }
            }
        }
        Console.WriteLine($"The following record Id was deleted: {recordId}. Press Enter to return to main menu.\n\n");
        Console.ReadLine();

        MainMenu.GetUserInput();
    }
    public static void ViewRecords()
    {

        int counter = 0;
        Console.Clear();
        using (var connection = new SQLiteConnection(DbController.ConnectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            var viewType = Helper.GetViewType();

            switch (viewType.ToLower())
            {
                case "0":
                    Console.Clear();
                    return;

                case "all":
                    tableCmd.CommandText = $"SELECT * FROM habits";
                    break;

                case "activity":
                    Console.WriteLine("\n\nEnter the activity type of the records you want to view: ");
                    var activityRecord = Console.ReadLine();
                    tableCmd.CommandText = $"SELECT * FROM habits WHERE Activity LIKE '%{activityRecord}%'";
                    break;

                case "date":
                    tableCmd.CommandText = Helper.GetDateViewType();
                    break;
            }

            var tableData = new List<Habit>();
            var amountData = new List<Int32>();

            SQLiteDataReader reader = tableCmd.ExecuteReader();

            //Add specified records to list of Habits
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableData.Add(
                        new Habit
                        {
                            Id = reader.GetInt32(0), //returns values of column(i) specified
                            Date = DateTime.ParseExact(reader.GetString(1), "MM-dd-yyyy", new CultureInfo("en-US")),
                            Activity = reader.GetString(2),
                            Unit = reader.GetString(3),
                            Amount = reader.GetInt32(4)
                        });
                    amountData.Add(reader.GetInt32(4));
                    counter++;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\nNo records found.");
            }

            connection.Close();

            // Add up elements in sumData list
            var summedAmount = amountData.Sum();


            //Print records in list
            Console.WriteLine("===================================================================================");
            foreach (var ex in tableData)
            {
                Console.WriteLine(@$"ID: {ex.Id}  ||  Date: {ex.Date.ToString("MM-dd-yyyy")}  ||  Activity: {ex.Activity}  ||  Units: {ex.Amount}/{ex.Unit}");
            }
            Console.WriteLine("===================================================================================");

            Console.WriteLine($"\nTotal Units for specified records: {summedAmount}");
        }
        Console.WriteLine($"Total Entries: {counter}");
        Console.WriteLine("=======================================");
    }
}
