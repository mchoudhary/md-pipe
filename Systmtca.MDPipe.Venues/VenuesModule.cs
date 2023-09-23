using Microsoft.Extensions.DependencyInjection;

namespace Systmtca.MDPipe.Venues
{
    public static class VenuesModule
    {
        public static void RegisterVenuesModuleServices(this IServiceCollection services)
        {
            services.AddSingleton<IVenueWebsocketFeedFactory, VenueWebsocketFeedFactory>();
        }
    }
}
