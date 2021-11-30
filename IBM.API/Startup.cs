using IBM.Application.Interfaces;
using IBM.Application.Services;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using IBM.Infrastructure.Data;
using IBM.Infrastructure.ExternalComunication;
using IBM.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.API
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
            var uri = Configuration.GetSection("HerokuAPI");

            services.AddHttpClient();
            services.AddControllers();

            services.AddDbContext<IBMContext>(
                 m => m.UseSqlServer(Configuration.GetConnectionString("IBMconnectionString")), ServiceLifetime.Singleton);

            services.AddHttpClient<IComunicationRepository, ExternalApiRepository>(c => c.BaseAddress = new Uri(uri.Value));

            services.AddMvc();

            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IRateService, RateService>();

            services.AddSingleton<IRounder, BankersRounding>();

            services.AddTransient<IRateRepository, RateRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
