using NLog;
using SchadLucas.EatSmart.Data.Types;
using System;

namespace SchadLucas.EatSmart.ViewModels
{
    public abstract class ViewModel : DataContextObject, IViewModel
    {
        private bool _isActive;
        private string _windowTitle;

        public virtual event EventHandler Disabled;
        public virtual event EventHandler Disabling;
        public virtual event EventHandler Enabled;
        public virtual event EventHandler Enabling;

        /// <summary>
        ///     Gets the <see cref="NLog.Logger" /> of the deriving Class
        /// </summary>
        protected Logger Logger => LogManager.GetLogger(GetType().Name);

        public bool IsActive
        {
            get => _isActive;
            protected set => SetField(ref _isActive, value);
        }
        public string WindowTitle
        {
            get => _windowTitle;
            protected set => SetField(ref _windowTitle, value);
        }

        protected ViewModel()
        {
            Logger.Debug($"Instantiated {Logger.Name}");
        }

        protected virtual void OnDisabled()
        {
            Logger.Debug($"Disabled ViewModel \"{GetType().Name}\"");
            Disabled?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnDisabling()
        {
            Logger.Debug($"Disabling ViewModel \"{GetType().Name}\"");
            Disabling?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnEnabled()
        {
            Logger.Debug($"Enabled ViewModel \"{GetType().Name}\"");
            Enabled?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnEnabling()
        {
            Logger.Debug($"Enabling ViewModel \"{GetType().Name}\"");
            WindowTitle = GetType().Name;
            Enabling?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Disable()
        {
            OnDisabling();
            IsActive = false;
            OnDisabled();
        }

        public virtual void Enable()
        {
            OnEnabling();
            IsActive = true;
            OnEnabled();
        }

        public virtual void SetArguments<T>(T[] args)
        {
        }
    }
}