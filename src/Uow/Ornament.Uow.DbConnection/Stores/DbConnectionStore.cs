using System;
using Ornament.Stores;
using Ornament.Uow;

namespace Ornament.Stores
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class DbConnectionStore<T, TId> : StoreBase<T, TId, DbUow>
        where T : class
        where TId : IEquatable<TId>

    {
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        protected DbConnectionStore(DbUow context) : base(context)
        {
        }
    }
}