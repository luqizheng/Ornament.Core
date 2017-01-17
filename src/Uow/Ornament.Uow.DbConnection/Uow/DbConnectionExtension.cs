using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Ornament.Uow
{
    /// <summary>
    /// </summary>
    public static class DbConnectionExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionBuilder"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbUow(this IServiceCollection services,
            Func<IDbConnection> connectionBuilder)
        {
            return services.AddDbUow(() => new DbUow(connectionBuilder()));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="uowBuider"></param>
        /// <param name="isDefault">if true, use Resolve(typeof(DbUow)) return this Uow.</param>
        /// <returns></returns>
        public static IServiceCollection AddDbUow<T>(this IServiceCollection services,
            Func<T> uowBuider, bool isDefault = false)
            where T : DbUow
        {
            services.AddScoped(sp => uowBuider());

            if (isDefault && (typeof(T) != typeof(DbUow)))
                services.AddScoped(sp =>
                {
                    var c = sp.GetService<T>();
                    return (DbUow) c;
                });
            return services;
        }
    }
}