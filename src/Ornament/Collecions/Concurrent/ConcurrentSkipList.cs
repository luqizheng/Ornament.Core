﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Ornament.Collecions.Concurrent
{
    public class ConcurrentSkipList<T> : SkipList<T>, IProducerConsumerCollection<T>, IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public void Dispose()
        {
            ((IDisposable) _lock).Dispose();
        }

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

        public bool TryAdd(T item)
        {
            Add(item);
            return true;
        }

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

        public object SyncRoot
        {
            get { throw new NotSupportedException(""); }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

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