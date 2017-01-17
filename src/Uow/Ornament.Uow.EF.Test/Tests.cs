using Xunit;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Ornament.Uow.EF.Test.Models;

namespace Ornament.Uow.EF.Test
{
    public class Tests
    {
        private DbContextOptions<MyContext> _options;
        public Tests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseInMemoryDatabase();
            _options = optionsBuilder.Options;
        }
        [Fact]
        public void Test1()
        {
          
            MyContext context=new MyContext(_options);

            MyEfUow uow=new MyEfUow(context);
            uow.Begin();
            uow.Context.Add(new Blog()
            {
                BlogId = 11,
                Url = ":"
            });
            uow.Commit();
            uow.Dispose();



        }
    }
}
