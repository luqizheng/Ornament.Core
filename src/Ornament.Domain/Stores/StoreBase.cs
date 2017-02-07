using System;
using Ornament.Uow;

namespace Ornament.Stores
{
    /// <summary>
    ///     Class StoreBase.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId">The type of the t identifier.</typeparam>
    /// <typeparam name="TUnitOfWork"></typeparam>
    /// <seealso>
    ///     <cref>IStore{T,TId}</cref>
    /// </seealso>
    public abstract class StoreBase<T, TId, TUnitOfWork> : IStore<T, TId>
        where T : class
        where TId : IEquatable<TId>
        where TUnitOfWork : IUnitOfWork
    {
        private readonly TUnitOfWork _uow;

        /// <summary>
        ///     The disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     The self handler uow
        /// </summary>
        private bool _selfHandlerUow;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see>
        ///         <cref>StoreBase{T, TId}</cref>
        ///     </see>
        ///     class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected StoreBase(TUnitOfWork context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _uow = context;
        }

        /// <summary>
        ///     Gets the uow provider.
        /// </summary>
        public virtual TUnitOfWork Uow
        {
            get
            {
                if (!_uow.HadBegun)
                {
                    _uow.Begin();
                    _selfHandlerUow = true;
                }
                else
                {
                    _selfHandlerUow = false;
                }
                return _uow;
            }
        }

        /// <summary>
        ///     Gets the uow provider.
        /// </summary>
        /// <value>The uow provider.</value>
        IUnitOfWork IStore<T, TId>.Uow => Uow;


        public virtual void Dispose()
        {
            if (_selfHandlerUow)
                Uow.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Throws if disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        protected virtual void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            _disposed = true;
        }

        
        public abstract T Get(TId id);

        public abstract void Delete(TId id);

        
    }
}