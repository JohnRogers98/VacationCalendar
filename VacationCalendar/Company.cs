
namespace VacationCalendar
{
    public class Company
    {
        public static IEnumerable<Employee> Employees { get; } = new List<Employee>
        {
            new Employee("John"),
            new Employee("Laurie"),
            new Employee("Amy"),
            new Employee("Jo"),
            new Employee("Meg"),
            new Employee("Beth")
        };
    }
}
