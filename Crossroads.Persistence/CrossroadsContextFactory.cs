
namespace Crossroads.Persistence
{
    using System;
    using System.Data.Entity.Infrastructure;
    using Infinity.Mobius.Configuration;

    /// <summary>
    /// A factory for creating CrossroadsContext instances. Used to enable design-time services
    /// that require a parameter-less constructor. This is necessary for code-first migrations
    /// to work correctly.
    /// </summary>
    public class CrossroadsContextFactory : IDbContextFactory<CrossroadsContext>
    {
        /// <summary>
        /// Creates a new instance of a CrossroadsContext type.
        /// </summary>
        /// <returns>An instance of CrossroadsContext.</returns>
        public CrossroadsContext Create()
        {
            var connectionStringName = ApplicationSettingsConfig.ConnectionStringsCrossroadsContext;

            string connectionString = MobiusConfiguration.Current[connectionStringName];

            if (string.IsNullOrEmpty(connectionString))
            {
                ApplicationSettingsConfig.Initialize();
                connectionString = MobiusConfiguration.Current[connectionStringName];
            }

            return new CrossroadsContext(connectionString);
        }
    }
}
