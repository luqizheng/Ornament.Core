using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ornament.Stores;
using Ornament.Uow;
using Xunit;

namespace Ornament.UnitTest
{
    public class TestDbConnection
    {
        [Fact]
        public void TestInherit()
        {

        }
    }

    public class AEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DbConnectionMock : DbConnectionStore<AEntity, int>
    {
        public DbConnectionMock(DbUow context) : base(context)
        {
        }

        public void Add(AEntity entity)
        {

        }

        public override AEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
