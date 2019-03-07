using ExamenVuelingJCGarcia.Controllers;
using ExamenVuelingJCGarcia.DTOs;
using IbmTests.Controllers.Mocks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace IbmTests.Controllers
{
    public class TransactionsControllerTest
    {
        private readonly TransactionsController TestController;

        public TransactionsControllerTest()
        {
            this.TestController = new TransactionsController(new TransactionServiceMock(), NLog.LogManager.LoadConfiguration("NLog.config"));
        }

        [Fact]
        public void TestGetAll()
        {
            // Execution
            var TestResult = TestController.GetAllTransactions().ToList();

            // Assertions
            Assert.True(TestResult.Count == 5, "TestResult should have received 5 rows!");
            Assert.Equal("T2006", TestResult[0].Sku);
            Assert.Equal(10.00m, TestResult[0].Amount);
            Assert.Equal("USD", TestResult[0].Currency);
            Assert.Equal("M2007", TestResult[1].Sku);
            Assert.Equal(34.57m, TestResult[1].Amount);
            Assert.Equal("CAD", TestResult[1].Currency);
            Assert.Equal("R2008", TestResult[2].Sku);
            Assert.Equal(17.95m, TestResult[2].Amount);
            Assert.Equal("USD", TestResult[2].Currency);
            Assert.Equal("T2006", TestResult[3].Sku);
            Assert.Equal(7.63m, TestResult[3].Amount);
            Assert.Equal("EUR", TestResult[3].Currency);
            Assert.Equal("B2009", TestResult[4].Sku);
            Assert.Equal(21.23m, TestResult[4].Amount);
            Assert.Equal("USD", TestResult[4].Currency);
        }

        [Fact]
        public void TestGetMany()
        {
            // Arrange
            const string TestSku = "T2006";

            // Execution
            var ActionResult = TestController.GetBySku(TestSku);

            var OkResult = Assert.IsType<OkObjectResult>(ActionResult);
            var TestResult = Assert.IsAssignableFrom<TransactionsAmountDto>(OkResult.Value);

            // Assertions
            Assert.True(TestResult.Transactions.Count == 2, "TestResult should have received 2 rows!");
            Assert.Equal("T2006", TestResult.Transactions[0].Sku);
            Assert.Equal(10.00m, TestResult.Transactions[0].Amount);
            Assert.Equal("USD", TestResult.Transactions[0].Currency);
            Assert.Equal("T2006", TestResult.Transactions[1].Sku);
            Assert.Equal(7.63m, TestResult.Transactions[1].Amount);
            Assert.Equal("EUR", TestResult.Transactions[1].Currency);
            Assert.Equal(17.63m, TestResult.Total);
        }
    }
}
