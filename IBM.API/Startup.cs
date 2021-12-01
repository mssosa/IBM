using IBM.Application.Interfaces;
using IBM.Application.Services;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using IBM.Infrastructure.Data;
using IBM.Infrastructure.ExternalProviders;
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
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddDbContext<IBMContext>(
                 m => m.UseSqlServer(Configuration.GetConnectionString("IBMconnectionString")), ServiceLifetime.Singleton);

            services.AddHttpClient<IExternalProviderRepository, ExternalProviderRepository>(c => c.BaseAddress = new Uri(uri.Value));

            services.AddMvc();

            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddSingleton<IRounder, BankersRounding>();

            services.AddTransient<IRateRepository, RateRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBMContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                context.Database.Migrate();
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
