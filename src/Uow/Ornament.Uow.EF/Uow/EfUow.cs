using System;
using Microsoft.EntityFrameworkCore;
using Ornament.Domain.Uow;

namespace Ornament.Uow
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class EfUow<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public EfUow(TDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
        }


        /// <summary>
        /// </summary>
        public bool EnableTransaction { get; set; }

        /// <summary>
        /// </summary>
        public DbContext Context { get; }

        /// <summary>
        /// </summary>
        public bool HadBegun { get; private set; }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// </summary>
        public void Begin()
        {
            Context.Database.AutoTransactionsEnabled = EnableTransaction;

            HadBegun = true;
        }

        /// <summary>
        /// </summary>
        public void Rollback()
        {
            if (EnableTransaction)
                Context.Database.RollbackTransaction();
        }

        /// <summary>
        /// </summary>
        public void Commit()
        {
            if (EnableTransaction)
                Context.Database.CommitTransaction();
            Context.SaveChanges();
        }

        /// <summary>
        /// </summary>
        public void Close()
        {
        }
    }
}