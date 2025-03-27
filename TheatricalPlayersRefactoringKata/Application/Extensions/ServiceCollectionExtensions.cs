using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

namespace TheatricalPlayersRefactoringKata.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<PlayTypeService>();
            services.AddScoped<PerformanceService>();
            services.AddScoped<IPlayService, PlayService>();
            services.AddScoped<IPerformanceRepository, InMemoryPerformanceRepository>();
            return services;
        }   
    }
}