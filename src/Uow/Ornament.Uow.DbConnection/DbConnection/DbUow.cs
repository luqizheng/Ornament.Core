using System;
using System.Data;
using Ornament.Uow;

namespace Ornament.Uow.DbConnection
{
    public class DbUow : IUnitOfWork
    {
        private readonly bool _isTranscation;
        private bool _selfClose;

        public DbUow(IDbConnection connection, bool isTranscation)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            Connection = connection;
            _isTranscation = isTranscation;
        }

        public IsolationLevel? IsolationLevel { get; set; }

        public IDbConnection Connection { get; }

        public IDbTransaction DbTransaction { get; private set; }

        public bool HadBegun => Connection.State == ConnectionState.Open;

        public void Dispose()
        {
            if ((Connection.State != ConnectionState.Closed) && _selfClose)
                Connection.Close();
        }

        public void Begin()
        {
            if (HadBegun) return;
            _selfClose = true;
            Connection.Open();
            if (!_isTranscation) return;
            DbTransaction = IsolationLevel != null
                ? Connection.BeginTransaction(IsolationLevel.Value)
                : Connection.BeginTransaction();
        }

        public void Rollback()
        {
            DbTransaction?.Rollback();
        }

        public void Commit()
        {
            DbTransaction?.Commit();
        }

        public void Close()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}