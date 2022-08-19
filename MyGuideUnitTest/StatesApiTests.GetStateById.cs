using FluentAssertions;
using IntelligentTourGuide.Web.Controllers;
using IntelligentTourGuide.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MyGuideUnitTest
{
   public partial class StatesApiTests
    {
        [Fact]
        public void GetStateById_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(StatesApiTests.GetStateById_NotFoundResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new StatesController(dbContext, logger);
            short findStateID = 900;

            // ACT
            IActionResult actionResultGet = controller.GetState(findStateID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetStateById_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(StatesApiTests.GetStateById_BadRequestResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new StatesController(dbContext, logger);
            short? findStateID = null;

            // ACT
            IActionResult actionResultGet = controller.GetState(findStateID).Result;

            // ASSERT - check if the IActionResult is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetStateById_OkResult()
        {
            // ARRANGE
            var dbName = nameof(StatesApiTests.GetStateById_OkResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new StatesController(dbContext, logger);
            short findStateID = 2;

            // ACT
            IActionResult actionResultGet = controller.GetState(findStateID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetStateById_CorrectResult()
        {
            // ARRANGE
            var dbName = nameof(StatesApiTests.GetStateById_OkResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new StatesController(dbContext, logger);

            short findStateID = 2;
            State expectedState = DbContextMocker.TestData_States
                                       .SingleOrDefault(c => c.StateId == findStateID);

            // ACT
            IActionResult actionResultGet = controller.GetState(findStateID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if IActionResult (i.e., OkObjectResult) contains an object of the type Category
            OkObjectResult okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;
            Assert.IsType<State>(okResult.Value);

            // Extract the category object from the result.
            State actualState = okResult.Value.Should().BeAssignableTo<State>().Subject;
            _testOutputHelper.WriteLine($"Found: CategoryID == {actualState.StateId}");

            // ASSERT - if category is NOT NULL
            Assert.NotNull(actualState);

            // ASSERT - if the CategoryId is containing the expected data.
            Assert.Equal<int>(expected: expectedState.StateId,
                              actual: actualState.StateId);

            // ASSERT - if the CateogoryName is correct 
            Assert.Equal(expectedState.StateName, actualState.StateName);
        }
    }
}
