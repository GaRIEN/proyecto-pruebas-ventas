namespace Ventas.Api.Endpoints.Base
{
    public static class EndpointsRegistrar
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapClientsEndpoints();
        }
    }
}
