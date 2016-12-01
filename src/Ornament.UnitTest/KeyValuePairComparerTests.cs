using System.Collections.Generic;
using Xunit;

namespace Ornament.UnitTest
{
    
    public class KeyValuePairComparerTests
    {
          [Fact]
        public void CompEqual()
        {
            var comparer = new KeyValuePairComparer<int, string>();
            var a = new KeyValuePair<int, string>(1, null);
            var b = new KeyValuePair<int, string>(1, "asdf");
            Assert.Equal(0, comparer.Compare(a, b));
        }

        [Fact]
        public void CompareGreater()
        {
            var comparer = new KeyValuePairComparer<int, string>();
            var a = new KeyValuePair<int, string>(2, null);
            var b = new KeyValuePair<int, string>(1, "asdf");
            Assert.Equal(1, comparer.Compare(a, b));
        }

        [Fact]
        public void CompareNullSecondKey()
        {
            // All instances are greater than null (MSDN)
            var comparer = new KeyValuePairComparer<string, string>();
            var a = new KeyValuePair<string, string>("a", null);
            var b = new KeyValuePair<string, string>(null, "asdf");
            Assert.Equal(1, comparer.Compare(a, b));
        }

        [Fact]
        public void CompareNullFirstKey()
        {
            // All instances are greater than null (MSDN)
            var comparer = new KeyValuePairComparer<string, string>();
            var a = new KeyValuePair<string, string>(null, null);
            var b = new KeyValuePair<string, string>("a", "asdf");
            Assert.Equal(-1, comparer.Compare(a, b));
        }

        [Fact]
        public void CompareNullBothKeys()
        {
            var comparer = new KeyValuePairComparer<string, string>();
            var a = new KeyValuePair<string, string>(null, null);
            var b = new KeyValuePair<string, string>(null, "asdf");
            Assert.Equal(0, comparer.Compare(a, b));
        }

        [Fact]
        public void CompareUsingCustomKeyComparer()
        {
            // Test that the custom comparer for the keys is used if supplied
            var keyComparer = new ReverseIntComparer();
            var comparer = new KeyValuePairComparer<int, string>(keyComparer);
            var a = new KeyValuePair<int, string>(2, null);
            var b = new KeyValuePair<int, string>(1, "asdf");
            Assert.Equal(-1, comparer.Compare(a, b));
        }

        private class ReverseIntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return 0 - Comparer<int>.Default.Compare(x, y);
            }
        }
    }
}
