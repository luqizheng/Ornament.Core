using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using Ornament.Uow;

namespace Ornament
{
    public static class ConfigExtender
    {
        public static NhUowBuilder MsSql2012(this IServiceCollection provider,
            Action<MsSqlConfiguration> settings, string factoryName = "default")
        {
            var dbSettining = MsSqlConfiguration.MsSql2012;
            settings(dbSettining);

            return Mssql(provider, dbSettining);
        }

        public static NhUowBuilder MsSql2008(this IServiceCollection provider, Action<MsSqlConfiguration> settings,
            string name = "default")
        {
            var dbSettining = MsSqlConfiguration.MsSql2008;
            settings(dbSettining);
            return Mssql(provider, dbSettining);
        }

        private static NhUowBuilder Mssql(IServiceCollection provider, MsSqlConfiguration dbSetting)
        {
#if DEBUG
            if (dbSetting == null) throw new ArgumentNullException(nameof(dbSetting));

#endif

            var config = Fluently.Configure();
            config.Database(dbSetting);
            var ins = new NhUowBuilder(config);
            provider.AddSingleton(ins);
            provider.AddScoped(s => (NhUow) s.GetService<NhUowBuilder>().Create());

            return ins;
        }
    }
}