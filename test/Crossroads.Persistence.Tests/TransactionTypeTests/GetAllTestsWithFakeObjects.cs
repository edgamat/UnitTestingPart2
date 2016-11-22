using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Crossroads.Domain;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Crossroads.Persistence.Tests.TransactionTypeTests
{
    public class GetAllTestsWithFakeObjects
    {
        private ITestOutputHelper output;

        private ICrossroadsContext context;

        private List<TransactionType> data;

        public GetAllTestsWithFakeObjects(ITestOutputHelper output)
        {
            this.output = output;

            var fakeContext = new Mock<ICrossroadsContext>();

            var fakeDbSet = new Mock<DbSet<TransactionType>>();

            this.data = new List<TransactionType>();
            this.data.Add(new TransactionType { Abbreviation = "N", BackOutType = false, Active = true, Name = "New Business", Id = 1 });
            this.data.Add(new TransactionType { Abbreviation = "BN", BackOutType = true, Active = true, Name = "Backout of New Business", Id = 2 });

            var source = this.data.AsQueryable();

            fakeDbSet.As<IQueryable<TransactionType>>().Setup(x => x.Provider).Returns(source.Provider);
            fakeDbSet.As<IQueryable<TransactionType>>().Setup(x => x.Expression).Returns(source.Expression);
            fakeDbSet.As<IQueryable<TransactionType>>().Setup(x => x.ElementType).Returns(source.ElementType);
            fakeDbSet.As<IQueryable<TransactionType>>().Setup(x => x.GetEnumerator()).Returns(source.GetEnumerator());

            fakeDbSet.Setup(x => x.AsNoTracking()).Returns(fakeDbSet.Object);

            fakeContext.Setup(x => x.TransactionTypes).Returns(fakeDbSet.Object);

            fakeDbSet.Setup(x => x.Add(It.IsAny<TransactionType>())).Returns((TransactionType x) =>
            {
                var maxId = this.data.Max(d => d.Id);

                x.Id = maxId + 1;

                return x;
            });

            fakeContext.Setup(x => x.SaveChanges()).Returns(() =>
            {
                return 1;
            });


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

        [Fact]
        public void WhenAdding_NewIdIsGenerated()
        {
            var context = this.context;

            var repository = new TransactionTypeRepository(context);

            var item = new TransactionType { Abbreviation = "X", Active = true, BackOutType = false, Name = "Extension" };

            repository.Insert(item);

            this.output.WriteLine("{0}", item.Id);

            Assert.True(item.Id > 0);
        }
    }
}
