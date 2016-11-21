using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Crossroads.Persistence.Tests.TransactionTypeTests
{
    public class GetAllTests
    {
        private ITestOutputHelper output;

        private CrossroadsContextFactory factory;

        public GetAllTests(ITestOutputHelper output)
        {
            this.output = output;
            this.factory = new CrossroadsContextFactory();
        }

        [Fact]
        public void BasicUsage()
        {
            var context = this.factory.Create();

            context.Database.Log = this.output.WriteLine;

            var repository = new TransactionTypeRepository(context);

            var allTypes = repository.GetAll();

            context.Dispose();

            Assert.Equal(2, allTypes.Count());
        }

        [Fact]
        public void WhenIncludingBackouts_AllActiveRecordsInResults()
        {
            var context = this.factory.Create();

            context.Database.Log = this.output.WriteLine;

            var repository = new TransactionTypeRepository(context);

            var allTypes = repository.GetAll(excludeBackouts: false);

            context.Dispose();

            Assert.Equal(4, allTypes.Count());
        }
    }
}
