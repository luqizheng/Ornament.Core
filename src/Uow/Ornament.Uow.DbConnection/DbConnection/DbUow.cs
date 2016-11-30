using System;
using System.Data;

namespace Ornament.Uow.DbConnection
{
    /// <summary>
    /// 
    /// </summary>
    public class DbUow : IUnitOfWork
    {
        private readonly bool _isTranscation;
        private bool _selfClose;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="isTranscation"></param>
        public DbUow(IDbConnection connection, bool isTranscation)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            Connection = connection;
            _isTranscation = isTranscation;
        }
        /// <summary>
        /// 
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDbConnection Connection { get; }
        /// <summary>
        /// 
        /// </summary>
        public IDbTransaction DbTransaction { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HadBegun => Connection.State == ConnectionState.Open;
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if ((Connection.State != ConnectionState.Closed) && _selfClose)
                Connection.Close();
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            DbTransaction?.Rollback();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            DbTransaction?.Commit();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}