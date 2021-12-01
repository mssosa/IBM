using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace IBM.API.Models
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Examen Vueling - Matias Sosa {groupName}",
                    Version = groupName,
                    Description = "International Bussines Men - API \n Para probar correctamente la api se recomienda: \n1) Ingresar primeramente en .../api/Transactions.\n2) Ingresar en .../api/Rates. \n3) Luego de esto se podrán probar las funcionalidades OFFLINE. \n 4) Para consultar las transacciones almacenadas en la base de datos.../api/Transactions/offline \n 5) Para consultar los rates almacenados en la base de datos .../api/Rates/offline \n 6) Se puede consultar el listado de productos ingesando en .../api/Products \n 7) Tambien consultar un producto en particular .../api/Products/<skuBuscado> \n8) Tambien se puede testear el lanzamiento forzado de un error en ..api/testerror \n\nGracias por su tiempo.",
                    Contact = new OpenApiContact
                    {
                        Name = "Matias Sosa LinkedIn ",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/lic-matias-sosa/"),
                    }
                });
            });
        }
    }
}
