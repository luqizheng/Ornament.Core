using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ornament.Uow
{
    public static class EfConnectionExtension
    {
        public static IServiceCollection AddEfUow<TDbContext>(this IServiceCollection services,
            Action<DbContextOptions<TDbContext>> dbContextOptions)
            where TDbContext : DbContext
        {
            services.AddSingleton(sp =>
            {
                var result = new DbContextOptions<TDbContext>();
                dbContextOptions(result);
                return result;
            });
            services.AddTransient<TDbContext>();
            services.AddScoped(sp =>
            {
                var dbContext = sp.GetRequiredService<TDbContext>();
                var db = new EfUow<TDbContext>(dbContext);
                return db;
            });
            return services;
        }
    }
}