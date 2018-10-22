using JetBrains.Annotations;
using System;
using SchadLucas.EatSmart.Data.Types;

namespace SchadLucas.EatSmart.ViewModels
{
    [UsedImplicitly]
    public interface IViewModel : IDataContextObject
    {
        event EventHandler Disabled;

        event EventHandler Disabling;

        event EventHandler Enabled;

        event EventHandler Enabling;

        bool IsActive { get; }

        string WindowTitle { get; }

        void Disable();

        void Enable();

        void SetArguments<T>(T[] args);
    }
}