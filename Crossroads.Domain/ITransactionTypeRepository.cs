using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Crossroads.Domain
{
    public interface ITransactionTypeRepository
    {
        IEnumerable<TransactionType> GetAll(bool? excludeBackouts = true);

        IEnumerable<TransactionType> Search(Expression<Func<TransactionType, bool>> filter);

        TransactionType Find(string key);

        void Insert(TransactionType item);

        void Delete(string key);

        void Update(TransactionType item);
    }
}
