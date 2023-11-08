
namespace VacationCalendar
{
    public record class Employee : Person
    {
        public IList<Vacation> Vacations { get; } = new List<Vacation>();

        private IList<Func<VacationLine, Boolean>> _vacationRules = new List<Func<VacationLine, Boolean>>();

        public String Name { get; init; }

        public Int32 NotUsedVacationCount { get; private set; }

        public Employee(String name)
        {
            Id = new Guid();
            Name = name;

            NotUsedVacationCount = 28;
            InitializeVacationRules();
        }

        public Boolean AddVacations(VacationLine vacationLine)
        {
            Boolean isNotAccepted = _vacationRules.Any(rule => rule(vacationLine) == false);
            if (isNotAccepted)
            {
                return false;
            }

            foreach (Vacation vacation in vacationLine.Vacations)
            {
                Vacations.Add(vacation);
            }

            NotUsedVacationCount -= vacationLine.Vacations.Count();

            return true;
        }

        private void InitializeVacationRules()
        {
            Func<VacationLine, Boolean> rule = delegate (VacationLine vacationLine)
            {
                if (vacationLine.Vacations.Count() > NotUsedVacationCount)
                    return false;
                return true;
            };
            _vacationRules.Add(rule);
        }
    }
}
