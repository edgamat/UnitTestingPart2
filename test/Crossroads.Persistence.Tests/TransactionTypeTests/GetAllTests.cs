using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Crossroads.Domain;
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

        [Fact]
        public void WhenAdding_NewIdIsGenerated()
        {
            using (var ts = new TransactionScope())
            {
                var context = this.factory.Create();

                context.Database.Log = this.output.WriteLine;

                var repository = new TransactionTypeRepository(context);

                var item = new TransactionType { Abbreviation = "X", Active = true, BackOutType = false, Name = "Extension" };

                repository.Insert(item);

                context.Dispose();

                this.output.WriteLine("{0}", item.Id);

                Assert.True(item.Id > 0);
            }
        }
    }
}
