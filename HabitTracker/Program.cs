using HabitTracker.Controller;
using HabitTracker.View;

namespace HabitTracker;

internal class Program
{
    static void Main(string[] args)
    {
        DbController.CreateTable();
        MainMenu.GetUserInput();
    }
}