using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Utilities
{
    public static class AwaitableTask
    {
        public static ConfiguredTaskAwaitable<T> Run<T>(Func<T> method, bool configureAwait = false)
        {
            return Task.Run(method).ConfigureAwait(configureAwait);
        }

        public static ConfiguredTaskAwaitable<T> Run<T>(Func<T> method, CancellationToken token, bool configureAwait = false)
        {
            return Task.Run(method, token).ConfigureAwait(configureAwait);
        }

        public static ConfiguredTaskAwaitable Run(Action method, bool configureAwait = false)
        {
            return Task.Run(method).ConfigureAwait(configureAwait);
        }

        public static ConfiguredTaskAwaitable Run(Action method, CancellationToken token, bool configureAwait = false)
        {
            return Task.Run(method, token).ConfigureAwait(configureAwait);
        }

        public static async Task<T> RunAsync<T>(Func<T> method, bool configureAwait = false)
        {
            return await Run(method);
        }

        public static async Task<T> RunAsync<T>(Func<T> method, CancellationToken token, bool configureAwait = false)
        {
            return await Run(method, token, configureAwait);
        }

        public static async Task RunAsync(Action method, bool configureAwait = false)
        {
            await Run(method, configureAwait);
        }

        public static async Task RunAsync(Action method, CancellationToken token, bool configureAwait = false)
        {
            await Run(method, token, configureAwait);
        }
    }
}