using SchadLucas.EatSmart.Extensions.System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SchadLucas.EatSmart.Data.Types.Collections
{
    public class ObservableDisposableObjectCollection<T> : ObservableCollection<T> where T : IDisposableObject
    {
        private void OnItemDisposed(object sender, System.EventArgs e)
        {
            Remove((T)sender);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.ForEach((IDisposableObject i) => i.Disposed += OnItemDisposed);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.ForEach((IDisposableObject i) => i.Disposed -= OnItemDisposed);
                    break;
            }
        }
    }
}