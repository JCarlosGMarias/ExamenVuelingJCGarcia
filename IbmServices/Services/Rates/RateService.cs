using IbmServices.Models;
using IbmServices.Repositories;
using IbmServices.Services.ResourceClients;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace IbmServices.Services.Rates
{
    public class RateService : IRateService
    {
        private readonly IRepository<Rate> _RateRepository;
        private readonly IResourceClient<Rate> _RateClient;
        private readonly Logger _Logger;

        public RateService(
            IRepository<Rate> RateRepository,
            IResourceClient<Rate> RateClient,
            LogFactory Factory)
        {
            this._RateRepository = RateRepository;
            this._RateClient = RateClient;
            this._Logger = Factory.GetCurrentClassLogger();
        }

        public IEnumerable<Rate> GetAll()
        {
            var Rates = this._RateClient.Fetch();

            if (Rates.Count > 0)
            {
                this._Logger.Info("Persisting new rates...");
                this.RefreshRates(Rates);
            }

            return this._RateRepository.GetAll();
        }

        #region Private Methods
        private void RefreshRates(List<Rate> Rates)
        {
            int NewRows = 0, UpdatedRows = 0;
            foreach (var Rate in Rates)
            {
                Rate RateEntity = this._RateRepository.GetAll()
                    .SingleOrDefault(x => x.From.Equals(Rate.From) && x.To.Equals(Rate.To));

                if (RateEntity == null)
                {
                    this._RateRepository.Create(new Rate()
                    {
                        From = Rate.From,
                        To = Rate.To,
                        RateVal = Rate.RateVal
                    });

                    NewRows++;
                }
                else
                {
                    RateEntity.RateVal = Rate.RateVal;
                    this._RateRepository.Update(RateEntity);
                    UpdatedRows++;
                }
            }

            this._Logger.Info($"{NewRows} rows will be added, {UpdatedRows} rows will be updated.");
            this._RateRepository.Commit();
        }
        #endregion
    }
}
