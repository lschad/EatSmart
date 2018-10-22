using System;

namespace SchadLucas.EatSmart.Data.Types
{
    public interface IDisposableObject : IDisposable
    {
        event EventHandler Disposed;

        event EventHandler Disposing;
    }
}