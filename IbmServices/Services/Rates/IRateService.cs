using IbmServices.Models;
using System.Collections.Generic;

namespace IbmServices.Services.Rates
{
    public interface IRateService
    {
        IEnumerable<Rate> GetAll();
    }
}
