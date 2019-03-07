using IbmServices.Services.Transactions;
using IbmServices.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using NLog;
using ExamenVuelingJCGarcia.DTOs;

namespace ExamenVuelingJCGarcia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly Logger _Logger;

        private readonly ITransactionService _TransactionService;

        public TransactionsController(ITransactionService _TransactionService, LogFactory Factory)
        {
            this._TransactionService = _TransactionService;
            this._Logger = Factory.GetCurrentClassLogger();
        }

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return this._TransactionService.GetAll();
        }

        // GET: api/Transactions/5
        [HttpGet("{Sku}")]
        public IActionResult GetBySku([FromRoute] string Sku)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Transaction> Transactions;
            try
            {
                Transactions = this._TransactionService.GetMany(Sku);

                if (Transactions == null)
                {
                    return new JsonResult(new List<Transaction>());
                }

                var Result = new TransactionsAmountDto()
                {
                    Transactions = Transactions.ToList(),
                    Total = Transactions.Sum(x => x.Amount)
                };

                return Ok(Result);
            }
            catch (Exception ex)
            {
                this._Logger.Error($"Error at transaction's conversion: {ex.Message}");
                return new JsonResult(new { success = false, message = "Service unavailable, please try again." });
            }
        }
    }
}