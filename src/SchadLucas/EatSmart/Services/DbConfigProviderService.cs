using NLog;
using System;
using System.Collections.Specialized;
using SchadLucas.EatSmart.Data.Exceptions;

namespace SchadLucas.EatSmart.Services
{
    public abstract class DbConfigProviderService : IDbConfigProviderService
    {
        /// <summary>
        ///     Gets the <see cref="NLog.Logger" /> of the deriving Class
        /// </summary>
        protected Logger Logger => LogManager.GetLogger(GetType().Name);
        /// <inheritdoc />
        public string Database { get; protected set; }

        /// <inheritdoc />
        public bool Encrypt { get; protected set; }

        /// <inheritdoc />
        public string Host { get; protected set; }

        /// <inheritdoc />
        public bool IntegratedSecurity { get; protected set; }

        /// <inheritdoc />
        public bool MultipleActiveResultSets { get; protected set; }

        /// <inheritdoc />
        public string Password { get; protected set; }

        /// <inheritdoc />
        public string UserId { get; protected set; }

        private void SetValuesFromCollection(NameValueCollection collection)
        {
            Database = collection["Database"];
            Encrypt = bool.Parse(collection["Encrypt"]);
            Host = collection["Host"];
            IntegratedSecurity = bool.Parse(collection["IntegratedSecurity"]);
            MultipleActiveResultSets = bool.Parse(collection["MultipleActiveResultSets"]);
            Password = collection["Password"];
            UserId = collection["UserId"];
        }

        /// <exception cref="DbConfigInitializationException">
        ///     Configuration could not be initialized.
        /// </exception>
        protected void Setup(NameValueCollection collection)
        {
            try
            {
                SetValuesFromCollection(collection);
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e, "A key is missing in the configuration.");
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, "A boolean value couldn't be parsed from the configuration.");
            }
            catch (Exception e) when (e is NotSupportedException || e is ArgumentNullException)
            {
                throw new DbConfigInitializationException("Configuration could not be initialized.", e);
            }
        }
    }
}