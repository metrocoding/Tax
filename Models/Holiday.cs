namespace Tax.Models
{
    public class Holiday
    {
        public Holiday(int month, int day, int taxFreeDaysBeforeHoliday)
        {
            TaxFreeDaysBeforeHoliday = taxFreeDaysBeforeHoliday;
            HolidayDate = new DateTime(2013, month, day);
        }

        private int TaxFreeDaysBeforeHoliday { get; }
        
        private DateTime HolidayDate { get; }
        private DateTime StartDate => HolidayDate.AddDays(-TaxFreeDaysBeforeHoliday);

        // determines date is on holiday or previous day
        public bool IsVehiclePassedOnTheseDays(DateTime vehiclePassDate)
        {
            return vehiclePassDate >= StartDate && vehiclePassDate <= HolidayDate;
        }
    }
}