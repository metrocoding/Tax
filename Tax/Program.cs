// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Tax.BusinessLogic;
using Tax.Models;
using Tax.Repository;

namespace Tax;

public class Program
{
    public static void Main()
    {
        //setup our DI
        new ServiceCollection()
            .AddLogging()
            .AddScoped<IRepository<Holiday>, HolidaysRepository>()
            .AddScoped<IRepository<TimeRangeTaxFee>, TimeRangeTaxFeesRepository>()
            .AddScoped<IRepository<string>, TollFreeVehiclesRepository>()
            .AddScoped<ICongestionTaxCalculatorBusinessLogic, CongestionTaxCalculatorBusinessLogic>()
            .BuildServiceProvider();
    }
}