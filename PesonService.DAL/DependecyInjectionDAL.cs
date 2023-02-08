using Microsoft.Extensions.DependencyInjection;
using PesonService.DAL;
using PesonService.DAL.Contract;
using PesonService.DAL.Entity;
using PesonService.DAL.Repository;

namespace PersonService
{
    public static class DependecyInjectionDAL
    {
        public static void AddDalServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<PersonEntity>, DefaultRepository<PersonEntity>>();
        }
    }
}
