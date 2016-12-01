using System;
using System.Collections.Generic;
using FunctionalTests;
using Ornament.Collecions;
using Xunit;

namespace Ornament.UnitTest
{
   
    public class PriorityQueueTests : CollectionTests<PriorityQueue<int>>
    {
        protected internal override PriorityQueue<int> GetCollection(int? capacity = null)
        {
            if (capacity.HasValue)
            {
                return new PriorityQueue<int>(capacity.Value);
            }
            return new PriorityQueue<int>();
        }

        protected internal override void CheckStructure(PriorityQueue<int> queue)
        {
            for (int i = 0; i < queue.Count / 2; i++)
            {
                int left = (i + 1) * 2 - 1;
                int right = (i + 1) * 2;
                if (queue._heap[i] < queue._heap[left])
                {
                    var c = String.Format("Heap structure vaiolation. Item {0}:{1} must be greater or equal than {2}:{3}",
                        queue._heap[i], i, queue._heap[left], left);
                    throw new ArgumentException(c);
                }
                if (right < queue._heap.Length && queue._heap[i] < queue._heap[right])
                {
                    var c = String.Format("Heap structure vaiolation. Item {0}:{1} must be greater or equal than {2}:{3}",
                        queue._heap[i], i, queue._heap[right], right);
                    throw new ArgumentException(c);
                }
            }
        }

        [Fact]
        public override void CopyTo()
        {
            PriorityQueue<int> target = GetCollection();
            Assert.Equal(0, target.Count);

            const int count = 10;
            for (int i = 0; i < count; i++)
            {
                target.Enqueue(i);
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => target.CopyTo(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => target.CopyTo(new int[count], -1));
            Assert.Throws<ArgumentException>(() => target.CopyTo(new int[1], 0));

            var result = new int[count];
            target.CopyTo(result, 0);

            // Priority queue is max-based so greater items comes first
            for (int i = 0; i < count; i++)
            {
                Assert.Equal(count - i - 1, result[i]);
            }

            result = new int[count + 1];
            result[0] = -1;
            target.CopyTo(result, 1);
            Assert.Equal(-1, result[0]);
            for (int i = 0; i < count; i++)
            {
                Assert.Equal(count - i - 1, result[i + 1]);
            }
        }

        [Fact]
        public override void GetEnumerator()
        {
            PriorityQueue<int> target = GetCollection();
            Assert.Equal(0, target.Count);

            const int count = 10;
            for (int i = 0; i < count; i++)
            {
                target.Enqueue(i);
            }

            IEnumerator<int> enumerator = target.GetEnumerator();
            int x = 9;
            while (enumerator.MoveNext())
            {
                Assert.Equal(x, enumerator.Current);
                x--;
            }
            Assert.Equal(-1, x);
        }

        [Fact]
        public void TakeSimple()
        {
            PriorityQueue<int> target = GetCollection();
            Assert.Equal(0, target.Count);

            Assert.Throws<InvalidOperationException>(() => target.Peek());

            target.Enqueue(1);
            Assert.Equal(1, target.Peek());
            Assert.Equal(0, target.Count);

            target.Enqueue(2);
            Assert.Equal(2, target.Peek());
            Assert.Equal(0, target.Count);

            target.Enqueue(0);
            target.Enqueue(1);
            target.Enqueue(2);
            Assert.Equal(2, target.Peek());
            Assert.Equal(2, target.Count);
            Assert.Equal(1, target.Peek());
            Assert.Equal(1, target.Count);
            target.Enqueue(3);
            Assert.Equal(3, target.Peek());
            Assert.Equal(1, target.Count);
            Assert.Equal(0, target.Peek());
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
                    target.Enqueue(item);
                    store.Add(item);
                }
                else
                {
                    item = target.Peek();
                    Assert.Equal(store.Max, item);
                    store.Remove(store.Max);
                }
                CheckStructure(target);
            }
        }

        [Fact]
        public void PeekSimple()
        {
            PriorityQueue<int> target = GetCollection();
            Assert.Equal(0, target.Count);

            Assert.Throws<InvalidOperationException>(() => target.Peek());

            target.Enqueue(1);
            Assert.Equal(1, target.Peek());
            Assert.Equal(1, target.Count);

            target.Enqueue(2);
            Assert.Equal(2, target.Peek());
            Assert.Equal(2, target.Count);

            target.Enqueue(0);
            Assert.Equal(2, target.Peek());
            Assert.Equal(3, target.Count);

            target.Peek();
            Assert.Equal(1, target.Peek());

            target.Enqueue(3);
            Assert.Equal(3, target.Peek());
            Assert.Equal(3, target.Count);
        }

        [Fact]
        public void PeekRandomized()
        {
            PriorityQueue<int> target = GetCollection();
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

                target.Enqueue(item);
                store.Add(item);

                Assert.Equal(store.Max, target.Peek());
                CheckStructure(target);
            }
        }

        [Fact]
        public void CapacityGrow()
        {
            var target = GetCollection(3);

            Assert.Equal(0, target.Count);

            target.Enqueue(1);
            target.Enqueue(2);
            target.Enqueue(3);

            target.Enqueue(4);
            Assert.Equal(4, target.Count);
            Assert.Equal(6, target.Capacity);
            string result = string.Join(",", target);
            Assert.Equal("4,3,2,1", result);

            target.Enqueue(0);
            Assert.Equal(5, target.Count);
            Assert.Equal(6, target.Capacity);
            result = string.Join(",", target);
            Assert.Equal("4,3,2,1,0", result);
        }

        [Fact]
        public void CapacityShrink()
        {
            var target = GetCollection(50);

            for (var i = 0; i < 13; i++)
            {
                target.Enqueue(i);
            }
            Assert.Equal(50, target.Capacity);
            Assert.Equal(13, target.Count);
            target.Peek();
            Assert.Equal(25, target.Capacity);
            Assert.Equal(12, target.Count);
            target.Peek();
            Assert.Equal(25, target.Capacity);
            Assert.Equal(11, target.Count);
        }

        //[Fact]
        //public void UseCustomComparer()
        //{
        //    var target =
        //        new PriorityQueue<KeyValuePair<int, string>>(
        //            new KeyValuePairComparer<int, string>())
        //    {
        //        new KeyValuePair<int, string>(5, "1"),
        //        new KeyValuePair<int, string>(7, "2"),
        //        new KeyValuePair<int, string>(3, "3")
        //    };

        //    Assert.Equal("2", target.Peek().Value);
        //    Assert.Equal("1", target.Peek().Value);
        //    Assert.Equal("3", target.Peek().Value);
        //}

        [Fact]

        public void InstantiateWithNonComparableTypeAndNoComparer()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new PriorityQueue<KeyValuePair<int, string>>();
            });



        }
    }
}