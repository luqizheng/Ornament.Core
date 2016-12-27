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
            return services.AddDbUow(() => new DbUow(connectionBuilder()));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="uowBuider"></param>
        /// <param name="isDefault">for dependcy dbuow class</param>
        /// <returns></returns>
        public static IServiceCollection AddDbUow<T>(this IServiceCollection services,
            Func<T> uowBuider, bool isDefault = false)
            where T : DbUow
        {

            services.AddScoped(sp => uowBuider());

            if (isDefault && typeof(T) != typeof(DbUow))
                services.AddScoped<DbUow>(sp =>
                {
                    var c = sp.GetService<T>();
                    return (DbUow)c;
                });
            return services;
        }
    }
}