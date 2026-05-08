using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConwaysGoL.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConwaysGoL.MauiAppProject
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<CGoLBackgroundService>();

            return services;
        }
    }
}
