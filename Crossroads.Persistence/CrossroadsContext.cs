using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using Crossroads.Domain;
using Infinity.Mobius.Configuration;

namespace Crossroads.Persistence
{
    public class CrossroadsContext : DbContext, ICrossroadsContext
    {
        public CrossroadsContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual IDbSet<AgentTransaction> AgentTransactions { get; set; }

        public virtual IDbSet<TransactionType> TransactionTypes { get; set; }

        public virtual IDbSet<TransactionStatus> TransactionStatuses { get; set; }
    }
}
