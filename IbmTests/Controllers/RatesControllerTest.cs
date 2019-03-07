using ExamenVuelingJCGarcia.Controllers;
using IbmTests.Controllers.Mocks;
using System.Linq;
using Xunit;

namespace IbmTests.Controllers
{
    public class RatesControllerTest
    {
        [Fact]
        public void TestGetAll()
        {
            // Arrange
            var TestController = new RatesController(new RateServiceMock());

            // Execution
            var TestResult = TestController.GetRates().ToList();

            // Assertions
            Assert.True(TestResult.Count == 4, "TestResult should have received 4 rows!");
            Assert.Equal("EUR", TestResult[0].From);
            Assert.Equal("USD", TestResult[0].To);
            Assert.Equal(1.359m, TestResult[0].RateVal);
            Assert.Equal("CAD", TestResult[1].From);
            Assert.Equal("EUR", TestResult[1].To);
            Assert.Equal(0.732m, TestResult[1].RateVal);
            Assert.Equal("USD", TestResult[2].From);
            Assert.Equal("EUR", TestResult[2].To);
            Assert.Equal(0.736m, TestResult[2].RateVal);
            Assert.Equal("EUR", TestResult[3].From);
            Assert.Equal("CAD", TestResult[3].To);
            Assert.Equal(1.366m, TestResult[3].RateVal);
        }
    }
}
