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
        public void InsertState_OkResult()
        {
            // ARRANGE
            var dbName = nameof(StatesApiTests.InsertState_OkResult);
            var logger = Mock.Of<ILogger<StatesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new StatesController(dbContext, logger);
            State stateToAdd = new State
            {
                StateId = 5,
                StateName = null             // INVALID!  StateName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = controller.PostState(stateToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted state object
            State actualState = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<State>().Subject;

            // ASSERT - if the inserted state object is NOT NULL
            Assert.NotNull(actualState);

            Assert.Equal(stateToAdd.StateId, actualState.StateId);
            Assert.Equal(stateToAdd.StateName, actualState.StateName);
        }
    }
}
