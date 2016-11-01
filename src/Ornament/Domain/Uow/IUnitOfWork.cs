using System;

namespace Ornament.Domain.Uow
{
    /// <summary>
    ///     Interface IUnitOfWork
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Begins this instance.
        /// </summary>
        void Begin();

        /// <summary>
        ///     Rollbacks this instance.
        /// </summary>
        void Rollback();


        /// <summary>
        ///     Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        void Close();
        /// <summary>
        /// Gets a value indicating whether had call begin method
        /// </summary>
        /// <value><c>true</c> if [had begun]; otherwise, <c>false</c>.</value>
        bool HadBegun { get; }
    }

    
}