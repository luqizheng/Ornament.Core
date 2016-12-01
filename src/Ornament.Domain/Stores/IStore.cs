using System;
using System.Linq;
using Ornament.Domain.Uow;

namespace Ornament.Domain.Stores
{
    public interface IStore<T, TId, TUow> : IDisposable
        where T : class
        where TId : IEquatable<TId>
        where TUow : IUnitOfWork
    {
        IQueryable<T> Entities { get; }
        void Add(T t);
        void Update(T t);
        void Delete(T t);

        T Get(TId id);
    }
}