using System;
using System.Collections.Generic;
using Xunit;

namespace Ornament.UnitTest
{
    public abstract class CollectionTests<T> where T:ICollection<int>
    {
        protected internal abstract T GetCollection(int? capacity = null);
        protected internal abstract void CheckStructure(T target);

        [Fact]
        public void AddSimple()
        {
            var target = GetCollection();
            
            Assert.Equal(0, target.Count);


            target.Add(1);
            Assert.Equal(1, target.Count);
            Assert.True(target.Contains(1));

            target.Add(2);
            Assert.Equal(2, target.Count);
            Assert.True(target.Contains(1));
            Assert.True(target.Contains(2));

            target.Add(3);
            Assert.Equal(3, target.Count);
            Assert.True(target.Contains(1));
            Assert.True(target.Contains(2));
            Assert.True(target.Contains(3));

            target.Add(0);
        }


       [Fact]
        public virtual void AddRandomized()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            const int count = 1000;
            var random = new Random();
            var store = new bool[count];
            var targetCount = 0;

            // randomly add items from 0 to count-1 to collection and at each step:
            // - update store[i] to true to indicate that item should present in the collection
            // - update expected count
            // - check that collection count is correct
            // - check that items in the collection correspond to items in the store
            for (var i = 0; i < count; i++)
            {
                var item = random.Next(count);
                while (store[item])
                {
                    item = random.Next(count);
                }

                targetCount++;
                store[item] = true;

                target.Add(item);
                Assert.Equal(targetCount, target.Count);

                for (var j = 0; j < count; j++)
                {
                    Assert.Equal(store[j], target.Contains(j));
                }
                CheckStructure(target);
            }
        }

       [Fact]
        public virtual void Clear()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            target.Add(1);
            Assert.Equal(1, target.Count);
            Assert.True(target.Contains(1));

            target.Clear();
            Assert.Equal(0, target.Count);
            Assert.False(target.Contains(1));
        }

       [Fact]
        public virtual void CopyTo()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            const int count = 10;
            for (var i = 0; i < count; i++)
            {
                target.Add(i);
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => target.CopyTo(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => target.CopyTo(new int[count], -1));
            Assert.Throws<ArgumentException>(() => target.CopyTo(new int[1], 0));

            var result = new int[count];
            target.CopyTo(result, 0);

            for (var i = 0; i < count; i++)
            {
                Assert.Equal(i, result[i]);
            }

            result = new int[count + 1];
            result[0] = -1;
            target.CopyTo(result, 1);
            Assert.Equal(-1, result[0]);
            for (var i = 0; i < count; i++)
            {
                Assert.Equal(i, result[i+1]);
            }
        }

       [Fact]
        public virtual void RemoveSimple()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            for (var i = 0; i < 5; i++)
            {
                target.Add(i);
            }
            Assert.Equal(5, target.Count);

            Assert.True(target.Remove(0));
            Assert.Equal(4, target.Count);
            Assert.False(target.Contains(0));

            Assert.False(target.Remove(0));

            Assert.True(target.Remove(2));
            Assert.Equal(3, target.Count);
            Assert.False(target.Contains(2));
            Assert.False(target.Remove(2));

            Assert.True(target.Remove(1));
            Assert.True(target.Remove(3));
            Assert.Equal(1, target.Count);
            Assert.False(target.Contains(1));
            Assert.False(target.Contains(3));
            Assert.True(target.Contains(4));

            Assert.True(target.Remove(4));
            Assert.Equal(0, target.Count);
            Assert.False(target.Contains(4));
        }

       [Fact]
        public virtual void RemoveRandomized()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            const int count = 1000;
            var random = new Random();
            var store = new bool[count];

            // add items from 0 to count-1 into collection
            // set store[i] to true to indicate that item should be in the collection
            for (var i = 0; i < count; i++)
            {
                target.Add(i);
                store[i] = true;
            }
            var targetCount = count;

            // randomly remove items from collection and at each step:
            // - update the store and expected count
            // - check that count is correct
            // - check that items in the store correspond to items in the collection
            for (var i = 0; i < count; i++)
            {
                var item = random.Next(count);
                while (!store[item])
                {
                    // if item is not in the store it should not be in the collection
                    Assert.False(target.Remove(item));
                    item = random.Next(count);
                }

                targetCount--;
                store[item] = false;

                target.Remove(item);
                Assert.Equal(targetCount, target.Count);

                for (var j = 0; j < count; j++)
                {
                    Assert.Equal(store[j], target.Contains(j));
                }
                CheckStructure(target);
            }
        }

       [Fact]
        public virtual void GetEnumerator()
        {
            var target = GetCollection();
            Assert.Equal(0, target.Count);

            const int count = 10;
            for (var i = 0; i < count; i++)
            {
                target.Add(i);
            }

            var store = new List<int>(count);
            store.AddRange(target);
            var result = store.ToArray();
            Assert.Equal(count, result.Length);
            Array.Sort(result);
            for (var i = 0; i < count; i++)
            {
                Assert.Equal(i, result[i]);
            }
        }
    }
}
