using IbmServices.Models;
using System.Collections.Generic;

namespace ExamenVuelingJCGarcia.DTOs
{
    public class TransactionsAmountDto
    {
        public List<Transaction> Transactions { get; set; }

        public decimal Total { get; set; }

        public TransactionsAmountDto()
        {
            this.Transactions = new List<Transaction>();
        }
    }
}
