using HabitTracker;
using System.Data.SQLite;

namespace ExerciseTracker
{
    internal class Program
    {
        public static string ConnectionString { get; private set; } = @"Data Source=habit-Tracker.db";
        
        static void Main(string[] args)
        {
            
            DbController.CreateDb(); // If db does not exist, create.
        }
        
    }
}