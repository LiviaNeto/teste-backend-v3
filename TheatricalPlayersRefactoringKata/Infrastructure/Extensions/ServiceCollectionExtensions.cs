using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IPlayTypeRepository, PlayTypeConfiguration>();
            services.AddSingleton<IPlayRepository, InMemoryPlayRepository>();
            services.AddSingleton<IPerformanceRepository, InMemoryPerformanceRepository>();
            services.AddSingleton<IInvoiceRepository, InMemoryInvoiceRepository>();

            return services;
        }
    }
}