using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Ornament.Uow;

namespace Ornament.Stores
{
    public class NhStore<T, TId, TUow> : StoreBase<T, TId, TUow>
        where TUow : NhUow
        where TId : IEquatable<TId>
        where T : class

    {
        protected NhStore(TUow context) : base(context)
        {
        }


        protected ISession Context => ((NhUow) Context).Session;


        public IQueryable<T> Entities => Context.Query<T>();


        public void SaveOrUpdate(T t)
        {
            Context.SaveOrUpdate(t);
        }

        public void Delete(T t)
        {
            Context.Delete(t);
        }

        public T Get(TId id)
        {
            return Context.Get<T>(id);
        }

        public T Load(TId id)
        {
            return Context.Load<T>(id);
        }

        public T Merge(T t)
        {
            return Context.Merge(t);
        }

        public void Save(T t)
        {
            Context.Save(t);
        }

        public void Update(T t)
        {
            Context.Update(t);
        }

        public void SaveChange()
        {
            Context.Flush();
        }
    }
}