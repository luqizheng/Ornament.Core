using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Ornament.Domain.Uow;
using Ornament.NHibernate.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Ornament.NHibernate
{
    public static class ConfigExtender
    {
        public static NhUowFactory MsSql2012(this IServiceCollection provider,
            Action<MsSqlConfiguration> settings, string factoryName = "default")
        {
            var dbSettining = MsSqlConfiguration.MsSql2012;
            settings(dbSettining);

            return Mssql(provider, dbSettining);
        }

        public static NhUowFactory MsSql2008(this IServiceCollection provider, Action<MsSqlConfiguration> settings,
            string name = "default")
        {
            var dbSettining = MsSqlConfiguration.MsSql2008;
            settings(dbSettining);
            return Mssql(provider, dbSettining);
        }

        private static NhUowFactory Mssql(IServiceCollection provider, MsSqlConfiguration dbSetting)
        {
#if DEBUG
            if (dbSetting == null) throw new ArgumentNullException(nameof(dbSetting));

#endif
           
            var config = Fluently.Configure();
            config.Database(dbSetting);
            var ins = new NhUowFactory(config);
            provider.AddSingleton<NhUowFactory>(ins);
            provider.AddScoped<NhUow>(s => (NhUow)s.GetService<NhUowFactory>().Create());
            provider.AddScoped<IUnitOfWork>(s => s.GetRequiredService<NhUow>() as IUnitOfWork);
            return ins;
        }
    }
}