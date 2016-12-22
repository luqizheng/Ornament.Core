using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Ornament.Uow
{
    public static class DbConnectionExtension
    {
        public static IServiceCollection AddDbUow(this IServiceCollection services,
            Func<IDbConnection> connectionBuilder)
        {
            return services.AddDbUow<DbUow>(connectionBuilder, true);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionBuilder"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbUow<T>(this IServiceCollection services,
            Func<IDbConnection> connectionBuilder, bool isDefault = false)
            where T : DbUow
        {
            services.AddScoped(sp =>
            {
                var c = sp.GetRequiredService<T>();
                c.Connection = connectionBuilder();
                return c;
            });
            if (isDefault)
                services.AddScoped(sp =>
                {
                    var c = sp.GetRequiredService<T>();
                    c.Connection = connectionBuilder();
                    return (DbUow) c;
                });
            return services;
        }
    }
}