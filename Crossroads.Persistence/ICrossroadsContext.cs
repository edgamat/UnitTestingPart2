using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Crossroads.Domain;

namespace Crossroads.Persistence
{
    public interface ICrossroadsContext
    {
        IDbSet<AgentTransaction> AgentTransactions { get; set; }

        IDbSet<TransactionStatus> TransactionStatuses { get; set; }

        IDbSet<TransactionType> TransactionTypes { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}