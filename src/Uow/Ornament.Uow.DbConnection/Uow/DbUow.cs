using System;
using System.Data;
using Ornament.Uow;

namespace Ornament.Uow
{
    /// <summary>
    /// </summary>
    public class DbUow : IUnitOfWork
    {
        /// <summary>
        /// </summary>
        private bool _selfClose;

        /// <summary>
        /// </summary>
        /// <param name="connection"></param>
        public DbUow(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            Connection = connection;
        }


        /// <summary>
        /// </summary>
        public bool EnableTransaction { get; set; }

        /// <summary>
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// </summary>
        public IDbConnection Connection { get; internal set; }

        /// <summary>
        /// </summary>
        public IDbTransaction DbTransaction { get; private set; }

        /// <summary>
        /// </summary>
        public bool HadBegun => Connection.State == ConnectionState.Open;

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            if ((Connection.State != ConnectionState.Closed) && _selfClose)
                Connection.Close();
        }

        /// <summary>
        /// </summary>
        public void Begin()
        {
            if (HadBegun) return;
            _selfClose = true;
            Connection.Open();

            if (!EnableTransaction) return;
            DbTransaction = IsolationLevel != null
                ? Connection.BeginTransaction(IsolationLevel.Value)
                : Connection.BeginTransaction();
        }

        /// <summary>
        /// </summary>
        public void Rollback()
        {
            DbTransaction?.Rollback();
        }

        /// <summary>
        /// </summary>
        public void Commit()
        {
            DbTransaction?.Commit();
        }

        /// <summary>
        /// </summary>
        public void Close()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}