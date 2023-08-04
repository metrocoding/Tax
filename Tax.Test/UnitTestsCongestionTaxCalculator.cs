using Moq;
using Tax.BusinessLogic;
using Tax.Interfaces;
using Tax.Models;
using Tax.Repository;
using Xunit;

namespace Tax.Test;

public class UnitTestsCongestionTaxCalculator
{
    private readonly CongestionTaxCalculatorBusinessLogic _congestionTaxCalculator;


    // setup
    public UnitTestsCongestionTaxCalculator()
    {
        Mock<IRepository<TimeRangeTaxFee>> timeRangeTaxFeeRepository = new();
        timeRangeTaxFeeRepository.Setup(x => x.GetAll())
            .Returns(new TimeRangeTaxFeesRepository().GetAll());

        Mock<IRepository<string>> tollFreeVehiclesRepository = new();
        tollFreeVehiclesRepository.Setup(x => x.GetAll())
            .Returns(new TollFreeVehiclesRepository().GetAll);

        Mock<IRepository<Holiday>> holidayRepository = new();
        holidayRepository.Setup(x => x.GetAll())
            .Returns(new HolidaysRepository().GetAll());


        _congestionTaxCalculator = new CongestionTaxCalculatorBusinessLogic(
            holidayRepository.Object,
            timeRangeTaxFeeRepository.Object,
            tollFreeVehiclesRepository.Object);
    }

    [Fact]
    public void GetTaxReturnsCorrectFee()
    {
        // arrange
        var dates = new List<DateTime>
        {
            DateTime.Parse("2013-01-14 21:00:00"),
            DateTime.Parse("2013-01-15 21:00:00"),
            DateTime.Parse("2013-02-07 06:23:27"),
            DateTime.Parse("2013-02-07 15:27:00"),
            DateTime.Parse("2013-02-08 06:27:00"),
            DateTime.Parse("2013-02-08 06:20:27"),
            DateTime.Parse("2013-02-08 14:35:00"),
            DateTime.Parse("2013-02-08 15:29:00"),
            DateTime.Parse("2013-02-08 15:47:00"),
            DateTime.Parse("2013-02-08 16:01:00"),
            DateTime.Parse("2013-02-08 16:48:00"),
            DateTime.Parse("2013-02-08 17:49:00"),
            DateTime.Parse("2013-02-08 18:29:00"),
            DateTime.Parse("2013-02-08 18:35:00"),
            DateTime.Parse("2013-03-26 14:25:00"),
            DateTime.Parse("2013-03-28 14:07:27")
        };

        IVehicle vehicle = new Car();

        // action
        var fee = _congestionTaxCalculator.GetTax(vehicle, dates);

        // assert
        Assert.Equal(97, fee);
    }

    [Fact]
    public void GetTaxReturnsZeroForTractor()
    {
        // arrange
        var dates = new List<DateTime>
        {
            DateTime.Parse("2013-02-08 18:35:00"),
            DateTime.Parse("2013-03-26 14:25:00"),
            DateTime.Parse("2013-03-28 14:07:27")
        };

        IVehicle vehicle = new Tractor();

        // action
        var fee = _congestionTaxCalculator.GetTax(vehicle, dates);

        // assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFeeReturnsCorrectFee()
    {
        // arrange
        var date = DateTime.Parse("2013-01-14 15:10:00");

        // act
        var fee = _congestionTaxCalculator.GetTollFee(date);

        // assert
        Assert.Equal(13, fee);
    }

    [Fact]
    public void GetTollFeeReturnsZeroInHoliday()
    {
        // arrange
        var date = DateTime.Parse("2013-02-14 15:10:00");

        // act
        var fee = _congestionTaxCalculator.GetTollFee(date);

        // assert
        Assert.Equal(0, fee);
    }
}