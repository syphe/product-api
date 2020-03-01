using Microsoft.Extensions.DependencyInjection;
using ProductApi.BusinessLogic.Orchestrators;

namespace ProductApi.BusinessLogic
{
    /// <summary>
    /// Business Logic services required to be added to the DI container.
    /// </summary>
    public static class BusinessLogicServices
    {
        /// <summary>
        /// Adds the required Business Logic services into the DI container specified.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureBusinessLogicServices(IServiceCollection services)
        {
            services.AddTransient<IOrderOrchestrator, OrderOrchestrator>();
        }
    }
}
