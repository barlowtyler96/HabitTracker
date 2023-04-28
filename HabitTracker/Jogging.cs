namespace ExerciseTracker
{
    internal class Exercise
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string Habit { get; set; }

        public string Unit { get; set; }
        public int Amount { get; set; }
    }
}