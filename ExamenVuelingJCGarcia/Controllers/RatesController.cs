using System.Collections.Generic;
using IbmServices.Models;
using IbmServices.Services.Rates;
using Microsoft.AspNetCore.Mvc;

namespace ExamenVuelingJCGarcia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService _RateService;

        public RatesController(IRateService RateService)
        {
            this._RateService = RateService;
        }

        // GET: api/Rates
        [HttpGet]
        public IEnumerable<Rate> GetRates()
        {
            return this._RateService.GetAll();
        }
    }
}