﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ornament.Domain.Stores;
using Ornament.Stores;
using Ornament.Uow;

namespace Ornament.Stores
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class EfConnectionStore<T, TId, TDbContext> : StoreBase<T, TId, EfUow<TDbContext>>
        where T : class
        where TId : IEquatable<TId>
        where TDbContext : DbContext
    {
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        protected EfConnectionStore(EfUow<TDbContext> context) : base(context)
        {
        }

        public DbSet<T> Entities => this.Uow.Context.Set<T>();
    }
}