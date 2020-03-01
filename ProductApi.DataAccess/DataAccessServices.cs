using Microsoft.Extensions.DependencyInjection;
using ProductApi.DataAccess.Repository;

namespace ProductApi.DataAccess
{
    /// <summary>
    /// DataAccess Services required to be added to the DI container.
    /// </summary>
    public static class DataAccessServices
    {
        /// <summary>
        /// Adds the required DataAccess services into the DI container specified.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> collection to add services to.</param>
        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<>), typeof(MemoryRepository<>));
        }
    }
}
