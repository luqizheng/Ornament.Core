using System;
using NHibernate;
using Ornament.Domain.Uow;

namespace Ornament.NHibernate.Uow
{
    public class NhUowStateless : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private IStatelessSession _session;
        private bool _disposed = false;
        private readonly bool _useTransaction;
        public NhUowStateless(ISessionFactory sessionFactory, bool useTransaction = true)
        {
            if (sessionFactory == null) throw new ArgumentNullException(nameof(sessionFactory));
            _sessionFactory = sessionFactory;
            _useTransaction = useTransaction;
        }
        public void Begin()
        {
            ThrowIfDisposed();
            if (_sessionFactory != null)
                _session = _sessionFactory.OpenStatelessSession();
            if (_useTransaction)
            {
                _session.BeginTransaction();
            }
        }

        public void Commit()
        {
            ThrowIfDisposed();
            if (_session == null)
            {
                throw new UowExcepton("UowProvider is not opened.");
            }
            if (_useTransaction)
            {
                _session.Transaction.Commit();
            }
        }

        public void Rollback()
        {
            ThrowIfDisposed();
            if (_session == null)
            {
                throw new UowExcepton("UowProvider is not opened.");
            }
            _session.Transaction.Rollback();
        }

        public void Close()
        {
            ThrowIfDisposed();
            if (_session == null)
            {
                throw new UowExcepton("UowProvider is not opened.");
            }
            _session.Close();
        }

        public bool HadBegun => _session != null;

        public void Dispose()
        {
            ThrowIfDisposed();
            _disposed = true;
            if (this._session.IsConnected)
                this._session.Close();
        }

        public IStatelessSession Session
        {
            get
            {
                if (_session == null)
                {
                    throw new UowExcepton("UowProvider is not open.");
                }
                return _session;
            }
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}