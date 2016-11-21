
namespace Crossroads.Persistence
{
    using System;
    using System.Collections.Specialized;
    using Infinity.Mobius.Configuration;

    public class ApplicationSettingsConfig
    {
        public const string ConnectionStringsCrossroadsContext = "ConnectionStrings:CrossroadsContext";

        public static void Initialize()
        {
            var settings = new NameValueCollection();

            settings[ApplicationSettingsConfig.ConnectionStringsCrossroadsContext] =
                "Server=(LocalDB)\\v11.0;Database=Crossroads1_1;Trusted_Connection=True;";

            MobiusConfiguration.UseSettings(settings);
        }
    }
}
