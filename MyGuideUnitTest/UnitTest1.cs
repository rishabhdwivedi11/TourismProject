using System;
using Xunit;
using Xunit.Abstractions;

namespace MyGuideUnitTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(
            ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            // ARRANGE
            int expectedResult = 45;
            int actualResult;
            int a = 11, b = 34;

            // ACT
            actualResult = a + b;

            _testOutputHelper.WriteLine($"input values are {a} and {b}");
            _testOutputHelper.WriteLine($"expectedResult = {expectedResult}, ActualResult = {actualResult}");

            // ASSERT
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
