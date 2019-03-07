using IbmServices.Configs;
using IbmServices.Models;
using IbmServices.Repositories;
using IbmServices.Services.Rates;
using IbmServices.Services.ResourceClients;
using IbmServices.Services.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExamenVuelingJCGarcia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration's injection
            services.Configure<ExternalResourcesModel>(Configuration.GetSection("ExternalResources"));

            // Logging injection
            services.AddSingleton(NLog.LogManager.LoadConfiguration("NLog.config"));

            // DBContext inyection
            services.AddDbContext<IbmContext>(opts => opts.UseInMemoryDatabase("IbmDB"));

            // Repositories' injection
            services.AddScoped<IRepository<Rate>, RatesRepository>();
            services.AddScoped<IRepository<Transaction>, TransactionsRepository>();

            // Services' injection
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IResourceClient<Rate>, RateClient>();
            services.AddScoped<IResourceClient<Transaction>, TransactionClient>();

            // Model's injection
            services.AddScoped<BaseEntity, Rate>();
            services.AddScoped<BaseEntity, Transaction>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
