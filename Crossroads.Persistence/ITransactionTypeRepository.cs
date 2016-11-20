using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crossroads.Domain;

namespace Crossroads.Persistence
{
    public interface ITransactionTypeRepository
    {
        IEnumerable<TransactionType> GetAll(bool? excludeBackouts = true);

        TransactionType Find(string key);
    }
}
