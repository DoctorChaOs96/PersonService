﻿using AutoMapper;
using PersonService.Configuration;
using PersonService.Helpers;
using PesonService.DAL;

namespace PersonService
{
    public static class DependecyInjection
    {
        public static void RegisterDependencies(WebApplicationBuilder builder)
        {
            var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>();

            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddSingleton<ISecurityHelper, SecurityHelper>();

            builder.Services.AddBllServices();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            var profiles = typeof(DependecyInjection).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            var serviceProfiles = typeof(BLL.Service.PersonService).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            var coreProfiles = typeof(PersonServiceDbContext).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));

            var allProfiles = profiles.Concat(serviceProfiles).Concat(coreProfiles);

            services.AddScoped(x =>
            {
                var configuration = new MapperConfiguration(configuration =>
                {
                    foreach (var profile in allProfiles)
                    {
                        configuration.AddProfile(ActivatorUtilities.CreateInstance(x, profile) as Profile);
                    }
                });

#if DEBUG
                configuration.AssertConfigurationIsValid();
#endif

                return configuration.CreateMapper();
            });
        }
    }
}
