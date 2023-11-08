
namespace VacationCalendar
{
    public struct VacationLine
    {
        public Guid Id { get; }

        public IEnumerable<Vacation> Vacations { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        private VacationLine(IEnumerable<Vacation> vacations)
        {
            Id = Guid.NewGuid();
            Vacations = new List<Vacation>(vacations);

            Start = Vacations.First().DateTime;
            End = Vacations.Last().DateTime;
        }

        public static VacationLine Generate(DateTime from, DateTime to)
        {
            var days = new List<Vacation>();
            for (int i = 0; i < (to - from).Days; i++)
            {
                days.Add(new Vacation(from.AddDays(i)));
            }

            return new VacationLine(days);
        }
    }
}
