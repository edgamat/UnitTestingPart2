using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using Crossroads.Domain;
using Infinity.Mobius.Configuration;

namespace Crossroads.Persistence
{
    public class CrossroadsContext : DbContext
    {
        public CrossroadsContext()
            : this(MobiusConfiguration.Current[ApplicationSettingsConfig.ConnectionStringsCrossroadsContext])
        {
        }

        public CrossroadsContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.InitializeLogging();
        }

        public CrossroadsContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            this.InitializeLogging();
        }

        public virtual IDbSet<AgentTransaction> AgentTransactions { get; set; }

        public virtual IDbSet<TransactionType> TransactionTypes { get; set; }

        public virtual IDbSet<TransactionStatus> TransactionStatuses { get; set; }

        protected virtual void InitializeLogging()
        {
            this.Database.Log = delegate (string message)
            {
                Console.Write(message);
                System.Diagnostics.Debug.Write(message);
            };
        }
    }
}
