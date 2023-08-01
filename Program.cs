﻿// See https://aka.ms/new-console-template for more information

using Tax;
using Tax.Models;

var congestionTaxCalculator = new CongestionTaxCalculator();

var fee = CongestionTaxCalculator.GetTax(new Car(), new List<DateTime>
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
});

Console.WriteLine(fee);