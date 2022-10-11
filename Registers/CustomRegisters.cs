using kimberly_ws.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace kimberly_ws.Registers
{
    public static class CustomRegisters
    {
        /// <summary>
        /// Registro de los servicios/repositorios personalizados
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection addCustomRegisters(this IServiceCollection services)
        {
            services.AddTransient(typeof(LoginRepository));
            services.AddTransient(typeof(MainRepository));
            return services;
        }
    }
}
