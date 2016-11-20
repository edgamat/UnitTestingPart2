using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossroads.Domain;

namespace Crossroads.Persistence
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly CrossroadsContext context;

        public TransactionTypeRepository(CrossroadsContext context)
        {
            this.context = context;
        }

        public IEnumerable<TransactionType> GetAll(bool? excludeBackouts = true)
        {
            var query = this.context.TransactionTypes.Where(x => x.Active == true);

            if (excludeBackouts == true)
            {
                query = query.Where(x => x.BackOutType == false);
            }

            return query.ToList();
        }

        public TransactionType Find(string key)
        {
            var query = this.context.TransactionTypes
                .Where(x => x.Abbreviation == key);

            var item = query.FirstOrDefault();

            return item;
        }
    }
}
