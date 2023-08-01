using Tax.Models;

namespace Tax.DB
{
    public static class TimeRangeTaxFees
    {
        public static IEnumerable<TimeRangeTaxFee> GetTimeBasedTaxFees() => new List<TimeRangeTaxFee>
        {
            new(8, new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0)),
            new(13, new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0)),
            new(18, new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0)),
            new(13, new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0)),
            new(8, new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0)),
            new(13, new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0)),
            new(18, new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 0)),
            new(13, new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0)),
            new(8, new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0)),
            new(0, new TimeSpan(18, 30, 0), new TimeSpan(5, 59, 0))
        };
    }
}