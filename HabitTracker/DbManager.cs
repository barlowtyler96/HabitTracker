using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTracker
{
    internal class DbManager
    {


        //Create
        public static void Insert()
        {
            string date = Helpers.GetDateInput();

            int miles = Helpers.GetNumberInput("Enter the amount of miles jogged as an integer: (1, 2, 3)." +
                                        "\nType 0 to return to the main menu");

            using (var conneciton = new SQLiteConnection(Program.ConnectionString))
            {
                conneciton.Open();
                var tableCmd = conneciton.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO jogging(date, miles) VALUES ('{date}', {miles})";

                tableCmd.ExecuteNonQuery();

                conneciton.Close();
            }
            Console.Clear();
        }





        //Read
        public static void ViewAllRecords()
        {
            Console.Clear();
            using (var connection = new SQLiteConnection(Program.ConnectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"SELECT * FROM jogging";

                var tableData = new List<Exercise>();

                SQLiteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new Exercise
                            {
                                Id = reader.GetInt32(0), //returns value of column specified
                                Date = DateTime.ParseExact(reader.GetString(1), "mm-dd-yy", new CultureInfo("en-US")), //returns date from column specified
                                Miles = reader.GetInt32(2) //returns miles of column specified
                            });
                    }
                }
                else { Console.WriteLine("No records found."); }

                connection.Close();


                Console.WriteLine("====================================");
                foreach (var ex in tableData)
                {
                    Console.WriteLine($"ID: {ex.Id} Date: {ex.Date.ToString("MM-dd-yyyy")} miles: {ex.Miles}");
                }
                Console.WriteLine("====================================");
            }
        }





        //Update
        public static void Update()
        {
            Console.Clear();

            ViewAllRecords();

            var recordId = Helpers.GetNumberInput("\n\nPlease type the Id of the record you'd like to update " +
                                                "or 0 to return to the Main Menu");
            using (var connection = new SQLiteConnection(Program.ConnectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM jogging WHERE Id = {recordId})";
                var checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());//returns 0 for false 1 for true

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nThe following record Id doesnt exist: {recordId}\n\n");
                    connection.Close();
                    Update();
                }

                string date = Helpers.GetDateInput();

                var miles = Helpers.GetNumberInput("Enter the amount of miles jogged as an integer: (1, 2, 3)." +
                                        "\nType 0 to return to the main menu");

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE jogging SET date = '{date}', miles = {miles}" +
                                       $" WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }






        //Delete
        public static void Delete()
        {
            Console.Clear();
            ViewAllRecords();

            var recordId = Helpers.GetNumberInput("\n\nPlease type the Id of the record you'd like to delete " +
                                                "or 0 to return to the Main Menu");

            using (var connection = new SQLiteConnection(Program.ConnectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from jogging WHERE Id = '{recordId}'";

                var rowCount = tableCmd.ExecuteNonQuery();//returns the amount of rows affected by command

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nThe record you entered does not exist: {recordId}. Press Enter to continue.\n\n");
                    Console.ReadLine();

                    Delete();
                }
            }

            Console.WriteLine($"The following record Id was deleted: {recordId}. Press Enter to return to main menu.\n\n");
            Console.ReadLine();

            MainMenu.GetUserInput();
        }
    }
}