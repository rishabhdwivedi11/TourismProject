using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace MyGuideUnitTest
{
    public partial class StatesApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public StatesApiTests(
            ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
