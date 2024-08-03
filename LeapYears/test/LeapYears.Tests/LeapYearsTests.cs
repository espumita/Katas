using FluentAssertions;

namespace LeapYears.Tests;

public class LeapYearsTests {


    [TestCase(400)]
    [TestCase(800)]
    [TestCase(1200)]
    [TestCase(1600)]
    [TestCase(2000)]
    [TestCase(2400)]
    public void get_years_divisible_by_400_as_leap_years(int year) {
        var leapYears = new LeapYears();

        var isLeapYear = leapYears.IsLeapYear(year);

        isLeapYear.Should().BeTrue();
    }

    [TestCase(2100)]
    [TestCase(1900)]
    [TestCase(1800)]
    [TestCase(1700)]
    public void get_years_divisible_by_100_but_not_by_400_as_not_leap_years(int year) {
        var leapYears = new LeapYears();

        var isLeapYear = leapYears.IsLeapYear(year);

        isLeapYear.Should().BeFalse();
    }

    [TestCase(2004)]
    [TestCase(2008)]
    [TestCase(2012)]
    [TestCase(2016)]
    [TestCase(2020)]
    [TestCase(2024)]
    [TestCase(2028)]
    public void get_years_divisible_by_4_but_not_by_100_as_leap_years(int year) {
        var leapYears = new LeapYears();

        var isLeapYear = leapYears.IsLeapYear(year);

        isLeapYear.Should().BeTrue();
    }

    [TestCase(2017)]
    [TestCase(2018)]
    [TestCase(2019)]
    [TestCase(2021)]
    [TestCase(2022)]
    [TestCase(2023)]
    public void get_years_not_divisible_by_4_as_not_leap_years(int year) {
        var leapYears = new LeapYears();

        var isLeapYear = leapYears.IsLeapYear(year);

        isLeapYear.Should().BeFalse();
    }


}