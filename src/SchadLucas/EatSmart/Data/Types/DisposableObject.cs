using System;
using System.Diagnostics.CodeAnalysis;

namespace SchadLucas.EatSmart.Data.Types
{
    public abstract class DisposableObject : IDisposableObject
    {
        private bool _isDisposed;

        private event EventHandler DisposedEvent;

        private event EventHandler DisposingEvent;

        /// <summary>
        ///     Raised when the object was disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the object was already disposed.</exception>
        public event EventHandler Disposed
        {
            add
            {
                Assert();
                DisposedEvent += value;
            }
            remove => DisposedEvent -= value;
        }

        /// <summary>
        ///     Raised right before the object gets disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the object was already disposed.</exception>
        public event EventHandler Disposing
        {
            add
            {
                Assert();
                DisposingEvent += value;
            }
            remove => DisposingEvent -= value;
        }

        protected DisposableObject()
        {
            _isDisposed = false;
        }

        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "False Positive. It calls Dispose(false) within OnDispose(true).")]
        ~DisposableObject()
        {
            try
            {
                OnDispose(false);
            }
            catch
            {
                // ignored
            }
        }

        private void OnDispose(bool disposing)
        {
            try
            {
                if (!_isDisposed)
                {
                    DisposingEvent?.Invoke(this, System.EventArgs.Empty);
                    Dispose(disposing);
                    DisposedEvent?.Invoke(this, System.EventArgs.Empty);
                }
            }
            finally
            {
                _isDisposed = true;
                DisposedEvent = null;
            }
        }

        /// <summary>
        ///     Raises the <see cref="ObjectDisposedException" /> if the object has already been disposed.
        /// </summary>
        protected virtual void Assert()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        /// <summary>
        ///     Your implementation of the dispose method
        /// </summary>
        protected abstract void Dispose(bool disposing);

        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "False Positive. It calls Dispose(true) within OnDispose(true). Gc.SuppressFinalize is called in the finally block.")]
        public void Dispose()
        {
            try
            {
                OnDispose(true);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}