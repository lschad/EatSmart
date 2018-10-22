using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

#pragma warning disable 1584, 1712

namespace SchadLucas.EatSmart.Extensions.Autofac.Builder
{
    public static class RegistrationBuilderExtensions
    {
        /// <summary>
        ///     Filters the scanned types to include only those assignable to the provided type.
        /// </summary>
        /// <param name="registration">Registration to filter types from.</param>
        /// <typeparam name="T">
        ///     The type or interface which all classes must be assignable from.
        /// </typeparam>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2));
        }

        /// <inheritdoc cref="AssignableTo{T1,T2}" />
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2, T3>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2))
                   .AssignableTo(typeof(T3));
        }

        /// <inheritdoc cref="AssignableTo{T1,T2}" />
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2, T3, T4>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2))
                   .AssignableTo(typeof(T3))
                   .AssignableTo(typeof(T4));
        }

        /// <inheritdoc cref="AssignableTo{T1,T2}" />
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2, T3, T4, T5>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2))
                   .AssignableTo(typeof(T3))
                   .AssignableTo(typeof(T4))
                   .AssignableTo(typeof(T5));
        }

        /// <inheritdoc cref="AssignableTo{T1,T2}" />
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2, T3, T4, T5, T6>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2))
                   .AssignableTo(typeof(T3))
                   .AssignableTo(typeof(T4))
                   .AssignableTo(typeof(T5))
                   .AssignableTo(typeof(T6));
        }

        /// <inheritdoc cref="AssignableTo{T1,T2}" />
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2, T3, T4, T5, T6, T7>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2))
                   .AssignableTo(typeof(T3))
                   .AssignableTo(typeof(T4))
                   .AssignableTo(typeof(T5))
                   .AssignableTo(typeof(T6))
                   .AssignableTo(typeof(T7));
        }

        /// <inheritdoc cref="AssignableTo{T1,T2}" />
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AssignableTo<T1, T2, T3, T4, T5, T6, T7, T8>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration)
        {
            return registration
                   .AssignableTo(typeof(T1))
                   .AssignableTo(typeof(T2))
                   .AssignableTo(typeof(T3))
                   .AssignableTo(typeof(T4))
                   .AssignableTo(typeof(T5))
                   .AssignableTo(typeof(T6))
                   .AssignableTo(typeof(T7))
                   .AssignableTo(typeof(T8));
        }
    }
}