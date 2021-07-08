using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using XUnitElegantTesting.Models;
using XunitElegantTestingTests.Fixtures;

namespace XunitElegantTestingTests.Models
{
    public class CalculatorTests
    {
        private readonly Calculator _sut;

        public CalculatorTests()
        {
            _sut = new Calculator();
        }

        [Fact(Skip = "This test is broken")]
        public void AddTwoNumbersShoulEqualTheirEqual()
        {
            decimal expected = 12;
            _sut.Add(5);
            _sut.Add(8);
            expected.Should().Be(_sut.Value);
        }

        [Theory]
        [InlineData(13,5,8)]
        [InlineData(0,-3,3)]
        [InlineData(0,0,0)]
        public void AddTwoNumbersShoulEqualTheirEqualTheory(
            decimal expected, decimal firstToAdd, decimal secondToAdd)
        {
            _sut.Add(firstToAdd);
            _sut.Add(secondToAdd);
            expected.Should().Be(_sut.Value);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void AddManyNumbersShoulEqualTheirEqualTheory(
            decimal expected, params decimal[] valuesToAdd)
        {
            foreach (var value in valuesToAdd)
            {
                _sut.Add(value);
            }

            expected.Should().Be(_sut.Value);
        }


        [Theory]
        [ClassData(typeof(DivisionTestData))]
        public void DivideManyNumbersTheory(
    decimal expected, params decimal[] valuesToDivide)
        {
            foreach (var value in valuesToDivide)
            {
                _sut.Divide(value);
            }

            expected.Should().Be(_sut.Value);
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { 15, new decimal[] { 10, 5 }};
            yield return new object[] { 15, new decimal[] { 5, 5, 5 }};
            yield return new object[] { -10, new decimal[] { -30, 20 }};
        }

    }

}
