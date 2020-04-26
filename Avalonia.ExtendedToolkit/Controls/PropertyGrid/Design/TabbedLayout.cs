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


        public new static readonly StyledProperty<object> SelectedContentProperty =
            TabControl.SelectedContentProperty.AddOwner<TabbedLayout>();

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


        public new static readonly StyledProperty<IDataTemplate> SelectedContentTemplateProperty =
            TabControl.SelectedContentTemplateProperty.AddOwner<TabbedLayout>();

        /// <summary>
        /// Initializes the <see cref="TabbedLayout"/> class.
        /// </summary>
        static TabbedLayout()
        {
        }

        /// <summary>
        /// property grid command
        /// </summary>
        public ICommand ClosePropertyTabCommand { get; internal set; }

        /// <summary>
        /// property grid command
        /// </summary>
        public ICommand ShowExtendedEditorCommand { get; internal set; }

        /// <summary>
        /// Gets or sets the item header property.
        /// </summary>
        /// <value>The item header property.</value>
        public string ItemHeaderProperty { get; set; }

        public static readonly AttachedProperty<bool> CanCloseProperty =
            AvaloniaProperty.RegisterAttached<TabbedLayout, IControl, bool>("CanClose");

        public static bool GetCanClose(IControl element)
        {
            if (element == null)
                throw new ArgumentNullException("obj");
            return element.GetValue(CanCloseProperty);
        }

        public static void SetCanClose(IControl element, bool value)
        {
            if (element == null)
                throw new ArgumentNullException("obj");

            element.SetValue(CanCloseProperty, value);
        }

        public static readonly AttachedProperty<string> HeaderProperty =
            AvaloniaProperty.RegisterAttached<TabbedLayout, IControl, string>
            ("Header", defaultValue: "Unknown");

        public static string GetHeader(IControl element)
        {
            if (element == null)
                throw new ArgumentNullException("obj");
            return element.GetValue(HeaderProperty);
        }

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
            ClosePropertyTabCommand = ReactiveCommand.Create(
               () => OnClosePropertyTabCommand(), CanClosePropertyTab()
               , outputScheduler: RxApp.MainThreadScheduler);

            ShowExtendedEditorCommand = ReactiveCommand.Create<object>(x => OnShowExtendedEditor(x)
            , outputScheduler: RxApp.MainThreadScheduler);

            SelectedContentProperty.Changed.AddClassHandler<TabbedLayout>((o, e) => OnSelectedContentChanged(o, e));


            this.ItemContainerGenerator.Materialized += ItemContainerGenerator_Materialized;
        }

        private void OnSelectedContentChanged(TabbedLayout o, AvaloniaPropertyChangedEventArgs e)
        {
            TabItem oldTabItem = GetTabItem(e.OldValue);
            if(oldTabItem!=null)
            {
                oldTabItem.IsSelected = false;
            }


            if (e.NewValue == null)
                return;

            TabItem tabItem = GetTabItem(e.NewValue);

            if (tabItem != null)
            {
                tabItem.IsSelected = true;
            }

        }


        private TabItem GetTabItem(object item)
        {
            TabItem tabItem = item as TabItem;

            if (tabItem == null)
            {
                tabItem = (item as IControl)?.Parent as TabItem;
            }
            return tabItem;
        }






        private void ItemContainerGenerator_Materialized(object sender, ItemContainerEventArgs e)
        {
            if (sender is TabbedLayoutItem)
            {

            }


            var tab = e.Containers.FirstOrDefault().Item as TabbedLayoutItem;

            if (tab != null)
            {
                var item = tab.Content;

                //TODO: Assign PG as DataContext here?
                //tab.DataContext = item;

                var layout = item as IControl;
                if (!string.IsNullOrEmpty(ItemHeaderProperty))
                {
                    var bHeader = new Binding(ItemHeaderProperty)
                    {
                        Source = item,
                        Mode = BindingMode.OneWay,
                    };

                    tab.Bind(HeaderedContentControl.HeaderProperty, bHeader);
                }
                else
                {
                    if (layout != null)
                    {
                        //tab.Header = GetHeader(layout);
                        //tab.CanClose = GetCanClose(layout);
                    }
                }

                if (item is GridEntry)
                {
                    var binding = new Binding("IsVisible")
                    {
                        Source = item,
                        Mode = BindingMode.OneWay,
                    };
                    tab.Bind(Visual.IsVisibleProperty, binding);
                }

                tab.PropertyChanged += TabItem_PropertyChanged;

            }






        }

        private void TabItem_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(TabControl.IsVisible))
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


        private IObservable<bool> CanClosePropertyTab()
        {
            return this.WhenAny(x => x, (control) =>
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
            });
        }

        private void OnClosePropertyTabCommand()
        {
            if (SelectedItem != null)
            {
                Items.OfType<object>().ToList()
                      .Remove(SelectedItem);
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

                // Try inserting extended tab after currently selected one
                if (SelectedItem != null)
                    Items.OfType<object>().ToList().Insert(Items.OfType<object>()
                        .ToList().IndexOf(SelectedItem) + 1, extendedTab);
                else
                    Items.OfType<object>().ToList()
                        .Add(extendedTab);

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

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new TabItemContainerGenerator(this);
        }




        protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        {

            if (SelectedContent != null || SelectedIndex == -1)
            {
                return;
            }

            var container = ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as TabItem;

            if (container == null)
            {
                return;
            }

            UpdateSelectedContent(container);
        }

        private void UpdateSelectedContent(IContentControl item)
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

#warning todo
        /// <summary>
        /// Prepares the specified element to display the specified item.
        /// </summary>
        /// <param name="element">Element used to display the specified item.</param>
        /// <param name="item">Specified item.</param>
        //protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        //{
        //    base.PrepareContainerForItemOverride(element, item);

        //    if (element == item)
        //        return;
        //    // Processing items not wrapped with TabbedLayoutItem container

        //    var tab = element as TabbedLayoutItem;
        //    if (tab != null)
        //    {
        //        //TODO: Assign PG as DataContext here?
        //        //tab.DataContext = item;

        //        var layout = item as DependencyObject;
        //        if (layout != null)
        //        {
        //            tab.Header = GetHeader(layout);
        //            tab.CanClose = GetCanClose(layout);
        //        }
        //        else if (!string.IsNullOrEmpty(ItemHeaderProperty))
        //        {
        //            var bHeader = new Binding(ItemHeaderProperty)
        //            {
        //                Source = item,
        //                Mode = BindingMode.OneWay,
        //                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        //            };

        //            tab.SetBinding(HeaderedContentControl.HeaderProperty, bHeader);
        //        }

        //        if (item is GridEntry)
        //        {
        //            var binding = new Binding("IsVisible")
        //            {
        //                Source = item,
        //                Mode = BindingMode.OneWay,
        //                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //                Converter = visibilityConverter
        //            };
        //            tab.SetBinding(UIElement.VisibilityProperty, binding);
        //        }

        //        tab.IsVisibleChanged += OnTabVisibilityChanged;
        //    }
        //}

        internal GridEntry GetFirstVisibleEntry()
        {
            return Items.OfType<GridEntry>().FirstOrDefault(item => item.IsVisible);
        }

        internal int GetVisibleEntryCount()
        {
            return Items.OfType<GridEntry>().Count(item => item.IsVisible);
        }

        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own ItemContainer.
        /// </summary>
        /// <param name="item">Specified item.</param>
        /// <returns>
        /// Returns true if the item is its own ItemContainer; otherwise, false.
        /// </returns>
        //protected override bool IsItemItsOwnContainerOverride(object item)
        //{
        //    return item is TabbedLayoutItem;
        //}

        /// <summary>
        /// Creates or identifies the element used to display the specified item.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.TabItem"/>.
        /// </returns>
        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    return new TabbedLayoutItem();
        //}
    }
}
