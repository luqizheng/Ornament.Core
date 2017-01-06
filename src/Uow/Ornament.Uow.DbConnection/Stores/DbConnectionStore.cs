using System;
using Ornament.Domain.Stores;
using Ornament.Uow;

namespace Ornament.Stores
{
    public abstract class DbConnectionStore<T, TId> : StoreBase<T, TId, DbUow>
        where T : class
        where TId : IEquatable<TId>

    {
        protected DbConnectionStore(DbUow context) : base(context)
        {
        }
    }
}