using Tax.DB;
using Tax.Interfaces;

namespace Tax
{
    public class CongestionTaxCalculator
    {
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that days
         */
        public static int GetTax(IVehicle vehicle, IEnumerable<DateTime> dates)
        {
            if (vehicle.IsTollFree()) return 0;
            var overallFee = 0;

            // group by day
            var grouped = dates.GroupBy(d => d.DayOfYear);
            foreach (var items in grouped)
            {
                var fee = 0;
                var passesInDay = items.ToList();

                // calculate tax per day
                while (passesInDay.Any())
                {
                    passesInDay.Sort((a, b) => a.CompareTo(b));
                    var firstDate = passesInDay.First();
                    var passesWithin60Minutes =
                        passesInDay.Where(d => d.Subtract(firstDate).TotalMinutes <= 60).ToList();
                    fee += passesWithin60Minutes.Max(GetTollFee);
                    passesInDay.RemoveAll(d => passesWithin60Minutes.Contains(d));

                    if (fee <= 60) continue;

                    fee = 60;
                    passesInDay.Clear();
                }

                overallFee += fee;
            }

            // return overall tax even more than one day 
            return overallFee;
        }

        private static int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;

            var timeRangeTaxFee = TimeRangeTaxFees.GetTimeBasedTaxFees().SingleOrDefault(t =>
                (t.StartTime <= date.TimeOfDay && t.EndTime >= date.TimeOfDay) || (t.StartTime > t.EndTime &&
                    (t.StartTime <= date.TimeOfDay || t.EndTime >= date.TimeOfDay)));

            ArgumentNullException.ThrowIfNull(timeRangeTaxFee);

            return timeRangeTaxFee.Fee;
        }

        private static bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ||
                   Holidays.GetHolidays().Any(h => h.IsVehiclePassedOnTheseDays(date));
        }
    }
}