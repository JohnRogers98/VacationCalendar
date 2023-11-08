
namespace VacationCalendar
{
    public record struct Vacation
    {
        public DateTime DateTime { get; init; }

        public Vacation(DateTime dt)
        {
            DateTime = dt;
        }
    }
}
