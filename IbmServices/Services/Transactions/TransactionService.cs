using System.Collections.Generic;
using System.Linq;
using IbmServices.Models;
using IbmServices.Repositories;
using IbmServices.Services.Rates;
using IbmServices.Services.ResourceClients;
using NLog;

namespace IbmServices.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly string FinalCurrency = "EUR";

        private readonly IRepository<Transaction> _TransactionRepository;
        private readonly IResourceClient<Transaction> _TransactionClient;
        private readonly IRateService _RateService;
        private readonly Logger _Logger;

        public TransactionService(
            IRepository<Transaction> TransactionRepository,
            IResourceClient<Transaction> TransactionClient,
            IRateService RateService,
            LogFactory Factory)
        {
            this._TransactionRepository = TransactionRepository;
            this._TransactionClient = TransactionClient;
            this._RateService = RateService;
            this._Logger = Factory.GetCurrentClassLogger();
        }

        public IEnumerable<Transaction> GetAll()
        {
            var TransactionList = this._TransactionClient.Fetch();

            if (TransactionList.Count > 0)
            {
                this._Logger.Info("Persisting new transactions...");
                this.RefreshTransactions(TransactionList);
            }

            return this._TransactionRepository.GetAll();
        }

        public IEnumerable<Transaction> GetMany(string Sku)
        {
            var Transactions = this.GetAll().Where(x => x.Sku.Equals(Sku)).ToList();
            var RateList = this._RateService.GetAll().ToList();

            foreach (var Transaction in Transactions.Where(x => !x.Currency.Equals(FinalCurrency)))
            {
                var RatesFromTransaction = RateList.Where(x => x.From.Equals(Transaction.Currency));

                if (RatesFromTransaction.Any())
                {
                    this.SetRatesFromCurrency(Transaction, RatesFromTransaction, RateList);
                }
                else
                {
                    var RatesToTransaction = RateList.Where(x => x.To.Equals(Transaction.Currency));

                    this.SetRatesToCurrency(Transaction, RatesToTransaction, RateList);
                }
            }

            return Transactions;
        }

        #region Private Methods
        private void RefreshTransactions(List<Transaction> Transactions)
        {
            foreach (var Transaction in Transactions)
            {
                this._TransactionRepository.Create(new Transaction()
                {
                    Sku = Transaction.Sku,
                    Amount = Transaction.Amount,
                    Currency = Transaction.Currency
                });
            }

            this._TransactionRepository.Commit();
        }

        private void SetRatesFromCurrency(Transaction TransactionEntity, IEnumerable<Rate> RatesFrom, List<Rate> RateList)
        {
            if (RatesFrom.Any(x => x.To.Equals(this.FinalCurrency)))
            {
                var Rate = RatesFrom.Single(x => x.To.Equals(this.FinalCurrency));

                TransactionEntity.Amount *= Rate.RateVal;
                TransactionEntity.Currency = Rate.To;
            }
            else
            {
                foreach (var SubRate in RatesFrom)
                {
                    var SubRatesFrom = RateList
                        .Where(x => x.From.Equals(SubRate.To) && !x.To.Equals(TransactionEntity.Currency));

                    if (SubRatesFrom.Any())
                    {
                        this.SetRatesFromCurrency(TransactionEntity, SubRatesFrom, RateList);
                    }
                    else
                    {
                        var SubRatesTo = RateList
                            .Where(x => x.To.Equals(SubRate.From) && !x.From.Equals(TransactionEntity.Currency));

                        this.SetRatesToCurrency(TransactionEntity, SubRatesTo, RateList);
                    }
                }
            }
        }

        private void SetRatesToCurrency(Transaction TransactionEntity, IEnumerable<Rate> RatesTo, List<Rate> RateList)
        {
            if (RatesTo.Any(x => x.From.Equals(this.FinalCurrency)))
            {
                var Rate = RatesTo.Single(x => x.From.Equals(this.FinalCurrency));

                TransactionEntity.Amount /= Rate.RateVal;
                TransactionEntity.Currency = Rate.From;
            }
            else
            {
                foreach (var SubRate in RatesTo)
                {
                    var SubRatesTo = RateList
                        .Where(x => x.To.Equals(SubRate.From) && !x.To.Equals(TransactionEntity.Currency));

                    if (SubRatesTo.Any())
                    {
                        this.SetRatesToCurrency(TransactionEntity, SubRatesTo, RateList);
                    }
                    else
                    {
                        var SubRatesFrom = RateList
                            .Where(x => x.From.Equals(SubRate.To) && !x.To.Equals(TransactionEntity.Currency));

                        this.SetRatesFromCurrency(TransactionEntity, SubRatesFrom, RateList);
                    }
                }
            }
        }
        #endregion
    }
}
