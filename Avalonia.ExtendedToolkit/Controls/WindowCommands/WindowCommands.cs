using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;

namespace Avalonia.ExtendedToolkit.Controls
{
    //should be toolbar but avalonia have no right now
    public class WindowCommands : HeaderedItemsControl, INotifyPropertyChanged
    {
        public Type StyleKey => typeof(WindowCommands);

        /// <summary>
        /// Gets or sets the value indicating current theme.
        /// </summary>
        public WindowCommandTheme Theme
        {
            get { return (WindowCommandTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly StyledProperty<WindowCommandTheme> ThemeProperty =
            AvaloniaProperty.Register<WindowCommands, WindowCommandTheme>(nameof(Theme));

        ///// <summary>
        ///// Gets or sets the value indicating light theme template.
        ///// </summary>
        //public ControlTemplate LightTemplate
        //{
        //    get { return (ControlTemplate)GetValue(LightTemplateProperty); }
        //    set { SetValue(LightTemplateProperty, value); }
        //}

        //public static readonly StyledProperty<ControlTemplate> LightTemplateProperty =
        //    AvaloniaProperty.Register<WindowCommands, ControlTemplate>(nameof(LightTemplate));

        public Classes LightClasses
        {
            get { return (Classes)GetValue(LightClassesProperty); }
            set { SetValue(LightClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> LightClassesProperty =
            AvaloniaProperty.Register<WindowCommands, Classes>(nameof(LightClasses));

        ///// <summary>
        ///// Gets or sets the value indicating light theme template.
        ///// </summary>
        //public ControlTemplate DarkTemplate
        //{
        //    get { return (ControlTemplate)GetValue(DarkTemplateProperty); }
        //    set { SetValue(DarkTemplateProperty, value); }
        //}

        //public static readonly StyledProperty<ControlTemplate> DarkTemplateProperty =
        //    AvaloniaProperty.Register<WindowCommands, ControlTemplate>(nameof(DarkTemplate));

        public Classes DarkClasses
        {
            get { return (Classes)GetValue(DarkClassesProperty); }
            set { SetValue(DarkClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> DarkClassesProperty =
            AvaloniaProperty.Register<WindowCommands, Classes>(nameof(DarkClasses));

        /// <summary>
        /// Gets or sets the value indicating whether to show the separators.
        /// </summary>
        public bool ShowSeparators
        {
            get { return (bool)GetValue(ShowSeparatorsProperty); }
            set { SetValue(ShowSeparatorsProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowSeparatorsProperty =
            AvaloniaProperty.Register<WindowCommands, bool>(nameof(ShowSeparators));

        /// <summary>
        /// Gets or sets the value indicating whether to show the last separator.
        /// </summary>
        public bool ShowLastSeparator
        {
            get { return (bool)GetValue(ShowLastSeparatorProperty); }
            set { SetValue(ShowLastSeparatorProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowLastSeparatorProperty =
            AvaloniaProperty.Register<WindowCommands, bool>(nameof(ShowLastSeparator));

        /// <summary>
        /// Gets or sets the value indicating separator height.
        /// </summary>
        public int SeparatorHeight
        {
            get { return (int)GetValue(SeparatorHeightProperty); }
            set { SetValue(SeparatorHeightProperty, value); }
        }

        public static readonly StyledProperty<int> SeparatorHeightProperty =
            AvaloniaProperty.Register<WindowCommands, int>(nameof(SeparatorHeight), defaultValue: 15);

        public MetroWindow ParentWindow
        {
            get { return (MetroWindow)GetValue(ParentWindowProperty); }
            set { SetValue(ParentWindowProperty, value); }
        }

        public static readonly StyledProperty<MetroWindow> ParentWindowProperty =
            AvaloniaProperty.Register<WindowCommands, MetroWindow>(nameof(ParentWindow));

        public bool ShowTitleBar
        {
            get { return (bool)GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        public static readonly StyledProperty<bool> ShowTitleBarProperty =
            AvaloniaProperty.Register<WindowCommands, bool>(nameof(ShowTitleBar));

        public new Classes Classes
        {
            get { return (Classes)GetValue(ClassesProperty); }
            set { SetValue(ClassesProperty, value); }
        }

        public static readonly StyledProperty<Classes> ClassesProperty =
            AvaloniaProperty.Register<WindowCommands, Classes>(nameof(Classes));

        public WindowCommands()
        {
            ThemeProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnThemeChanged(o, e));
            ShowSeparatorsProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnShowSeparatorsChanged(o, e));
            ShowLastSeparatorProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnShowLastSeparatorChanged(o, e));
            ParentWindowProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnParentWindowChanged(o, e));

            (Items as AvaloniaList<object>).CollectionChanged += OnItemsChanged;

            this.Initialized += WindowCommands_Initialized;

            ClassesProperty.Changed.AddClassHandler<WindowCommands>((o, e) => OnClassesChanged(o, e));
        }

        private void OnClassesChanged(WindowCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            Classes classes = e.NewValue as Classes;
            if(classes!=null)
            {
                if(LightClasses!=null)
                {
                    foreach (var item in LightClasses)
                    {
                        base.Classes.Remove(item);
                    }
                }

                if (DarkClasses != null)
                {
                    foreach (var item in DarkClasses)
                    {
                        base.Classes.Remove(item);
                    }
                }

                foreach (var item in classes)
                {
                    base.Classes.Add(item);
                }
            }
        }

        private void OnParentWindowChanged(WindowCommands o, AvaloniaPropertyChangedEventArgs e)
        {
            MetroWindow metroWindow = e.NewValue as MetroWindow;
            if (metroWindow != null)
            {
                ShowTitleBar = metroWindow.ShowTitleBar;
            }
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
            if (parentWindow == null)
            {
                this.ParentWindow = this.TryFindParent<MetroWindow>();
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
                //if (o.LightTemplate != null)
                //{
                //    o.SetValue(TemplateProperty, o.LightTemplate);
                //}

                if (o.LightClasses != null)
                {
                    if (o.DarkClasses != null)
                    {
                        foreach (var item in o.DarkClasses)
                        {
                            o.Classes.Remove(item);
                        }
                    }

                    foreach (var item in o.LightClasses)
                    {
                        o.Classes.Remove(item);
                    }

                    o.Classes.AddRange(o.LightClasses);

                    //o.SetValue(TemplateProperty, o.LightTemplate);
                }
            }
            else if ((WindowCommandTheme)e.NewValue == WindowCommandTheme.Dark)
            {
                //if (o.DarkTemplate != null)
                //{
                //    o.SetValue(TemplateProperty, o.DarkTemplate);
                //}
                if (o.DarkClasses != null)
                {
                    if (o.LightClasses != null)
                    {
                        foreach (var item in o.LightClasses)
                        {
                            o.Classes.Remove(item);
                        }
                    }

                    foreach (var item in o.DarkClasses)
                    {
                        o.Classes.Remove(item);
                    }

                    o.Classes.AddRange(o.DarkClasses);
                }
            }
        }

        private void ResetSeparators(bool reset = true)
        {
            if (Items.OfType<object>().Any() == false)
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

            var index = ItemContainerGenerator.IndexFromContainer(parent);
            return (WindowCommandsItem)ItemContainerGenerator.ContainerFromIndex(index);
            //return (WindowCommandsItem)this.ItemContainerGenerator.ContainerFromItem(item);
        }

        private IEnumerable<WindowCommandsItem> GetWindowCommandsItems()
        {
            return (from object item in (IEnumerable)this.Items select this.GetWindowCommandsItem(item)).Where(i => i != null);
        }

        //private Window _parentWindow;

        //public Window ParentWindow
        //{
        //    get { return _parentWindow; }
        //    set
        //    {
        //        if (Equals(_parentWindow, value))
        //        {
        //            return;
        //        }
        //        _parentWindow = value;

        //        //this.RaisePropertyChanged("ParentWindow");
        //    }
        //}
    }
}
