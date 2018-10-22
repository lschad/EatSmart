using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace SchadLucas.EatSmart.UI.Views
{
    public interface IFoodView : IView { }

    /// <summary>
    ///     Interaction logic for FoodView.xaml
    /// </summary>
    [UsedImplicitly]
    public partial class FoodView : IFoodView
    {
        public FoodView()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // scroll to the selected item once the selection is changed. that's important because
            // the selection gets changed programmatically when a new food is added
            if (sender is ListBox listBox && e.AddedItems is IEnumerable<object> items)
            {
                var item = items.FirstOrDefault();
                if (item != null)
                {
                    listBox.ScrollIntoView(item);
                }
            }
        }
    }
}