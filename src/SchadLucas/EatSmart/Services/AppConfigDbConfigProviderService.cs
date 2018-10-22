using System.Collections.Specialized;
using System.Configuration;
using SchadLucas.EatSmart.Data.Exceptions;

namespace SchadLucas.EatSmart.Services
{
    public sealed class AppConfigDbConfigProviderService : DbConfigProviderService
    {
        /// <summary>
        ///     Initializes a new Instance of the <see cref="AppConfigDbConfigProviderService" /> class.
        /// </summary>
        /// <remarks>
        ///     Provides Configuration Options for a Database Connection based on <i>App.config</i>
        /// </remarks>
        /// <exception cref="DbConfigInitializationException">
        ///     Throws if the Configuration could not be initialized.
        /// </exception>
        public AppConfigDbConfigProviderService()
        {
            var cfg = GetConfigurationSection();
            if (cfg == null)
            {
                throw new DbConfigInitializationException("Could not find app.config section <i>DatabaseConnection</i>");
            }
            else
            {
                Setup(cfg);
            }
        }

        private NameValueCollection GetConfigurationSection()
        {
            try
            {
                return ConfigurationManager.GetSection("DatabaseConnection") as NameValueCollection;
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}