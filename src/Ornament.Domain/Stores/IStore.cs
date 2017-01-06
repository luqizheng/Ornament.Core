using System;
using System.Linq;
using Ornament.Domain.Uow;

namespace Ornament.Domain.Stores
{
    public interface IStore<T, TId> : IDisposable
        where T : class
        where TId : IEquatable<TId>
    {
        IUnitOfWork Uow { get; }
    }
}