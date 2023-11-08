
namespace VacationCalendar
{
    public class Calendar
    {
        private DateTime StartOfYear { get; } = new DateTime(DateTime.Today.Year, 1, 1);
        private DateTime EndOfYear = new DateTime(DateTime.Today.Year, 12, 31);
        private List<String> WorkingDays { get; } = new List<String> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        private Dictionary<DateTime, Vacation> _vacationsDictionary
            = new Dictionary<DateTime, Vacation>();

        private IList<Func<VacationLine, Boolean>> _vacationRules = new List<Func<VacationLine, Boolean>>();


        public Calendar()
        {
            InitializeVacationRules();
        }


        public void RandomFilling()
        {
            Random random = new Random();
            int[] vacationSteps = { 7, 14 };
            int randomStep;

            int dayRange = (EndOfYear - StartOfYear).Days;

            DateTime startOfVacation;
            DateTime endOfVacation;
            int lengthOfVacation;
            VacationLine vacationLine;

            foreach (var employee in Company.Employees)
            {
                while (employee.NotUsedVacationCount > 0)
                {
                    startOfVacation = StartOfYear.AddDays(random.Next(dayRange));

                    if (!WorkingDays.Contains(startOfVacation.DayOfWeek.ToString()))
                    {
                        continue;
                    }

                    randomStep = random.Next(vacationSteps.Length);
                    lengthOfVacation = employee.NotUsedVacationCount == 7 ? 7 : vacationSteps[randomStep];

                    endOfVacation = startOfVacation.AddDays(lengthOfVacation);

                    vacationLine = VacationLine.Generate(startOfVacation, endOfVacation);

                    var accepted = IsAcceptedByCalendarRules(vacationLine);
                    if (!accepted)
                    {
                        continue;
                    }

                    var isAdded = employee.AddVacations(vacationLine);
                    if (isAdded)
                    {
                        AddVacatioins(vacationLine);
                    }
                }
            }
        }

        private void AddVacatioins(VacationLine vacationLine)
        {
            foreach (var vacation in vacationLine.Vacations)
            {
                _vacationsDictionary.Add(vacation.DateTime, vacation);
            }
        }

        private void InitializeVacationRules()
        {
            Func<VacationLine, Boolean> rule = delegate (VacationLine vacationLine)
            {
                DateTime from = vacationLine.Start.AddDays(-3);
                DateTime end = vacationLine.End;

                for (int i = 0; i <= (end - from).Days; i++)
                {
                    if (_vacationsDictionary.Keys.Contains(from.AddDays(i)))
                    {
                        return false;
                    }
                }
                return true;
            };

            _vacationRules.Add(rule);
        }

        private Boolean IsAcceptedByCalendarRules(VacationLine vacationLine)
        {
            if (_vacationRules.Any(rule => rule(vacationLine) == false))
                return false;
            return true;
        }
    }
}
