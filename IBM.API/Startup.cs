using IBM.API.Exceptions;
using IBM.API.Models;
using IBM.Application.Interfaces;
using IBM.Application.Services;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using IBM.Infrastructure.Data;
using IBM.Infrastructure.ExternalProviders;
using IBM.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace IBM.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var uri = Configuration.GetSection("HerokuAPI");

            services.AddHttpClient();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            SwaggerConfiguration.AddSwagger(services);

            services.AddDbContext<IBMContext>(
                 m => m.UseSqlServer(Configuration.GetConnectionString("IBMconnectionString")), ServiceLifetime.Singleton);

            services.AddHttpClient<IExternalProviderRepository, ExternalProviderRepository>(c => c.BaseAddress = new Uri(uri.Value));

            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddSingleton<IRounder, BankersRounding>();

            services.AddTransient<IRateRepository, RateRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddSingleton<IRateOperationWith, RateOperationWith>();
            services.AddSingleton<ICurrencyConverter, CurrencyConverter>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBMContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                context.Database.Migrate();


            }
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IBM.MSS API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
