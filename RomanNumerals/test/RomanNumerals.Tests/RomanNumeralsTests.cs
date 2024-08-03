using FluentAssertions;

namespace RomanNumerals.Tests;

public class RomanNumeralsTests {

    [TestCase(1, "I")]
    [TestCase(2, "II")]
    [TestCase(3, "III")]
    [TestCase(4, "IV")]
    [TestCase(5, "V")]
    [TestCase(6, "VI")]
    [TestCase(7, "VII")]
    [TestCase(8, "VIII")]
    [TestCase(9, "IX")]
    [TestCase(10, "X")]
    [TestCase(11, "XI")]
    [TestCase(12, "XII")]
    [TestCase(13, "XIII")]
    [TestCase(14, "XIV")]
    [TestCase(18, "XVIII")]
    [TestCase(19, "XIX")]
    [TestCase(20, "XX")]
    [TestCase(39, "XXXIX")]
    [TestCase(40, "XL")]
    [TestCase(41, "XLI")]
    [TestCase(49, "XLIX")]
    [TestCase(50, "L")]
    [TestCase(51, "LI")]
    [TestCase(89, "LXXXIX")]
    [TestCase(90, "XC")]
    [TestCase(99, "XCIX")]
    [TestCase(100, "C")]
    [TestCase(399, "CCCXCIX")]
    public void return_ronam_numeral(int number, string romanNumeral) {
        var romanNumerals = new RomanNumerals();

        var output = romanNumerals.Execute(number);

        output.Should().Be(romanNumeral);
    }

}