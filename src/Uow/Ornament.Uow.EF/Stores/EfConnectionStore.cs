using System;
using Microsoft.EntityFrameworkCore;
using Ornament.Domain.Stores;
using Ornament.Uow;

namespace Ornament.Stores
{
    public abstract class EfConnectionStore<T, TId, TDbContext> : StoreBase<T, TId, EfUow<TDbContext>>
        where T : class
        where TId : IEquatable<TId>
        where TDbContext : DbContext
    {
        protected EfConnectionStore(EfUow<TDbContext> context) : base(context)
        {
        }

        
    }
}
