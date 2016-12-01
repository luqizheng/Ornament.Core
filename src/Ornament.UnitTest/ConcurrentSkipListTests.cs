using Xunit;

namespace Ornament.UnitTest
{
    [Fact]
    public sealed class ConcurrentSkipListTests : SkipListTests
    {
        protected internal override SkipList<int> GetCollection(int? capacity = null)
        {
            return new ConcurrentSkipList<int>();
        }
    }
}