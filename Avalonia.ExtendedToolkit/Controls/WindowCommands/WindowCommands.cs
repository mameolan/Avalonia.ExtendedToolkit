using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// An Enum representing different themes for window commands.
    /// </summary>
    public enum WindowCommandTheme
    {
        Light,
        Dark
    }

    //should be toolbar but avalonia have no right now
    public class WindowCommands : HeaderedItemsControl, INotifyPropertyChanged
    {
        public WindowCommandTheme Theme
        {
            get { return (WindowCommandTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly StyledProperty<WindowCommandTheme> ThemeProperty =
            AvaloniaProperty.Register<WindowCommands, WindowCommandTheme>(nameof(Theme));

        public ControlTemplate LightTemplate
        {
            get { return (ControlTemplate)GetValue(LightTemplateProperty); }
            set { SetValue(LightTemplateProperty, value); }
        }

        public static readonly StyledProperty<ControlTemplate> LightTemplateProperty =
            AvaloniaProperty.Register<WindowCommands, ControlTemplate>(nameof(LightTemplate));

        public ControlTemplate DarkTemplate
        {
            get { return (ControlTemplate)GetValue(DarkTemplateProperty); }
            set { SetValue(DarkTemplateProperty, value); }
        }

        public static readonly StyledProperty<ControlTemplate> DarkTemplateProperty =
            AvaloniaProperty.Register<WindowCommands, ControlTemplate>(nameof(DarkTemplate));

        public bool ShowSeparators
        {
            get { return (bool)GetValue(ShowSeparatorsProperty); }
            set { SetValue(ShowSeparatorsProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowSeparatorsProperty =
            AvaloniaProperty.Register<WindowCommands, bool>(nameof(ShowSeparators));

        public bool ShowLastSeparator
        {
            get { return (bool)GetValue(ShowLastSeparatorProperty); }
            set { SetValue(ShowLastSeparatorProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowLastSeparatorProperty =
            AvaloniaProperty.Register<WindowCommands, bool>(nameof(ShowLastSeparator));

        public int SeparatorHeight
        {
            get { return (int)GetValue(SeparatorHeightProperty); }
            set { SetValue(SeparatorHeightProperty, value); }
        }

        public static readonly StyledProperty<int> SeparatorHeightProperty =
            AvaloniaProperty.Register<WindowCommands, int>(nameof(SeparatorHeight), defaultValue: 15);

        public WindowCommands()
        {
            ThemeProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnThemeChanged(o, e));
            ShowSeparatorsProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnShowSeparatorsChanged(o, e));
            ShowLastSeparatorProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnShowLastSeparatorChanged(o, e));
            //ItemsProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnItemsChanged(o, e));

            (Items as AvaloniaList<object>).CollectionChanged += OnItemsChanged;
            
            this.Initialized += WindowCommands_Initialized;
        }

        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ResetSeparators();
        }

        private void WindowCommands_Initialized(object sender, EventArgs e)
        {
            var contentPresenter = this.TryFindParent<ContentPresenter>();
            if (contentPresenter != null)
            {
                this.SetValue(DockPanel.DockProperty, contentPresenter.GetValue(DockPanel.DockProperty));
            }

            var parentWindow = this.ParentWindow;
            if (null == parentWindow)
            {
                this.ParentWindow = this.TryFindParent<Window>();
            }
        }

        private void OnShowLastSeparatorChanged(WindowCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }
            ResetSeparators(false);
        }

        private void OnShowSeparatorsChanged(WindowCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }
            ResetSeparators();
        }

        private void OnThemeChanged(WindowCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            // only apply theme if value is changed
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            if ((WindowCommandTheme)e.NewValue == WindowCommandTheme.Light)
            {
                if (o.LightTemplate != null)
                {
                    o.SetValue(TemplateProperty, o.LightTemplate);
                }
            }
            else if ((WindowCommandTheme)e.NewValue == WindowCommandTheme.Dark)
            {
                if (o.DarkTemplate != null)
                {
                    o.SetValue(TemplateProperty, o.DarkTemplate);
                }
            }
        }

        private void ResetSeparators(bool reset = true)
        {
            if (Items.OfType<object>().Count() == 0)
            {
                return;
            }

            var windowCommandsItems = this.GetWindowCommandsItems().ToList();

            if (reset)
            {
                foreach (var windowCommandsItem in windowCommandsItems)
                {
                    windowCommandsItem.IsSeparatorVisible = ShowSeparators;
                }
            }

            var lastContainer = windowCommandsItems.LastOrDefault(i => i.IsVisible);
            if (lastContainer != null)
            {
                lastContainer.IsSeparatorVisible = ShowSeparators && ShowLastSeparator;
            }
        }

        private WindowCommandsItem GetWindowCommandsItem(object item)
        {
            var windowCommandsItem = item as WindowCommandsItem;
            if (windowCommandsItem != null)
            {
                return windowCommandsItem;
            }

            IControl control = item as IControl;
            IControl parent = control.Parent;

            var index= ItemContainerGenerator.IndexFromContainer(parent);
            return (WindowCommandsItem)ItemContainerGenerator.ContainerFromIndex(index);
            //return (WindowCommandsItem)this.ItemContainerGenerator.ContainerFromItem(item);
        }

        private IEnumerable<WindowCommandsItem> GetWindowCommandsItems()
        {
            return (from object item in (IEnumerable)this.Items select this.GetWindowCommandsItem(item)).Where(i => i != null);
        }

        private Window _parentWindow;

        public Window ParentWindow
        {
            get { return _parentWindow; }
            set
            {
                if (Equals(_parentWindow, value))
                {
                    return;
                }
                _parentWindow = value;
                //this.RaisePropertyChanged("ParentWindow");
            }
        }
    }
}