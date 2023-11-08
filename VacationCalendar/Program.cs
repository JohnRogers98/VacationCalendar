using VacationCalendar;

internal class Program
{
    private static void Main(string[] args)
    {
        //Program p = new Program();
        //p.RandomVacationFilling();

        Calendar calendar = new Calendar();
        calendar.RandomFilling();

        foreach (var employee in Company.Employees)
        {
            Console.WriteLine($"Vacation days of {employee.Name}");
            foreach (var vacation in employee.Vacations)
            {
                Console.WriteLine(vacation.DateTime);
            }
        }
        Console.ReadKey();
    }

    public void RandomVacationFilling()
    {
        var vacationDictionary = GetVacationDictionary();
        var aviableWorkingDays = GetWorkingDays();

        Random random = new Random();
        int[] vacationSteps = { 7, 14 };
        int randomStep;

        DateTime startOfYear = new DateTime(DateTime.Today.Year, 1, 1);
        DateTime endOfYear = new DateTime(DateTime.Today.Year, 12, 31);
        int dayRange = (endOfYear - startOfYear).Days;

        DateTime startOfVacation;
        DateTime endOfVacation;
        int lengthOfVacation;
        int vacationCount = 28;

        List<DateTime> allVacations = new List<DateTime>();

        foreach (var vacationList in vacationDictionary.Values)
        {
            vacationCount = 28;

            while (vacationCount > 0)
            {
                startOfVacation = startOfYear.AddDays(random.Next(dayRange));

                if (!aviableWorkingDays.Contains(startOfVacation.DayOfWeek.ToString()))
                {
                    continue;
                }

                randomStep = random.Next(vacationSteps.Length);
                lengthOfVacation = vacationCount == 7 ? 7 : vacationSteps[randomStep];

                endOfVacation = startOfVacation.AddDays(lengthOfVacation);

                //Check by allVacation list rules
                if (allVacations.Any(element => element.AddDays(3) >= startOfVacation && element.AddDays(3) <= endOfVacation))
                {
                    continue;
                }

                for (int i = 0; i < (endOfVacation - startOfVacation).Days; i++)
                {
                    allVacations.Add(startOfVacation.AddDays(i));
                    vacationList.Add(startOfVacation.AddDays(i));
                }
                vacationCount -= lengthOfVacation;
            }
        }

        DisplayVacations(vacationDictionary);
    }

    private Dictionary<String, List<DateTime>> GetVacationDictionary()
    {
        return new Dictionary<String, List<DateTime>>
        {
            ["Иванов И Иванович"] = new List<DateTime>(),
            ["Петров Петр Петрович"] = new List<DateTime>(),
            ["Юлина Юлия Юлиановна"] = new List<DateTime>(),
            ["Сидоров Сидор Сидорович"] = new List<DateTime>(),
            ["Павлов Павел Павлович"] = new List<DateTime>(),
            ["Георгиев Георг Георгиевич"] = new List<DateTime>()
        };
    }

    private List<String> GetWorkingDays()
    {
        return new List<String> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
    }

    private void DisplayVacations(Dictionary<String, List<DateTime>> vacationDictionary)
    {
        foreach (var vacationPair in vacationDictionary)
        {
            Console.WriteLine($"Vacation days of {vacationPair.Key} :");
            vacationPair.Value.ForEach(vacation => Console.WriteLine(vacation));
        }
    }
}