using System;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Ornament.Uow;

namespace Ornament
{
    public abstract class NhUowFactoryBase
    {
        private ISessionFactory _sessionFactory;
        private readonly FluentConfiguration _config;
        private readonly object _sessionFactoryLocke = 1;
        public NhUowFactoryBase(FluentConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _config = config;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork Create()
        {
            if (_sessionFactory == null)
            {
                lock (_sessionFactoryLocke)
                {
                    if (_sessionFactory == null)
                        _sessionFactory = _config.BuildSessionFactory();
                }
            }
            return Create(_sessionFactory);
        }

        public NhUowFactoryBase AddAssemblyOf<T>()
        {
            _config.Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<T>());
            return this;
        }

        public NhUowFactoryBase AddAssemblyOf(Type typeOfMappingClass)
        {
            _config.Mappings(m =>
                m.FluentMappings.AddFromAssembly(typeOfMappingClass.Assembly));

            return this;
        }

        public NhUowFactoryBase AddType(Type t)
        {
            _config.Mappings(m =>
                m.FluentMappings.Add(t));

            return this;
        }

        public NhUowFactoryBase UpdateSchema(bool updateDbStructure)
        {
            var config = _config.BuildConfiguration();
            var export = new SchemaUpdate(config);
            export.Execute(true, true);
            return this;
        }

        protected abstract IUnitOfWork Create(ISessionFactory factory);




    }
    public class NhUowBuilder : NhUowFactoryBase
    {
        public NhUowBuilder(FluentConfiguration config) : base(config)
        {
        }

        protected override IUnitOfWork Create(ISessionFactory factory)
        {
            return new NhUow(factory);
        }
    }

    public class NhSessionlessFactory : NhUowFactoryBase
    {
        public NhSessionlessFactory(FluentConfiguration config) : base(config)
        {
        }

        protected override IUnitOfWork Create(ISessionFactory factory)
        {
            return new NhUowStateless(factory);
        }
    }
}