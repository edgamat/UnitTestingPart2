using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossroads.Domain;
using Crossroads.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Crossroads.Web.Controllers
{
    [Route("api/[controller]")]
    public class TransactionTypeController : Controller
    {
        private ITransactionTypeRepository transactionTypes;

        public TransactionTypeController(ITransactionTypeRepository transactionTypes)
        {
            this.transactionTypes = transactionTypes;
        }

        [HttpGet]
        public IEnumerable<TransactionType> Index(bool? excludeBackouts = true)
        {
            return transactionTypes.GetAll(excludeBackouts);
        }

        [HttpGet("{id}", Name = "GetTransactionType")]
        public IActionResult GetById(string id)
        {
            var item = transactionTypes.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }
    }
}
