using System.Data.SQLite;

namespace ExerciseTracker
{
    internal class Program
    {
        public static string ConnectionString { get; private set; } = @"Data Source=habit-Tracker.db";
        
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=habit-Tracker.db";

            //Using statement calls Dispose() after the using block is left.
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand(); //Creates a command to send to DB

                tableCmd.CommandText = //Defines the command string to create a table
                    @"CREATE TABLE IF NOT EXISTS jogging
                        (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        miles INTEGER
                        )";

                tableCmd.ExecuteNonQuery();//Executes the command without returning a value. Only telling it to create a table.

                connection.Close(); //Closes the connection with the DB
            }
            MainMenu.GetUserInput();
        }
        
    }
}