using IbmServices.Models;
using IbmServices.Services.Rates;
using System.Collections.Generic;

namespace IbmTests.Controllers.Mocks
{
    public class RateServiceMock : IRateService
    {
        public IEnumerable<Rate> GetAll()
        {
            return new List<Rate>()
            {
                new Rate() { Id = 1, From = "EUR", To = "USD", RateVal = 1.359m },
                new Rate() { Id = 2, From = "CAD", To = "EUR", RateVal = 0.732m },
                new Rate() { Id = 3, From = "USD", To = "EUR", RateVal = 0.736m },
                new Rate() { Id = 4, From = "EUR", To = "CAD", RateVal = 1.366m }
            };
        }
    }
}
