using JetBrains.Annotations;

namespace SchadLucas.EatSmart.Services
{
    [UsedImplicitly]
    public interface IDbConfigProviderService : IService
    {
        /// <summary>
        ///     Gets the name of the database.
        /// </summary>
        string Database { get; }

        /// <summary>
        ///     Gets a value indicating if the connection should be encrypted or not.
        /// </summary>
        bool Encrypt { get; }

        /// <summary>
        ///     Gets the address of the host.
        /// </summary>
        string Host { get; }

        /// <summary>
        ///     Gets a value indicating if <i>Integrated Security</i> should be used or not.
        /// </summary>
        bool IntegratedSecurity { get; }

        /// <summary>
        ///     Gets a value indicating if <i>Multiple Active Result Sets</i> should be used or not.
        /// </summary>
        bool MultipleActiveResultSets { get; }

        /// <summary>
        ///     Gets the password of the connection.
        /// </summary>
        string Password { get; }

        /// <summary>
        ///     Gets the User Identification for the connection.
        /// </summary>
        string UserId { get; }
    }
}