using FluentAssertions;
using IntelligentTourGuide.Web.Controllers;
using IntelligentTourGuide.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyGuideUnitTest
{
    public partial class StatesApiTests
    {
        [Fact]
        public void GetStates_OkResult()
        {
            // 1. ARRANGE
            var dbName = nameof(StatesApiTests.GetStates_OkResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new StatesController(dbContext, logger);

            // 2. ACT
            IActionResult actionResultGet = controller.GetStates().Result;

            // 3. ASSERT
            // ---- Check if the IActionResult is OK (HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ---- If the Status Cose is HTTP 200 "OK"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

       

        [Fact]
        public void GetStates_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(StatesApiTests.GetStates_CheckCorrectResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new StatesController(dbContext, logger);

            // ACT
            IActionResult actionResultGet = controller.GetStates().Result;

            // ASSERT if the IActionResult is OK (HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);

            // Extract the result from the IActionResut object
            var okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT if OkResult contains an object of the correct type
            Assert.IsAssignableFrom<List<State>>(okResult.Value);

            // Extract the Categories from the result of the action.
            var states = okResult.Value.Should().BeAssignableTo<List<State>>().Subject;

            // ASSERT if categories is NOT NULL
            Assert.NotNull(states);

            // ASEERT if number of categories matches with the TEST Data
            Assert.Equal(expected: DbContextMocker.TestData_States.Length,
                         actual: states.Count);

            // ASSERT if data is correct
            int ndx = 0;
            foreach (State state in DbContextMocker.TestData_States)
            {
                Assert.Equal<int>(expected: state.StateId,
                                  actual: states[ndx].StateId);
                Assert.Equal(expected: state.StateName,
                             actual: states[ndx].StateName);

                _testOutputHelper.WriteLine($"Row # {ndx} OKAY");
                ndx++;
            }
        }
    }
}
