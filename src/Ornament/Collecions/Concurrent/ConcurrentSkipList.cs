using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Ornament.Collecions.Concurrent
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentSkipList<T> : SkipList<T>, IProducerConsumerCollection<T>, IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            ((IDisposable) _lock).Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> enumerator;
            _lock.EnterReadLock();
            try
            {
                enumerator = base.GetEnumerator();
            }
            finally
            {
                _lock.ExitReadLock();
            }
            return enumerator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryAdd(T item)
        {
            Add(item);
            return true;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryTake(out T item)
        {
            item = default(T);
            Node node;
            _lock.EnterUpgradeableReadLock();
            try
            {
                if (Count == 0) return false;

                node = _head.GetNext(0);
                _lock.EnterWriteLock();
                try
                {
                    DeleteNode(node);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
            item = node.Item;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] array;
            _lock.EnterReadLock();
            try
            {
                array = new T[Count];
                CopyTo(array, 0);
            }
            finally
            {
                _lock.ExitReadLock();
            }
            return array;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public override void CopyTo(Array array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                base.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public object SyncRoot
        {
            get { throw new NotSupportedException(""); }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                base.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Contains(T item)
        {
            Node node;
            _lock.EnterReadLock();
            try
            {
                node = FindNode(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }

            _lock.EnterWriteLock();
            try
            {
                _lastFoundNode = node;
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            return CompareNode(node, item) == 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public override void Add(T item)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                var prev = FindNode(item);

                _lock.EnterWriteLock();
                try
                {
                    _lastFoundNode = AddNewNode(item, prev);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Remove(T item)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                var node = FindNode(item);
                if (CompareNode(node, item) != 0) return false;

                _lock.EnterWriteLock();
                try
                {
                    DeleteNode(node);
                    if (_lastFoundNode == node)
                        _lastFoundNode = _head;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T GetLast()
        {
            _lock.EnterReadLock();
            try
            {
                return base.GetLast();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Peek()
        {
            _lock.EnterReadLock();
            try
            {
                return base.Peek();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Take()
        {
            _lock.EnterWriteLock();
            try
            {
                return base.Take();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T TakeLast()
        {
            _lock.EnterWriteLock();
            try
            {
                return base.TakeLast();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override T Floor(T item)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                return base.Floor(item);
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override T Ceiling(T item)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                return base.Ceiling(item);
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromItem"></param>
        /// <param name="toItem"></param>
        /// <param name="includeFromItem"></param>
        /// <param name="includeToItem"></param>
        /// <returns></returns>
        public override IEnumerable<T> Range(T fromItem, T toItem, bool includeFromItem = true,
            bool includeToItem = true)
        {
            _lock.EnterReadLock();
            try
            {
                return base.Range(fromItem, toItem, includeFromItem, includeToItem);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        protected internal override void SetLastFoundNode(Node node)
        {
            var hasOuterWriteLock = _lock.IsWriteLockHeld;
            if (!hasOuterWriteLock)
                _lock.EnterWriteLock();
            try
            {
                base.SetLastFoundNode(node);
            }
            finally
            {
                if (!hasOuterWriteLock)
                    _lock.ExitWriteLock();
            }
        }
    }
}