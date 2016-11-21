using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossroads.Domain;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Crossroads.Persistence.Tests.TransactionTypeTests
{
    public class GetAllTestsWithFakeObjects
    {
        private ITestOutputHelper output;

        private CrossroadsContextFactory factory;

        private CrossroadsContext context;

        public GetAllTestsWithFakeObjects(ITestOutputHelper output)
        {
            this.output = output;
            this.factory = new CrossroadsContextFactory();

            var fakeContext = new Mock<CrossroadsContext>("name=DefaultConnection");

            var data = new List<TransactionType>();
            data.Add(new TransactionType { Abbreviation = "N", BackOutType = false, Active = true, Name = "New Business" });
            data.Add(new TransactionType { Abbreviation = "BN", BackOutType = true, Active = true, Name = "Backout of New Business" });

            var mockSet = data.CreateMockSet();

            fakeContext.Setup(x => x.TransactionTypes).Returns(mockSet.Object);

            this.context = fakeContext.Object;
        }

        [Fact]
        public void BasicUsage()
        {
            var context = this.context;

            var repository = new TransactionTypeRepository(context);

            var allTypes = repository.GetAll();

            Assert.Equal(1, allTypes.Count());
        }

        [Fact]
        public void WhenIncludingBackouts_AllActiveRecordsInResults()
        {
            var context = this.context;

            var repository = new TransactionTypeRepository(context);

            var allTypes = repository.GetAll(excludeBackouts: false);

            Assert.Equal(2, allTypes.Count());
        }
    }
}
