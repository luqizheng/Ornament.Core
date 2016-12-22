using System;
using System.Data;
using Ornament.Domain.Uow;

namespace Ornament.Uow.Web
{
    public class DatabaseUowAttribute : UowAttribute
    {
        public DatabaseUowAttribute(Type uowType) : base(uowType)
        {
        }

        public bool EnableTransaction { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public override IUnitOfWork GetUnitOfWork(IServiceProvider context)
        {
            var d = base.GetUnitOfWork(context);
            var uow = d as DbUow;
            if (uow == null)
                throw new UowExcepton("DatabaseUow should handle DbUow only.");
            uow.IsolationLevel = IsolationLevel;
            uow.EnableTranscation = EnableTransaction;
            return uow;
        }
    }
}