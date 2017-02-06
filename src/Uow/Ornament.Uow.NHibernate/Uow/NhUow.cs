using System;
using NHibernate;
namespace Ornament.Uow
{
    /// <summary>
    ///     Class NhUow.
    /// </summary>
    /// <seealso cref="Ornament.Domain.Uow.IUnitOfWork" />
    public class NhUow : Ornament.Uow.IUnitOfWork
    {
        /// <summary>
        ///     The session factory
        /// </summary>
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        ///     The use transaction
        /// </summary>
        private readonly bool _useTransaction;

        /// <summary>
        ///     The disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     The session
        /// </summary>
        private ISession _session;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NhUow" /> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        /// <param name="useTransaction">if set to <c>true</c> [use transaction].</param>
        /// <exception cref="ArgumentNullException"></exception>
        public NhUow(ISessionFactory sessionFactory, bool useTransaction = false)
        {
            if (sessionFactory == null)
                throw new ArgumentNullException(nameof(sessionFactory));
            _sessionFactory = sessionFactory;
            _useTransaction = useTransaction;
        }

        /// <summary>
        ///     Gets the session.
        /// </summary>
        /// <value>The session.</value>
        /// <exception cref="UowExcepton">UowProvider is not open.</exception>
        public ISession Session
        {
            get
            {
                if (_session == null)
                    throw new UowExcepton("UowProvider is not open.");
                return _session;
            }
        }

        public bool HadBegun => _session != null;

        /// <summary>
        ///     Begins this instance.
        /// </summary>
        public void Begin()
        {
            ThrowIfDisposed();


            if (_session == null)
                _session = _sessionFactory.OpenSession();
            if (_useTransaction)
                _session.BeginTransaction();
        }

        /// <summary>
        ///     Commits this instance.
        /// </summary>
        /// <exception cref="UowExcepton">UowProvider is not opened.</exception>
        public void Commit()
        {
            ThrowIfDisposed();
            if (_session == null)
                throw new UowExcepton("UowProvider is not opened.");
            if (_useTransaction)
                _session.Transaction.Commit();
            _session.Flush();
        }

        /// <summary>
        ///     Rollbacks this instance.
        /// </summary>
        /// <exception cref="UowExcepton">UowProvider is not opened.</exception>
        public void Rollback()
        {
            ThrowIfDisposed();
            if (_session == null)
                throw new UowExcepton("UowProvider is not opened.");
            _session.Transaction.Rollback();
        }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        /// <exception cref="UowExcepton">UowProvider is not opened.</exception>
        public void Close()
        {
            ThrowIfDisposed();
            if (_session == null)
                throw new UowExcepton("UowProvider is not opened.");
            _session.Close();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
            if (_session != null && _session.IsConnected)
            {
                _session.Close();
                _session.Dispose();
            }
        }

        /// <summary>
        ///     Throws if disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}