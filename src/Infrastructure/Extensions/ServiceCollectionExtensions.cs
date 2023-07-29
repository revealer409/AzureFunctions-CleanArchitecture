﻿using Infrastructure.Services.YourExternal;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using Infrastructure.Services.YourExternal;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            //services.AddDbContext();
            services.AddExternalService();
            return services;
        }

        /*private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TimeSlottingDb"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            return services;
        }*/

        private static IServiceCollection AddExternalService(this IServiceCollection services)
        {
            services.AddHttpClient<IYourExternalService, YourExternalService>("externalServiceClient", client =>
            {
                client.BaseAddress = new Uri("https://reqres.in/api");
                //add more configs as needed e.g. client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(YourExternalServicePolicies.GetRetryPolicy(async (_, _, _, _) => { }));
            return services;
        }
    }
}
