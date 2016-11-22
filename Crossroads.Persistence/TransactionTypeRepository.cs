using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Crossroads.Domain;

namespace Crossroads.Persistence
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly ICrossroadsContext context;

        public TransactionTypeRepository(ICrossroadsContext context)
        {
            this.context = context;
        }

        public IEnumerable<TransactionType> GetAll(bool? excludeBackouts = true)
        {
            var query = this.context.TransactionTypes
                .AsNoTracking()
                .Where(x => x.Active == true);

            if (excludeBackouts == true)
            {
                query = query.Where(x => x.BackOutType == false);
            }

            return query.ToList();
        }

        public IEnumerable<TransactionType> Search(Expression<Func<TransactionType, bool>> filter)
        {
            var query = this.context.TransactionTypes.Where(filter);

            return query.ToList();
        }

        public TransactionType Find(string key)
        {
            var query = this.context.TransactionTypes
                .Where(x => x.Abbreviation == key);

            var item = query.FirstOrDefault();

            return item;
        }

        public void Insert(TransactionType item)
        {
            this.context.TransactionTypes.Add(item);
            this.context.SaveChanges();
        }

        public void Delete(string key)
        {
            var item = this.Find(key);
            if (item != null)
            {
                this.context.TransactionTypes.Remove(item);
                this.context.SaveChanges();
            }
        }

        public void Update(TransactionType item)
        {
            if (this.context.Entry(item).State == EntityState.Detached)
            {
                this.context.TransactionTypes.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}
