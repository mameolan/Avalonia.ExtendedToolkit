using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using System.Linq;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    public class TabbedLayoutContainerGenerator : ItemContainerGenerator<TabbedLayout>
    {
        public TabbedLayoutContainerGenerator(TabbedLayout owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
        {
            Owner = owner;

        }

        public new TabbedLayout Owner { get; }

        protected override IControl CreateContainer(object element)
        {
            var item = (element as ItemsControl);

            if (item == Owner)
                return base.CreateContainer(element);


            var tab = element as TabbedLayoutItem;
            if (tab != null)
            {
                //TODO: Assign PG as DataContext here?
                //tab.DataContext = item;

                var layout = item as Control;
                if (layout != null)
                {
                    tab.Header = TabbedLayout.GetHeader(layout);
                    tab.CanClose = TabbedLayout.GetCanClose(layout);
                }
                else if (!string.IsNullOrEmpty(tab.Header as string))
                {
                    var bHeader = new Binding("Header")
                    {
                        Source = item,
                        Mode = BindingMode.OneWay,
                        //UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };

                    tab.Bind(HeaderedContentControl.HeaderProperty, bHeader);
                }

                if (item is GridEntry)
                {
                    var binding = new Binding("IsVisible")
                    {
                        Source = item,
                        Mode = BindingMode.OneWay,
                        //UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        //Converter = visibilityConverter
                    };

                    tab.Bind(Visual.IsVisibleProperty, binding);
                }

                //tab.IsVisibleChanged += OnTabVisibilityChanged;
                tab.PropertyChanged += Tab_PropertyChanged;
            }




            return base.CreateContainer(element);
        }

        private void Tab_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.Property.Name==nameof(TabControl.IsVisible))
            {
                var propChanged = new AvaloniaPropertyChangedEventArgs
                    (sender as AvaloniaObject
                    , e.Property
                    , e.OldValue
                    , e.NewValue
                    , e.Priority);

                OnTabVisibilityChanged(sender, propChanged);
            }
        }

        private void OnTabVisibilityChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            var tabItem = sender as TabbedLayoutItem;
            if (tabItem == null || tabItem.DataContext == null)
                return;

            bool isVisible = (bool)e.NewValue;

            if (isVisible && Owner.GetVisibleEntryCount() == 1)
            {
                var visibleEntry = Owner.GetFirstVisibleEntry();
                if (Owner.SelectedItem != visibleEntry)
                    Owner.SelectedItem = visibleEntry;
            }
            else if (tabItem.IsSelected)
            {
                if (Owner.GetVisibleEntryCount() == 0)
                {
                    Owner.SelectedItem = null;
                    return;
                }

                if (Owner.Items.OfType<object>().ToList().IndexOf(tabItem.DataContext) > 0)
                    Owner.SelectedIndex--;
                else if (Owner.Items.OfType<object>().Count() > 1)
                    Owner.SelectedIndex++;
            }
        }


    }
}
