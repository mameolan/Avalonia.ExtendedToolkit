using System;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// The default tabbed layout.
    /// </summary>
    public class TabbedLayout : TabControl
    {

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(TabbedLayout);

        /// <summary>
        /// The fallback header for a tab if no header custom is provided.
        /// </summary>
        public const string DefaultHeader = "Unknown";

        /// <summary>
        /// Gets or sets the content of the selected tab.
        /// </summary>
        /// <value>
        /// The content of the selected tab.
        /// </value>
        ///
        public new object SelectedContent
        {
            get { return (object)GetValue(SelectedContentProperty); }
            set { SetValue(SelectedContentProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedContent"/>
        /// </summary>
        //.orig
        //public new static readonly StyledProperty<object> SelectedContentProperty =
        //    TabControl.SelectedContentProperty.AddOwner<TabbedLayout>();

        /// <summary>
        /// Gets or sets the content template for the selected tab.
        /// </summary>
        /// <value>
        /// The content template of the selected tab.
        /// </value>
        public new IDataTemplate SelectedContentTemplate
        {
            get { return (IDataTemplate)GetValue(SelectedContentTemplateProperty); }
            set { SetValue(SelectedContentTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedContentTemplate"/>
        /// </summary>
        //.orig
        //public new static readonly StyledProperty<IDataTemplate> SelectedContentTemplateProperty =
        //    TabControl.SelectedContentTemplateProperty.AddOwner<TabbedLayout>();

        /// <summary>
        /// Initializes the <see cref="TabbedLayout"/> class.
        /// </summary>
        static TabbedLayout()
        {
        }


        /// <summary>
        /// property grid command
        /// </summary>
        public ICommand ClosePropertyTabCommand
        {
            get { return (ICommand)GetValue(ClosePropertyTabCommandProperty); }
            internal set { SetValue(ClosePropertyTabCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ClosePropertyTabCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ClosePropertyTabCommandProperty =
            AvaloniaProperty.Register<TabbedLayout, ICommand>(nameof(ClosePropertyTabCommand));

        /// <summary>
        /// property grid command
        /// </summary>
        public ICommand ShowExtendedEditorCommand
        {
            get { return (ICommand)GetValue(ShowExtendedEditorCommandProperty); }
            set { SetValue(ShowExtendedEditorCommandProperty, value); }
        }

        /// <summary>
        /// <see cref="ShowExtendedEditorCommand"/>
        /// </summary>
        public static readonly StyledProperty<ICommand> ShowExtendedEditorCommandProperty =
            AvaloniaProperty.Register<TabbedLayout, ICommand>(nameof(ShowExtendedEditorCommand));

        /// <summary>
        /// Gets or sets the item header property.
        /// </summary>
        /// <value>The item header property.</value>
        public string ItemHeaderProperty { get; set; }

        /// <summary>
        /// CanClose AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<bool> CanCloseProperty =
            AvaloniaProperty.RegisterAttached<TabbedLayout, Control, bool>("CanClose");

        /// <summary>
        /// get CanClose
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetCanClose(Control element)
        {
            if (element == null)
                throw new ArgumentNullException("obj");
            return element.GetValue<bool>(CanCloseProperty);
        }

        /// <summary>
        /// set CanClose
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCanClose(Control element, bool value)
        {
            if (element == null)
                throw new ArgumentNullException("obj");

            element.SetValue(CanCloseProperty, value);
        }

        /// <summary>
        /// Header AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderProperty =
            AvaloniaProperty.RegisterAttached<TabbedLayout, Control, string>
            ("Header", defaultValue: "Unknown");

        /// <summary>
        /// get Header
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeader(Control element)
        {
            if (element == null)
                throw new ArgumentNullException("obj");
            return element.GetValue<string>(HeaderProperty);
        }

        /// <summary>
        /// set Header
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeader(Control element, string value)
        {
            if (element == null)
                throw new ArgumentNullException("obj");
            if (string.IsNullOrEmpty(value))
                value = DefaultHeader;
            element.SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabbedLayout"/> class.
        /// </summary>
        public TabbedLayout()
        {
            //somehow not refreshed in command
            var canExecuteClosePropertyTab = this.WhenAny(x => x, (control) => CanCloseExecute());

            ClosePropertyTabCommand = ReactiveCommand.Create(
               () => OnClosePropertyTabCommand()
               , outputScheduler: RxApp.MainThreadScheduler);

            ShowExtendedEditorCommand = ReactiveCommand.Create<object>(x => OnShowExtendedEditor(x)
            , outputScheduler: RxApp.MainThreadScheduler);

            SelectedContentProperty.Changed.AddClassHandler<TabbedLayout>((o, e) => OnSelectedContentChanged(o, e));
            //.orig
            //this.ItemContainerGenerator.Materialized += ItemContainerGenerator_Materialized;
        }

        internal bool CanCloseExecute()
        {
            var layoutItem = SelectedItem as TabbedLayoutItem;
            if (layoutItem != null)
            {
                return layoutItem.CanClose;
            }
            else
            {
                var obj = SelectedItem as Control;
                if (obj != null)
                {
                    return GetCanClose(obj);
                }
                return true;
            }
        }

        private void OnSelectedContentChanged(TabbedLayout o, AvaloniaPropertyChangedEventArgs e)
        {
            TabbedLayoutItem oldTabItem = GetTabbedLayoutItem(e.OldValue);
            if (oldTabItem != null)
            {
                oldTabItem.IsSelected = false;
            }

            if (e.NewValue == null)
                return;

            TabbedLayoutItem tabItem = GetTabbedLayoutItem(e.NewValue);

            if (tabItem != null)
            {
                if (tabItem.ClosePropertyTabCommand == null)
                {
                    tabItem.ClosePropertyTabCommand = ClosePropertyTabCommand;
                }

                tabItem.IsSelected = true;
            }
        }

        private TabbedLayoutItem GetTabbedLayoutItem(object item)
        {
            TabbedLayoutItem tabItem = item as TabbedLayoutItem;

            if (tabItem == null)
            {
                tabItem = (item as Control)?.Parent as TabbedLayoutItem;
            }
            return tabItem;
        }
        //.orig
        //private void ItemContainerGenerator_Materialized(object sender, ItemContainerEventArgs e)
        //{
        //    var tab = e.Containers.FirstOrDefault().Item as TabbedLayoutItem;

        //    if (tab != null)
        //    {
        //        if (tab.ClosePropertyTabCommand == null)
        //        {
        //            tab.ClosePropertyTabCommand = ClosePropertyTabCommand;
        //            tab.CanClose = true;
        //        }

        //        var item = tab.Content;

        //        //TODO: Assign PG as DataContext here?
        //        //tab.DataContext = item;

        //        var layout = item as Control;
        //        if (!string.IsNullOrEmpty(ItemHeaderProperty))
        //        {
        //            var bHeader = new Binding(ItemHeaderProperty)
        //            {
        //                Source = item,
        //                Mode = BindingMode.OneWay,
        //            };

        //            tab.Bind(HeaderedContentControl.HeaderProperty, bHeader);
        //        }
        //        else
        //        {
        //            if (layout != null)
        //            {
        //                //tab.Header = GetHeader(layout);
        //                //tab.CanClose = GetCanClose(layout);
        //            }
        //        }

        //        if (item is GridEntry)
        //        {
        //            //var binding = new Binding("IsVisible")
        //            //{
        //            //    Source = item,
        //            //    Mode = BindingMode.OneWay,
        //            //};
        //            //tab.Bind(Visual.IsVisibleProperty, binding);
        //        }

        //        tab.PropertyChanged -= TabItem_PropertyChanged;
        //        tab.PropertyChanged += TabItem_PropertyChanged;
        //    }
        //}

        private void TabItem_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(TabItem.IsSelected))
            {
                //var propChanged = new AvaloniaPropertyChangedEventArgs
                //    (sender as AvaloniaObject
                //    , e.Property
                //    , e.OldValue
                //    , e.NewValue
                //    , e.Priority);

                //OnTabVisibilityChanged(sender, propChanged);
            }
        }

        private void OnTabVisibilityChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            var tabItem = sender as TabbedLayoutItem;
            if (tabItem == null || tabItem.DataContext == null)
                return;

            bool isVisible = (bool)e.NewValue;

            if (isVisible && GetVisibleEntryCount() == 1)
            {
                var visibleEntry = GetFirstVisibleEntry();
                if (SelectedItem != visibleEntry)
                    SelectedItem = visibleEntry;
            }
            else if (tabItem.IsSelected)
            {
                if (GetVisibleEntryCount() == 0)
                {
                    SelectedItem = null;
                    return;
                }

                if (Items.OfType<object>().ToList().IndexOf(tabItem.DataContext) > 0)
                    SelectedIndex--;
                else if (Items.OfType<object>().Count() > 1)
                    SelectedIndex++;
            }
        }

        private void OnClosePropertyTabCommand()
        {
            var item = SelectedItem as TabbedLayoutItem;

            if (item != null && item.CanClose && Items.OfType<TabbedLayoutItem>().Count(x=> x.IsVisible) > 1)
            {
                if (item is ExtendedPropertyEditorTab)
                {
                    //only set is visible to false
                    //otherwise the editor controls have ui problems (?)
                    item.IsVisible = false;
                }
                else
                {
                    var items = Items.OfType<object>().ToList();
                    items.Remove(item);
                    ItemsSource = items;
                }
                SelectedItem = Items.OfType<object>().FirstOrDefault(x=> (x as Control)?.IsVisible==true);
            }
        }

        // TODO: Optimize implementation
        // TODO: move logic to public api
        // It should be possible creating/opening extended tabs from code
        private void OnShowExtendedEditor(object sender /*object sender, ExecutedRoutedEventArgs e*/)
        {
            var value = sender as PropertyItemValue;
            if (value == null)
                return;
            var property = value.ParentProperty;
            if (property == null)
                return;

            // Try getting already opened extended tab
            var extendedTab = FindExtendedEditorTab(property);
            if (extendedTab != null)
            {
                // Activate alreay opened tab
                extendedTab.IsVisible = true;
                SelectedItem = extendedTab;
            }
            else
            {
                // TODO: Access the PropertyGrid to get TypeEditor or PropertyEditor ExtendedTemplate here!

                // Check whether property value editor is actually an extended one
                if (property.Editor.ExtendedTemplate == null)
                    return;

                // create new extended tab, add to the tabs collection and activate it
                extendedTab = new ExtendedPropertyEditorTab(property);

                var items = Items.OfType<object>().ToList();
                // Try inserting extended tab after currently selected one
                if (SelectedItem != null)
                {
                    int index = items.IndexOf(SelectedItem) + 1;
                    items.Insert(index, extendedTab);
                }
                else
                {
                    items.Add(extendedTab);
                }
                //.edited
                ItemsSource = items;

                // Activate extended tab
                SelectedItem = extendedTab;
            }
        }

        /// <summary>
        /// Finds the extended editor tab.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Tab associated with property.</returns>
        public TabbedLayoutItem FindExtendedEditorTab(PropertyItem property)
        {
            return Items
              .OfType<ExtendedPropertyEditorTab>()
              .FirstOrDefault(tab => tab.Property == property);
        }

        /// <summary>
        /// uses TabItemContainerGenerator as generator
        /// </summary>
        /// <returns></returns>
        //.orig
        //protected override ItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    return new TabItemContainerGenerator(this);
        //}

        /// <summary>
        /// updates selected content
        /// </summary>
        /// <param name="e"></param>
        //.orig
        //protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        //{
        //    if (SelectedContent != null || SelectedIndex == -1)
        //    {
        //        return;
        //    }

        //    var container = ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as TabItem;

        //    if (container == null)
        //    {
        //        return;
        //    }

        //    UpdateSelectedContent(container);
        //}

        private void UpdateSelectedContent(ContentControl item)
        {
            if (SelectedContentTemplate != item.ContentTemplate)
            {
                SelectedContentTemplate = item.ContentTemplate;
            }

            if (SelectedContent != item.Content)
            {
                SelectedContent = item.Content;
            }
        }

        internal GridEntry GetFirstVisibleEntry()
        {
            return Items.OfType<GridEntry>().FirstOrDefault(item => item.IsVisible);
        }

        internal int GetVisibleEntryCount()
        {
            return Items.OfType<GridEntry>().Count(item => item.IsVisible);
        }
    }
}
