using Ornament.Collecions;
using Ornament.Collecions.Concurrent;
using Ornament.UnitTest;
using Xunit;
namespace FunctionalTests
{
   
    public class ConcurrentPriorityQueueTests : PriorityQueueTests
    {
        protected internal override PriorityQueue<int> GetCollection(int? capacity = null)
        {
            if (capacity.HasValue)
            {
                return new ConcurrentPriorityQueue<int>(capacity.Value);
            }
            return new ConcurrentPriorityQueue<int>();
        }
    }
}
