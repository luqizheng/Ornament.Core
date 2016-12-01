using System;
using System.Collections.Generic;
using Xunit;

namespace Ornament.UnitTest
{
  
    public class SkipListTests : CollectionTests<SkipList<int>>
    {
        protected internal override SkipList<int> GetCollection(int? capacity = null)
        {
            return new SkipList<int>();
        }

        protected internal override void CheckStructure(SkipList<int> target)
        {
            var node = target._head.GetNext(0);
            while (node != target._tail)
            {
                for (var level = 0; level < node.Height; level++)
                {
                    var next = node.GetNext(level);
                    Assert.True(next == target._tail || node.Item <= next.Item);
                }

                node = node.GetNext(0);
            }
        }

        [Fact]
        public void PeekSimple()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);
            AssertEx.Throws<InvalidOperationException>(() => target.Peek());

            target.Add(1);
            Assert.Equal(1, target.Peek());
            Assert.Equal(1, target.Count);

            target.Add(2);
            Assert.Equal(1, target.Peek());
            Assert.Equal(2, target.Count);


            target.Add(0);
            Assert.Equal(0, target.Peek());
            Assert.Equal(3, target.Count);
        }

        [Fact]
    
        public void PeekRandomized()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            var store = new SortedSet<int>();
            var random = new Random();
            const int count = 1000;
            for (int i = 0; i < count; i++)
            {
                int item = random.Next(2 * count);
                while (store.Contains(item))
                {
                    item = random.Next(2 * count);
                }

                target.Add(item);
                store.Add(item);

                Assert.Equal(store.Min, target.Peek());
                CheckStructure(target);
            }
        }

        [Fact]
        
        public void GetFirst()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);
            AssertEx.Throws<InvalidOperationException>(() => target.GetFirst());

            target.Add(1);
            Assert.Equal(1, target.GetFirst());
            Assert.Equal(1, target.Count);

            target.Add(2);
            Assert.Equal(1, target.GetFirst());
            Assert.Equal(2, target.Count);


            target.Add(0);
            Assert.Equal(0, target.GetFirst());
            Assert.Equal(3, target.Count);
        }

        [Fact]
        public void GetLast()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            AssertEx.Throws<InvalidOperationException>(() => target.GetLast());

            target.Add(1);
            Assert.Equal(1, target.GetLast());
            Assert.Equal(1, target.Count);

            target.Add(2);
            Assert.Equal(2, target.GetLast());
            Assert.Equal(2, target.Count);

            target.Add(0);
            Assert.Equal(2, target.GetLast());
            Assert.Equal(3, target.Count);

            target.Add(3);
            Assert.Equal(3, target.GetLast());
            Assert.Equal(4, target.Count);
        }

        [Fact]
        public void TakeSimple()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            AssertEx.Throws<InvalidOperationException>(() => target.Take());

            target.Add(1);
            Assert.Equal(1, target.Take());
            Assert.Equal(0, target.Count);

            target.Add(2);
            Assert.Equal(2, target.Take());
            Assert.Equal(0, target.Count);

            target.Add(0);
            target.Add(1);
            target.Add(2);
            Assert.Equal(0, target.Take());
            Assert.Equal(2, target.Count);
            Assert.Equal(1, target.Take());
            Assert.Equal(1, target.Count);
            target.Add(3);
            Assert.Equal(2, target.Take());
            Assert.Equal(1, target.Count);
            Assert.Equal(3, target.Take());
            Assert.Equal(0, target.Count);
        }

        [Fact]
        public void TakeRandomized()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            var store = new SortedSet<int>();
            var random = new Random();
            const int count = 1000;
            for (int i = 0; i < count; i++)
            {
                int item = random.Next(2 * count);
                while (store.Contains(item))
                {
                    item = random.Next(2 * count);
                }

                if (target.Count < 10)
                {
                    target.Add(item);
                    store.Add(item);
                }
                else
                {
                    item = target.Take();
                    Assert.Equal(store.Min, item);
                    store.Remove(store.Min);
                }
                CheckStructure(target);
            }
        }

        [Fact]
        public void TakeLastSimple()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            AssertEx.Throws<InvalidOperationException>(() => target.TakeLast());

            target.Add(1);
            Assert.Equal(1, target.TakeLast());
            Assert.Equal(0, target.Count);

            target.Add(2);
            Assert.Equal(2, target.TakeLast());
            Assert.Equal(0, target.Count);

            target.Add(0);
            target.Add(1);
            target.Add(2);
            Assert.Equal(2, target.TakeLast());
            Assert.Equal(2, target.Count);
            Assert.Equal(1, target.TakeLast());
            Assert.Equal(1, target.Count);
            target.Add(3);
            Assert.Equal(3, target.TakeLast());
            Assert.Equal(1, target.Count);
            Assert.Equal(0, target.TakeLast());
            Assert.Equal(0, target.Count);
        }

        [Fact]
        public void TakeLastRandomized()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            var store = new SortedSet<int>();
            var random = new Random();
            const int count = 1000;
            for (int i = 0; i < count; i++)
            {
                int item = random.Next(2 * count);
                while (store.Contains(item))
                {
                    item = random.Next(2 * count);
                }

                if (target.Count < 10)
                {
                    target.Add(item);
                    store.Add(item);
                }
                else
                {
                    item = target.TakeLast();
                    Assert.Equal(store.Max, item);
                    store.Remove(store.Max);
                }
                CheckStructure(target);
            }
        }

        [Fact]
        public void Floor()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            Assert.Equal(0, target.Floor(1));
            Assert.Equal(0, target.Floor(2));

            target.Add(1);
            Assert.Equal(1, target.Floor(1));
            Assert.Equal(1, target.Floor(2));

            target.Add(3);
            Assert.Equal(1, target.Floor(1));
            Assert.Equal(1, target.Floor(2));
            Assert.Equal(3, target.Floor(3));
            Assert.Equal(3, target.Floor(5));

            target.Add(5);
            Assert.Equal(1, target.Floor(1));
            Assert.Equal(1, target.Floor(2));
            Assert.Equal(3, target.Floor(3));
            Assert.Equal(5, target.Floor(5));
            Assert.Equal(5, target.Floor(7));

            target.Add(2);
            Assert.Equal(1, target.Floor(1));
            Assert.Equal(2, target.Floor(2));
            Assert.Equal(3, target.Floor(3));
            Assert.Equal(5, target.Floor(5));
            Assert.Equal(5, target.Floor(7));

            target.Remove(1);
            Assert.Equal(0, target.Floor(1));
            Assert.Equal(2, target.Floor(2));
            Assert.Equal(3, target.Floor(3));
            Assert.Equal(5, target.Floor(5));
            Assert.Equal(5, target.Floor(7));

            target.Remove(3);
            Assert.Equal(0, target.Floor(1));
            Assert.Equal(2, target.Floor(2));
            Assert.Equal(2, target.Floor(3));
            Assert.Equal(5, target.Floor(5));
            Assert.Equal(5, target.Floor(7));
        }

        [Fact]
        public void Ceiling()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            Assert.Equal(0, target.Ceiling(1));
            Assert.Equal(0, target.Ceiling(2));

            target.Add(1);
            Assert.Equal(1, target.Ceiling(0));
            Assert.Equal(1, target.Ceiling(1));
            Assert.Equal(0, target.Ceiling(2));

            target.Add(3);
            Assert.Equal(1, target.Ceiling(0));
            Assert.Equal(1, target.Ceiling(1));
            Assert.Equal(3, target.Ceiling(2));
            Assert.Equal(3, target.Ceiling(3));
            Assert.Equal(0, target.Ceiling(5));

            target.Add(5);
            Assert.Equal(1, target.Ceiling(1));
            Assert.Equal(3, target.Ceiling(2));
            Assert.Equal(3, target.Ceiling(3));
            Assert.Equal(5, target.Ceiling(4));
            Assert.Equal(5, target.Ceiling(5));
            Assert.Equal(0, target.Ceiling(7));

            target.Add(7);
            Assert.Equal(1, target.Ceiling(1));
            Assert.Equal(3, target.Ceiling(2));
            Assert.Equal(3, target.Ceiling(3));
            Assert.Equal(5, target.Ceiling(5));
            Assert.Equal(7, target.Ceiling(6));
            Assert.Equal(7, target.Ceiling(7));
            Assert.Equal(0, target.Ceiling(8));

            target.Remove(1);
            Assert.Equal(3, target.Ceiling(1));
            Assert.Equal(3, target.Ceiling(2));
            Assert.Equal(3, target.Ceiling(3));
            Assert.Equal(5, target.Ceiling(5));
            Assert.Equal(7, target.Ceiling(7));
            Assert.Equal(0, target.Ceiling(8));

            target.Remove(3);
            Assert.Equal(5, target.Ceiling(1));
            Assert.Equal(5, target.Ceiling(2));
            Assert.Equal(5, target.Ceiling(3));
            Assert.Equal(5, target.Ceiling(5));
            Assert.Equal(7, target.Ceiling(7));
            Assert.Equal(0, target.Ceiling(8));

            target.Remove(5);
            for (var i = 0; i < 8; i++)
            {
                Assert.Equal(7, target.Ceiling(i));
            }
            Assert.Equal(0, target.Ceiling(8));
        }

        [Fact]
        public void RangeSimple()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            for (var i = 0; i < 10; i++)
            {
                target.Add(i);
            }

            Assert.Equal("0,1,2,3,4,5,6,7,8,9", string.Join(",", target.Range(-1, 10)));
            Assert.Equal("0,1,2,3,4,5,6,7,8,9", string.Join(",", target.Range(0, 9)));
            Assert.Equal("1,2,3,4,5,6,7,8,9", string.Join(",", target.Range(0, 9, false)));
            Assert.Equal("1,2,3,4,5,6,7,8", string.Join(",", target.Range(0, 9, false, false)));
            Assert.Equal("2,3,4,5,6,7", string.Join(",", target.Range(2, 7)));
            Assert.Equal("2,3,4,5,6", string.Join(",", target.Range(2, 7, true, false)));
            Assert.Equal("3,4,5,6,7", string.Join(",", target.Range(2, 7, false)));
        }

        [Fact]
        [ExpectedException(typeof(ArgumentException))]
        public void InstantiateWithNonComparableTypeAndNoComparer()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SkipList<KeyValuePair<int, string>>();
        }
    }
}
