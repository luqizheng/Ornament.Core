using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Ornament.Domain.Entities;
using Ornament.Domain.Stores;
using Ornament.NHibernate.Uow;

namespace Ornament.NHibernate
{
    public abstract class Store<T, TId> : StoreBase<T, TId, NhUow>,
            IDisposable
        where T : EntityWithTypedId<TId>
        where TId : IEquatable<TId>

    {
        protected Store(NhUow context) : base(context)
        {
        }

        protected ISession Context
        {
            get { return Uow.Session; }
        }


        public override IQueryable<T> Entities => Context.Query<T>();



        public override void SaveOrUpdate(T t)
        {
            Context.SaveOrUpdate(t);
        }

        public override void Delete(T t)
        {
            Context.Delete(t);
        }

        public override T Get(TId id)
        {
            return Context.Get<T>(id);
        }

        public override T Load(TId id)
        {
            return Context.Load<T>(id);
        }

        public override T Merge(T t)
        {
            return Context.Merge(t);
        }

        public override void Save(T t)
        {
            Context.Save(t);
        }

        public override void Update(T t)
        {
            Context.Update(t);
        }

        public override void SaveChange()
        {
            Context.Flush();
        }

    }
}