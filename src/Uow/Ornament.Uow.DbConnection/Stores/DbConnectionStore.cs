using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ornament.Domain.Stores;
using Ornament.Uow.DbConnection;

namespace Ornament.Uow.Stores
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
