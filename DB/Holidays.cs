using Tax.Models;

namespace Tax.DB
{
    public static class Holidays
    {
        public static IEnumerable<Holiday> GetHolidays() => new List<Holiday> { new Holiday(2, 14, 3), new Holiday(6, 1, 3) };
    }
}