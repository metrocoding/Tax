using Tax.Interfaces;

namespace Tax.BusinessLogic;

public interface ICongestionTaxCalculatorBusinessLogic
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that days
         */
    int GetTax(IVehicle vehicle, IEnumerable<DateTime> dates);

    int GetTollFee(DateTime date);
    bool IsTollFreeDate(DateTime date);
}