using System;
using System.Linq;
using Ornament.Uow;

namespace Ornament.Stores
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IStore<T, TId> : IDisposable
        where T : class
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// get unit of work instance.
        /// </summary>
        IUnitOfWork Uow { get; }

        T Get(TId id);

        void Delete(TId id);
        
    }
}