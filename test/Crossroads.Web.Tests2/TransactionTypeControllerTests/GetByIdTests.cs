using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossroads.Domain;
using Crossroads.Persistence;
using Crossroads.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Crossroads.Web.Tests2.TransactionTypeControllerTests
{
    public class GetByIdTests
    {
        private ITestOutputHelper output;

        public GetByIdTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void WhenDataNotFound_ReturnNotFoundResult()
        {
            var fakeRepository = new Mock<ITransactionTypeRepository>();

            var controller = new TransactionTypeController(fakeRepository.Object);

            var result = controller.GetById(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void WhenDataFound_ReturnObjectResult()
        {
            var fakeData = new TransactionType { Abbreviation = "N" };

            var fakeRepository = new Mock<ITransactionTypeRepository>();

            fakeRepository.Setup(x => x.Find(It.IsAny<string>())).Returns(fakeData);

            var controller = new TransactionTypeController(fakeRepository.Object);

            var result = controller.GetById(null);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var data = Assert.IsType<TransactionType>(objectResult.Value);

            this.output.WriteLine("{0}", data.Abbreviation);

            Assert.Equal("N", data.Abbreviation);
        }
    }
}
