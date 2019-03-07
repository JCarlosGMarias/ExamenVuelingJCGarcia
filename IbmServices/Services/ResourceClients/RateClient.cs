using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using IbmServices.Configs;
using IbmServices.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;

namespace IbmServices.Services.ResourceClients
{
    public class RateClient : IResourceClient<Rate>
    {
        private readonly Logger _Logger;
        private readonly string _Uri;

        public RateClient(IOptions<ExternalResourcesModel> Settings, LogFactory Factory)
        {
            this._Uri = Settings.Value.RatesUri;
            this._Logger = Factory.GetCurrentClassLogger();
        }

        public List<Rate> Fetch()
        {
            var Result = new List<Rate>();
            try
            {
                var Client = WebRequest.Create(this._Uri);

                this._Logger.Info("Fetching data from external resource...");
                using (var Response = Client.GetResponse() as HttpWebResponse)
                {
                    using (var Reader = new StreamReader(Response.GetResponseStream()))
                    {
                        var DataStr = Reader.ReadToEnd();

                        Result.AddRange(JsonConvert.DeserializeObject<List<Rate>>(DataStr));
                        this._Logger.Info($"{Result.Count} rows retrieved.");
                    }
                }
            }
            catch (Exception ex)
            {
                this._Logger.Error(ex);
            }

            return Result;
        }
    }
}
