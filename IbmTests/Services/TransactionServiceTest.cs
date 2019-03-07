using IbmServices.Configs;
using IbmServices.Models;
using IbmServices.Services.Rates;
using IbmServices.Services.ResourceClients;
using IbmServices.Services.Transactions;
using IbmTests.Services.Mocks;
using Microsoft.Extensions.Options;
using System.Linq;
using Xunit;

namespace IbmTests.Services
{
    public class TransactionServiceTest
    {
        private readonly IResourceClient<Transaction> TestClient;
        private readonly ITransactionService TestService;

        public TransactionServiceTest()
        {
            var LogFactory = NLog.LogManager.LoadConfiguration("NLog.config");

            var TestRateService = new RateService(new RateRepositoryMock(), new RateClientMock(), LogFactory);

            this.TestClient = new TransactionClientMock();
            this.TestService = new TransactionService(new TransactionRepositoryMock(), this.TestClient, TestRateService, LogFactory);
        }

        [Fact]
        public void TestGetAll()
        {
            // Execution
            var TestResult = this.TestService.GetAll().ToList();

            // Assertions
            Assert.True(TestResult.Count == 5, "TestResult should have received 5 rows!");
            Assert.Equal("T2006", TestResult[0].Sku);
            Assert.Equal(10.00m, TestResult[0].Amount);
            Assert.Equal("USD", TestResult[0].Currency);
            Assert.Equal("T2006", TestResult[1].Sku);
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
        public void TestGetBySku()
        {
            // Arrange
            const string TestSku = "T2006";

            // Execution
            var TestResult = this.TestService.GetMany(TestSku).ToList();

            // Assertions
            Assert.True(TestResult.Count == 3, "TestResult should have received 3 rows!");
            Assert.Equal("T2006", TestResult[0].Sku);
            Assert.Equal(7.36m, TestResult[0].Amount, 2);
            Assert.Equal("EUR", TestResult[0].Currency);
            Assert.Equal("T2006", TestResult[1].Sku);
            Assert.Equal(25.31m, TestResult[1].Amount, 2);
            Assert.Equal("EUR", TestResult[1].Currency);
            Assert.Equal("T2006", TestResult[2].Sku);
            Assert.Equal(7.63m, TestResult[2].Amount, 2);
            Assert.Equal("EUR", TestResult[2].Currency);
        }
    }
}
