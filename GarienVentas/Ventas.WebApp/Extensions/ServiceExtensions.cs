using ApiClients.Base;
using ApiClients.Ventas;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Ventas.WebApp.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services, IConfiguration config)
        {
            // 1. URL Base desde appsettings
            var apiBaseUrl = config["AppSettings:VentasApiBase"]
                ?? throw new InvalidOperationException("VentasApiBase no configurado.");

            // 2. Handler compartido para el Bypass de SSL (útil en desarrollo)
            var commonHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            // 3. Acción de configuración común para el HttpClient
            Action<HttpClient> configureClient = client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            };

            // 4. REGISTRO DE CLIENTES
            // Al usar .AddHttpClient seguido de .ConfigurePrimaryHttpMessageHandler, 
            // aplicas la configuración a cada uno sin repetir el bloque SSL.

            services.AddHttpClient<IUsuariosApiClient, UsuariosApiClient>(configureClient)
                .ConfigurePrimaryHttpMessageHandler(() => commonHandler);


            // Si llegaras a usar AuthTokenHandler o TenantHandler en el futuro, solo agregas:
            // .AddHttpMessageHandler<AuthTokenHandler>() 

            return services;
        }
    }
}