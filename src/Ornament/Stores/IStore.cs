using System;
using System.Linq;
using Ornament.Domain.Entities;

namespace Ornament.Domain.Stores
{
    public interface IStore<T, TId>: IDisposable
        where T : EntityWithTypedId<TId>
        where TId : IEquatable<TId>
    {
        IQueryable<T> Entities { get; }

        void SaveOrUpdate(T t);
        void Update(T t);

        T Merge(T t);

        void Save(T t);

        void Delete(T t);

        T Get(TId id);

        T Load(TId id);

        void SaveChange();
    }
}