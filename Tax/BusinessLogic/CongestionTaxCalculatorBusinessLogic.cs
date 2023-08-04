using Tax.Interfaces;
using Tax.Models;
using Tax.Repository;

namespace Tax.BusinessLogic
{
    public class CongestionTaxCalculatorBusinessLogic : ICongestionTaxCalculatorBusinessLogic
    {
        private readonly IRepository<TimeRangeTaxFee> _timeRangeTaxFeeRepository;
        private readonly IRepository<string> _tollFreeVehiclesRepository;
        private readonly IRepository<Holiday> _holidayRepository;

        public CongestionTaxCalculatorBusinessLogic(
            IRepository<Holiday> holidayRepository,
            IRepository<TimeRangeTaxFee> timeRangeTaxFeeRepository,
            IRepository<string> tollFreeVehiclesRepository)
        {
            _holidayRepository = holidayRepository;
            _timeRangeTaxFeeRepository = timeRangeTaxFeeRepository;
            _tollFreeVehiclesRepository = tollFreeVehiclesRepository;
        }


        // little bit faster and more memory efficient than original method based on BenchmarkDotnet
        public int GetTax(IVehicle vehicle, IEnumerable<DateTime> dates)
        {
            if (vehicle.IsTollFree(_tollFreeVehiclesRepository.GetAll())) return 0;
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

        public int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;

            var timeRangeTaxFee = _timeRangeTaxFeeRepository.GetAll().SingleOrDefault(t =>
                (t.StartTime <= date.TimeOfDay && t.EndTime >= date.TimeOfDay) || (t.StartTime > t.EndTime &&
                    (t.StartTime <= date.TimeOfDay || t.EndTime >= date.TimeOfDay)));

            ArgumentNullException.ThrowIfNull(timeRangeTaxFee);

            return timeRangeTaxFee.Fee;
        }

        public bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ||
                   _holidayRepository.GetAll().Any(h => h.IsVehiclePassedOnTheseDays(date));
        }
    }
}