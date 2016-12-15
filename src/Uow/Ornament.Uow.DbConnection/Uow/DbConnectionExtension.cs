using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Ornament.Domain.Uow;

namespace Ornament.Uow
{
    public class UowSqSetting
    {
        public string ConnectionString { get; set; }
        public bool BeginTranscation { get; set; } = false;
        public IsolationLevel? IsolationLevel { get; set; }

        public Func<string, IDbConnection> CreateConnection { get; set; }
    }

    public static class DbConnectionExtension
    {
        public static IServiceCollection AddDbUow(this IServiceCollection services,
          Action<UowSqSetting> configFactory)
        {
            return services.AddDbUow<DbUow>(configFactory);
        }

        public static IServiceCollection AddDbUow<T>(this IServiceCollection services, Action<UowSqSetting> configFactory)
            where T : DbUow
        {
            var setting = new UowSqSetting();
            configFactory(setting);
            if (setting.CreateConnection == null)
                throw new UowExcepton("Please setting CreateConnection action");
            services.AddScoped(sp => new DbUow(setting.CreateConnection(setting.ConnectionString))
            {
                IsolationLevel = setting.IsolationLevel,
                BeginTranscation = setting.BeginTranscation,
            });
            return services;

        }
    }
}