using System;
using System.Data;
using Ornament.Domain.Uow;

namespace Ornament.Uow.Web
{
    /// <summary>
    /// </summary>
    public class DatabaseUowAttribute : UowAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="uowType"></param>
        public DatabaseUowAttribute(Type uowType) : base(uowType)
        {
        }

        /// <summary>
        /// </summary>
        public bool EnableTransaction { get; set; }

        /// <summary>
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="UowExcepton"></exception>
        public override IUnitOfWork GetUnitOfWork(IServiceProvider context)
        {
            var d = base.GetUnitOfWork(context);
            var uow = d as DbUow;
            if (uow == null)
                throw new UowExcepton("DatabaseUow should handle DbUow only.");
            uow.IsolationLevel = IsolationLevel;
            uow.EnableTransaction = EnableTransaction;
            return uow;
        }
    }
}