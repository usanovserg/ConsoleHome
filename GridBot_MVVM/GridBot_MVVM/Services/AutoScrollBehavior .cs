using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Interactivity;

namespace GridBotMVVM.Services;

public class AutoScrollBehavior : Behavior<DataGrid>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        ((INotifyCollectionChanged)this.AssociatedObject.Items).CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            int count = this.AssociatedObject.Items.Count;
            if (count > 0)
                this.AssociatedObject.ScrollIntoView(this.AssociatedObject.Items[count - 1]);
        }
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.SelectedIndex = this.AssociatedObject.Items.Count - 1;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
        ((INotifyCollectionChanged)this.AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
    }
}
