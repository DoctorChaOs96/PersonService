using Microsoft.Extensions.DependencyInjection;
using PersonService.BLL.Contract;
using PersonService.BLL.Service;

namespace PersonService
{
    public static class DependecyInjectionBLL
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, BLL.Service.PersonService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddDalServices();
        }
    }
}
