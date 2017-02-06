using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Ornament.Uow;

namespace Ornament.Uow.Web
{
    /// <summary>
    ///     用于启动默认Uow
    /// </summary>
    public class UowAttribute : ActionFilterAttribute
    {
        private readonly Type _uowType;

        public UowAttribute(Type uowType)
        {
            _uowType = uowType;
        }


        public virtual IUnitOfWork GetUnitOfWork(IServiceProvider context)
        {
            if (_uowType == null)
                return (IUnitOfWork) context.GetService(_uowType);
            return (IUnitOfWork) context.GetService(_uowType);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var uow = GetUnitOfWork(context.HttpContext.RequestServices);
            if (!uow.HadBegun)
                uow.Begin();
            base.OnActionExecuting(context);
        }

        /// <summary>
        ///     Called when [result executed].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var uow = GetUnitOfWork(context.HttpContext.RequestServices);
            try
            {
                if (context.Exception != null)
                    uow.Rollback();
                else
                    uow.Commit();
            }
            finally
            {
                uow.Close();
            }
            base.OnResultExecuted(context);
        }
    }
}