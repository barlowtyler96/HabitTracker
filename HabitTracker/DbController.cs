using ExerciseTracker;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    internal class DbController
    {
        public static void CreateDb()
        {
            
            var connectionString = @"Data Source=habit-Tracker.db";

            //Using statement calls Dispose() after the using block is left.
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand(); //Creates a command to send to DB

                tableCmd.CommandText = //Defines the command string to create a table
                    @"CREATE TABLE IF NOT EXISTS habits
                        (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Habit TEXT,
                        Unit TEXT,
                        Amount INTEGER
                        )";

                tableCmd.ExecuteNonQuery();//Executes the command without returning a value. Only telling it to create a table.

                connection.Close(); //Closes the connection with the DB
            }
            MainMenu.GetUserInput();
        }
    }
}
