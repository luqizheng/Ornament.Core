using System;
using Ornament.Domain.Stores;
using Ornament.Uow;

namespace Ornament.Stores
{
    public abstract class DbConnectionStore<T, TID> : StoreBase<T, TID, DbUow>
        where T : class
        where TID : IEquatable<TID>

    {
        protected DbConnectionStore(DbUow context) : base(context)
        {
        }
    }
}
