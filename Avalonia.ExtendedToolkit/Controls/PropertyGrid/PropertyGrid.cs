using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    public partial class PropertyGrid : TemplatedControl, INotifyPropertyChanged
    {
        public Type StyleKey => typeof(PropertyGrid);

        private static Attribute[] DefaultPropertiesFilter = new Attribute[]
        {
            new PropertyFilterAttribute(
            PropertyFilterOptions.SetValues
            | PropertyFilterOptions.UnsetValues
            | PropertyFilterOptions.Valid)
        };

        private List<BrowsablePropertyAttribute> browsableProperties = new List<BrowsablePropertyAttribute>();
        private List<BrowsableCategoryAttribute> browsableCategories = new List<BrowsableCategoryAttribute>();

        private object[] currentObjects;

        private EditorCollection _Editors = new EditorCollection();
        /// <summary>
        /// Gets the editors collection.
        /// </summary>
        /// <value>The editors collection.</value>
        public EditorCollection Editors
        {
            get { return _Editors; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has properties.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has properties; otherwise, <c>false</c>.
        /// </value>
        public bool HasProperties
        {
            get { return _properties != null && _properties.Count > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has categories.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has categories; otherwise, <c>false</c>.
        /// </value>
        public bool HasCategories
        {
            get { return _categories != null && _categories.Count > 0; }
        }



        public string CurrentDescription
        {
            get { return (string)GetValue(CurrentDescriptionProperty); }
            set { SetValue(CurrentDescriptionProperty, value); }
        }


        public static readonly StyledProperty<string> CurrentDescriptionProperty =
            AvaloniaProperty.Register<PropertyGrid, string>(nameof(CurrentDescription)
                , defaultValue: string.Empty);



        /// <summary>
        /// Gets or sets the brush for items background.
        /// </summary>
        /// <value>The items background brush.</value>
        public Brush ItemsBackground
        {
            get { return (Brush)GetValue(ItemsBackgroundProperty); }
            set { SetValue(ItemsBackgroundProperty, value); }
        }


        public static readonly StyledProperty<Brush> ItemsBackgroundProperty =
            AvaloniaProperty.Register<PropertyGrid, Brush>(nameof(ItemsBackground));



        /// <summary>
        /// Gets or sets the items foreground brush.
        /// </summary>
        /// <value>The items foreground brush.</value>
        public IBrush ItemsForeground
        {
            get { return (IBrush)GetValue(ItemsForegroundProperty); }
            set { SetValue(ItemsForegroundProperty, value ); }
        }


        public static readonly StyledProperty<IBrush> ItemsForegroundProperty =
            AvaloniaProperty.Register<PropertyGrid, IBrush>(nameof(ItemsForeground));



        /// <summary>
        /// Gets or sets the layout to be used to display properties.
        /// </summary>
        /// <value>The layout to be used to display properties.</value>
        public Control Layout
        {
            get { return (Control)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }


        public static readonly StyledProperty<Control> LayoutProperty =
            AvaloniaProperty.Register<PropertyGrid, Control>(nameof(Layout), defaultValue: default(AlphabeticalLayout));

        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get { return (currentObjects != null && currentObjects.Length != 0) ? currentObjects[0] : null; }
            set { SelectedObjects = (value == null) ? new object[0] : new[] { value }; }
        }

        /// <summary>
        /// Gets or sets the selected objects.
        /// </summary>
        /// <value>The selected objects.</value>
        public object[] SelectedObjects
        {
            get { return (currentObjects == null) ? new object[0] : (object[])currentObjects.Clone(); }
            set
            {
                // Ensure there are no nulls in the array
                VerifySelectedObjects(value);
                
                var sameSelection = false;

                // Check whether new selection is the same as was previously defined
                if (currentObjects != null && value != null && currentObjects.Length == value.Length)
                {
                    sameSelection = true;

                    for (var i = 0; i < value.Length && sameSelection; i++)
                    {
                        if (currentObjects[i] != value[i])
                            sameSelection = false;
                    }
                }

                if (!sameSelection)
                {
                    // Assign new objects and reload
                    if (value == null)
                    {
                        currentObjects = new object[0];
                        DoReload();
                    }
                    else
                    {
                        // process single selection
                        if (value.Length == 1 && currentObjects != null && currentObjects.Length == 1)
                        {
                            var oldValue = (currentObjects != null && currentObjects.Length > 0) ? currentObjects[0] : null;
                            var newValue = (value.Length > 0) ? value[0] : null;

                            currentObjects = (object[])value.Clone();

                            if (oldValue != null && newValue != null && oldValue.GetType().Equals(newValue.GetType()))
                                SwapSelectedObject(newValue);
                            else
                            {
                                DoReload();
                            }
                        }
                        // process multiple selection
                        else
                        {
                            currentObjects = (object[])value.Clone();
                            DoReload();
                        }
                    }

                    OnPropertyChanged("SelectedObjects");
                    OnPropertyChanged("SelectedObject");
                    OnSelectedObjectsChanged();
                }
                else
                {
                    // TODO: Swap multiple objects here? Guess nothing can be done in this case...
                }
            }
        }

        private GridEntryCollection<PropertyItem> _properties;
        /// <summary>
        /// Gets or sets the properties of the selected object(s).
        /// </summary>
        /// <value>The properties of the selected object(s).</value>
        public GridEntryCollection<PropertyItem> Properties
        {
            get { return _properties; }
            private set
            {
                if (_properties == value)
                    return;

                if (_properties != null)
                {
                    foreach (var item in _properties)
                    {
                        UnhookPropertyChanged(item);
                        item.Dispose();
                    }
                }

                if (value != null)
                {
                    _properties = value;

                    if (PropertyComparer != null)
                        _properties.Sort(PropertyComparer);

                    foreach (var item in _properties)
                        HookPropertyChanged(item);
                }

                OnPropertyChanged("Properties");
                OnPropertyChanged("HasProperties");
                OnPropertyChanged("BrowsableProperties");
            }
        }

        /// <summary>
        /// Enumerates the properties that should be visible for user
        /// </summary>
        public IEnumerable<PropertyItem> BrowsableProperties
        {
            get
            {
                if (_properties != null)
                {
                    foreach (var property in _properties)
                        if (property.IsBrowsable)
                            yield return property;
                }
            }
        }

        private IComparer<PropertyItem> _propertyComparer;

        /// <summary>
        /// Gets or sets the default property comparer.
        /// </summary>
        /// <value>The default property comparer.</value>
        public IComparer<PropertyItem> PropertyComparer
        {
            get { return _propertyComparer ?? (_propertyComparer = new PropertyItemComparer()); }
            set
            {
                if (_propertyComparer == value)
                    return;
                _propertyComparer = value;

                if (_properties != null)
                    _properties.Sort(_propertyComparer);

                OnPropertyChanged("PropertyComparer");
            }
        }

        private IComparer<CategoryItem> _categoryComparer;

        /// <summary>
        /// Gets or sets the default category comparer.
        /// </summary>
        /// <value>The default category comparer.</value>
        public IComparer<CategoryItem> CategoryComparer
        {
            get { return _categoryComparer ?? (_categoryComparer = new CategoryItemComparer()); }
            set
            {
                if (_categoryComparer == value)
                    return;
                _categoryComparer = value;

                if (_categories != null)
                    _categories.Sort(_categoryComparer);

                OnPropertyChanged("Categories");
            }
        }

        private GridEntryCollection<CategoryItem> _categories;
        /// <summary>
        /// Gets or sets the categories of the selected object(s).
        /// </summary>
        /// <value>The categories of the selected object(s).</value>
        public GridEntryCollection<CategoryItem> Categories
        {
            get { return _categories; }
            private set
            {
                if (_categories == value)
                    return;
                _categories = value;

                if (CategoryComparer != null)
                    _categories.Sort(CategoryComparer);

                OnPropertyChanged("Categories");
                OnPropertyChanged("HasCategories");
                OnPropertyChanged("BrowsableCategories");
            }
        }

        /// <summary>
        /// Enumerates the categories that should be visible for user.
        /// </summary>
        public IEnumerable<CategoryItem> BrowsableCategories
        {
            get
            {
                if (_categories != null)
                {
                    foreach (var category in _categories)
                        if (category.IsBrowsable)
                            yield return category;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether read-only properties should be displayed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if read-only properties should be displayed; otherwise, 
        /// 	<c>false</c>. Default is <c>false</c>.
        /// </value>
        public bool ShowReadOnlyProperties
        {
            get { return (bool)GetValue(ShowReadOnlyPropertiesProperty); }
            set { SetValue(ShowReadOnlyPropertiesProperty, value); }
        }


        public static readonly StyledProperty<bool> ShowReadOnlyPropertiesProperty =
            AvaloniaProperty.Register<PropertyGrid, bool>(nameof(ShowReadOnlyProperties));


        /// <summary>
        /// Gets or sets a value indicating whether attached properties should be displayed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if attached properties should be displayed; otherwise, <c>false</c>. Default is <c>false</c>.
        /// </value>
        public bool ShowAttachedProperties
        {
            get { return (bool)GetValue(ShowAttachedPropertiesProperty); }
            set { SetValue(ShowAttachedPropertiesProperty, value); }
        }


        public static readonly StyledProperty<bool> ShowAttachedPropertiesProperty =
            AvaloniaProperty.Register<PropertyGrid, bool>(nameof(ShowAttachedProperties));


        /// <summary>
        /// Gets or sets the property filter. 
        /// </summary>
        /// <value>The property filter.</value>
        public string PropertyFilter
        {
            get { return (string)GetValue(PropertyFilterProperty); }
            set { SetValue(PropertyFilterProperty, value); }
        }


        public static readonly StyledProperty<string> PropertyFilterProperty =
            AvaloniaProperty.Register<PropertyGrid, string>(nameof(PropertyFilter)
                , defaultValue: string.Empty
                , defaultBindingMode: Data.BindingMode.TwoWay
                );


        /// <summary>
        /// Gets or sets the property filter visibility state.
        /// </summary>
        /// <value>The property filter visibility state.</value>
        public bool PropertyFilterIsVisible
        {
            get { return (bool)GetValue(PropertyFilterIsVisibleProperty); }
            set { SetValue(PropertyFilterIsVisibleProperty, value); }
        }


        public static readonly StyledProperty<bool> PropertyFilterIsVisibleProperty =
            AvaloniaProperty.Register<PropertyGrid, bool>(nameof(PropertyFilterIsVisible)
                , defaultValue: true);


        /// <summary>
        /// Gets or sets the property display mode.
        /// </summary>
        /// <value>The property display mode.</value>
        public PropertyDisplayMode PropertyDisplayMode
        {
            get { return (PropertyDisplayMode)GetValue(PropertyDisplayModeProperty); }
            set { SetValue(PropertyDisplayModeProperty, value); }
        }


        public static readonly StyledProperty<PropertyDisplayMode> PropertyDisplayModeProperty =
            AvaloniaProperty.Register<PropertyGrid, PropertyDisplayMode>(nameof(PropertyDisplayMode)
                , defaultValue: PropertyDisplayMode.All);







        /// <summary>
        /// Occurs when selected objects are changed.
        /// </summary>
        public event EventHandler SelectedObjectsChanged;

        /// <summary>
        /// Called when selected objects were changed.
        /// </summary>
        protected virtual void OnSelectedObjectsChanged()
        {
            var handler = SelectedObjectsChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }


        public static readonly RoutedEvent<PropertyEditingEventArgs> PropertyEditingStartedEvent =
                    RoutedEvent.Register<PropertyGrid, PropertyEditingEventArgs>
            (nameof(PropertyEditingStartedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property editing is started.
        /// </summary>
        /// <remarks>
        /// This event is intended to be used in customization scenarios. 
        /// It is not used by PropertyGrid control directly.
        /// </remarks>
        public event PropertyEditingEventHandler PropertyEditingStarted
        {
            add
            {
                AddHandler(PropertyEditingStartedEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyEditingStartedEvent, value);
            }
        }



        public static readonly RoutedEvent<PropertyEditingEventArgs> PropertyEditingFinishedEvent =
                    RoutedEvent.Register<PropertyGrid, PropertyEditingEventArgs>
            (nameof(PropertyEditingFinishedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property editing is finished.
        /// </summary>
        /// <remarks>
        /// This event is intended to be used in customization scenarios. 
        /// It is not used by PropertyGrid control directly.
        /// </remarks>
        public event PropertyEditingEventHandler PropertyEditingFinished
        {
            add
            {
                AddHandler(PropertyEditingFinishedEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyEditingFinishedEvent, value);
            }
        }



        public static readonly RoutedEvent<RoutedEventArgs> PropertyValueChangedEvent =
                    RoutedEvent.Register<PropertyGrid, RoutedEventArgs>(nameof(PropertyValueChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when property item value is changed.
        /// </summary>
        public event EventHandler PropertyValueChanged
        {
            add
            {
                AddHandler(PropertyValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyValueChangedEvent, value);
            }
        }

        private void RaisePropertyValueChangedEvent(PropertyItem property, object oldValue)
        {
            var args = new PropertyValueChangedEventArgs(PropertyValueChangedEvent, property, oldValue);
            RaiseEvent(args);
        }





        static PropertyGrid()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(ThisType, new FrameworkPropertyMetadata(ThisType));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGrid"/> class.
        /// </summary>
        public PropertyGrid()
        {
            this.GotFocus += (o, e) =>
            {
                ShowDescription(o, e);
            };

            //EventManager.RegisterClassHandler(typeof(PropertyGrid), GotFocusEvent, new RoutedEventHandler(ShowDescription), true);

            // Assign Layout to be Alphabetical by default
            Layout = new AlphabeticalLayout();

            // Wire command bindings


            //InitializeCommandBindings();
            CurrentDescriptionProperty.Changed.AddClassHandler<PropertyGrid>((o, e) => OnCurrentDescriptionChanged(o, e));
            LayoutProperty.Changed.AddClassHandler<PropertyGrid>((o, e) => OnLayoutChanged(o, e));
            ShowReadOnlyPropertiesProperty.Changed.AddClassHandler<PropertyGrid>((o, e) => OnShowReadOnlyPropertiesChanged(o, e));
            ShowAttachedPropertiesProperty.Changed.AddClassHandler<PropertyGrid>((o, e) => OnShowAttachedPropertiesChanged(o, e));
            PropertyFilterProperty.Changed.AddClassHandler<PropertyGrid>((o, e) => OnPropertyFilterChanged(o, e));
            PropertyDisplayModeProperty.Changed.AddClassHandler<PropertyGrid>((o, e) => OnPropertyDisplayModePropertyChanged(o, e));
        }

        internal CategoryItem CreateCategory(CategoryAttribute attribute)
        {
            // Check the attribute argument to be passed
            Debug.Assert(attribute != null);
            if (attribute == null)
                return null;

            // Check browsable restrictions
            //if (!ShouldDisplayCategory(attribute.Category)) return null;

            // Create a new CategoryItem
            var categoryItem = new CategoryItem(this, attribute);
            categoryItem.IsBrowsable = ShouldDisplayCategory(categoryItem.Name);

            // Return resulting item
            return categoryItem;
        }

        private PropertyItem CreatePropertyItem(PropertyDescriptor descriptor)
        {
            // Check browsable restrictions
            //if (!ShoudDisplayProperty(descriptor)) return null;
#warning todo
            var dpDescriptor = null as PropertyDescriptor; //DependencyPropertyDescriptor.FromProperty(descriptor);
            // Provide additional checks for dependency properties
            if (dpDescriptor != null)
            {
                // Check whether dependency properties are not prohibited
                if (PropertyDisplayMode == PropertyDisplayMode.Native)
                    return null;

                // Check whether attached properties are to be displayed

                //if (dpDescriptor.IsAttached && !ShowAttachedProperties)
                //    return null;
            }
            else
            {
                if (PropertyDisplayMode == PropertyDisplayMode.Dependency)
                    return null;
            }

            // Check whether readonly properties are to be displayed
            if (descriptor.IsReadOnly && !ShowReadOnlyProperties)
                return null;

            // Note: superceded by ShouldDisplayProperty method call
            // Check whether property is browsable and add it to the collection
            // if (!descriptor.IsBrowsable) return null;

            //PropertyItem item = new PropertyItem(this, this.SelectedObject, descriptor);      

            var item = (currentObjects.Length > 1)
              ? new PropertyItem(this, currentObjects, descriptor)
              : new PropertyItem(this, SelectedObject, descriptor);

            //item.OverrideIsBrowsable(new bool?(ShoudDisplayProperty(descriptor)));
            item.IsBrowsable = ShoudDisplayProperty(descriptor);

            return item;
        }

        private bool ShoudDisplayProperty(PropertyDescriptor propertyDescriptor)
        {
            Debug.Assert(propertyDescriptor != null);
            if (propertyDescriptor == null)
                return false;

            // Check whether owning category is not restricted to ouput
            var showWithinCategory = ShouldDisplayCategory(propertyDescriptor.Category);
            if (!showWithinCategory)
                return false;

            // Check the explicit declaration
            var attribute = browsableProperties.FirstOrDefault(item => item.PropertyName == propertyDescriptor.Name);
            if (attribute != null)
                return attribute.Browsable;

            // Check the wildcard
            var wildcard = browsableProperties.FirstOrDefault(item => item.PropertyName == BrowsablePropertyAttribute.All);
            if (wildcard != null)
                return wildcard.Browsable;

            // Return default/standard Browsable settings for the property
            return propertyDescriptor.IsBrowsable;
        }

        private bool ShouldDisplayCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return false;

            // Check the explicit declaration
            var attribute = browsableCategories.FirstOrDefault(item => item.CategoryName == categoryName);
            if (attribute != null)
                return attribute.Browsable;

            // Check the wildcard
            var wildcard = browsableCategories.FirstOrDefault(item => item.CategoryName == BrowsableCategoryAttribute.All);
            if (wildcard != null)
                return wildcard.Browsable;

            // Allow by default if no restrictions were applied
            return true;
        }

        /// <summary>
        /// Gets the editor for a grid entry.
        /// </summary>
        /// <param name="entry">The entry to look the editor for.</param>
        /// <returns>Editor for the entry</returns>
        public virtual Editor GetEditor(GridEntry entry)
        {
            var property = entry as PropertyItem;
            if (property != null)
                return Editors.GetEditor(property);

            var category = entry as CategoryItem;
            if (category != null)
                return Editors.GetEditor(category);

            return null;
        }

        private void SwapSelectedObject(object value)
        {
            //foreach (PropertyItem property in this.Properties)
            //{
            //  property.SetPropertySouce(value);
            //}
            DoReload();
        }

        private IEnumerable<CategoryItem> CollectCategories(IEnumerable<PropertyItem> properties)
        {
            var categories = new Dictionary<string, CategoryItem>();
            var refusedCategories = new HashSet<string>();

            foreach (var property in properties)
            {
                if (refusedCategories.Contains(property.CategoryName))
                    continue;
                CategoryItem category;

                if (categories.ContainsKey(property.CategoryName))
                    category = categories[property.CategoryName];
                else
                {
                    category = CreateCategory(property.GetAttribute<CategoryAttribute>());

                    if (category == null)
                    {
                        refusedCategories.Add(property.CategoryName);
                        continue;
                    }

                    categories[category.Name] = category;
                }

                category.AddProperty(property);
            }

            return categories.Values.ToList();
        }

        private IEnumerable<PropertyItem> CollectProperties(object[] components)
        {
            if (components == null || components.Length == 0)
                throw new ArgumentNullException("components");

            // This is an obsolete code left for performance improvements demo. Will be removed in the future versions.
            /*
            var descriptors = component.Length == 1
              ? TypeDescriptor.GetProperties(component[0], DefaultPropertiesFilter).OfType<PropertyDescriptor>()
              : ObjectServices.GetMergedProperties(component);
            */

            // TODO: PropertyItem is to be wired with PropertyData rather than pure PropertyDescriptor in the next version!
            var descriptors = (components.Length == 1)
              ? MetadataRepository.GetProperties(components[0]).Select(prop => prop.Descriptor)
              : ObjectServices.GetMergedProperties(components);

            IList<PropertyItem> propertyCollection = new List<PropertyItem>();

            foreach (var propertyDescriptor in descriptors)
            // This is an obsolete code left for performance improvements demo. Will be removed in the future versions.
            //CollectProperties(component, propertyDescriptor, propertyCollection);
            {
                var item = CreatePropertyItem(propertyDescriptor);
                if (item != null)
                    propertyCollection.Add(item);
            }

            return propertyCollection;
        }

        // This is an obsolete code left for performance improvements demo. Will be removed in the future versions.
        /*
        private void CollectProperties(object component, PropertyDescriptor descriptor, IList<PropertyItem> propertyList)
        {
          if (descriptor.Attributes[typeof(FlatternHierarchyAttribute)] == null)
          {
            PropertyItem item = CreatePropertyItem(descriptor);
            if (item != null)
              propertyList.Add(item);
          }
          else
          {
            component = descriptor.GetValue(component);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
            foreach (PropertyDescriptor propertyDescriptor in properties)
              CollectProperties(component, propertyDescriptor, propertyList);
          }
        }
        */

        private static void VerifySelectedObjects(object[] value)
        {
            if (value != null && value.Length > 0)
            {
                // Ensure there are no nulls in the array
                for (var i = 0; i < value.Length; i++)
                {
                    if (value[i] == null)
                    {
                        var args = new object[] { i.ToString(CultureInfo.CurrentCulture), value.Length.ToString(CultureInfo.CurrentCulture) };
                        // TODO: Move exception format to resources/settings!
                        throw new ArgumentNullException(string.Format("Item {0} in the 'objs' array is null. The array must begin with at least {1} members.", args));
                    }
                }
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab && e.Source is AvaloniaObject)//tabbing over the property editors
            {
                var source = e.Source as IVisual;
                var element = e.KeyModifiers == KeyModifiers.Shift ? GetTabElement(source, -1) : GetTabElement(source, 1);
                if (element != null)
                {
                    element.Focus();
                    e.Handled = true;
                    return;
                }
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Gets the tab element on which the focus can be placed.
        /// </summary>
        /// <remarks>
        /// If an element is not enabled it will not be returned.
        /// </remarks>
        /// <param name="source">The source.</param>
        /// <param name="delta">The delta.</param>
        private IControl GetTabElement(IVisual source, int delta)
        {
            if (source == null)
                return null;
            PropertyContainer container = null;
            if (source is SearchTextBox && HasCategories)
            {
                var itemspres = FindVisualChild<ItemsPresenter>(this);
                if (itemspres != null)
                {
                    var catcontainer = FindVisualChild<CategoryContainer>(itemspres);
                    if (catcontainer != null)
                    {
                        container = FindVisualChild<PropertyContainer>(catcontainer);
                    }
                }
            }
            else
                container = FindVisualParent<PropertyContainer>(source);

            var spanel = FindVisualParent<StackPanel>(container);
            if (spanel != null && spanel.Children.Contains(container))
            {
                var index = spanel.Children.IndexOf(container);
                if (delta > 0)
                    index = (index == spanel.Children.Count - 1) ? 0 : index + delta;//go back to the first after last
                else
                    index = (index == 0) ? spanel.Children.Count - 1 : index + delta;//go to last after first
                                                                                     //loop inside the list
                if (index < 0)
                    index = spanel.Children.Count - 1;
                if (index >= spanel.Children.Count)
                    index = 0;


                var next = VisualTree.VisualExtensions.GetVisualChildren(spanel).ElementAt(index) as PropertyContainer;//  VisualTreeHelper.GetChild(spanel, index) as PropertyContainer;//this has always a Grid as visual child

                var grid = FindVisualChild<Grid>(next);
                if (grid != null && grid.Children.Count > 1)
                {
                    var pecp = grid.Children[1] as PropertyEditorContentPresenter;
                    var final = VisualTree.VisualExtensions.GetVisualChildren(pecp).ElementAt(0);//VisualTreeHelper.GetChild(pecp, 0);
                    if ((final as Control).IsEnabled && (final as Control).Focusable && !(next.DataContext as PropertyItem).IsReadOnly)
                        return final as Control;
                    else
                        return GetTabElement(final, delta);

                }

            }
            return null;
        }

        private static T FindVisualParent<T>(IVisual element) where T : class
        {
            if (element == null)
                return default(T);
            object parent = VisualTree.VisualExtensions.GetVisualParent(element);
            if (parent is T)
                return parent as T;
            if (parent is IVisual)
                return FindVisualParent<T>(parent as IVisual);
            return null;
        }
        private static T FindVisualChild<T>(IVisual element) where T : class
        {
            if (element == null)
                return default(T);
            if (element is T)
                return element as T;

            var children = VisualTree.VisualExtensions.GetVisualChildren(element);

            if (children.Any())
            {
                for (var i = 0; i < children.Count(); i++)
                {

                    //object child = VisualTreeHelper.GetChild(element, i);
                    object child = children.ElementAt(i);

                    if (child is SearchTextBox)
                        continue;//speeds up things a bit
                    if (child is T)
                        return child as T;
                    if (child is IVisual)
                    {
                        var res = FindVisualChild<T>(child as IVisual);
                        if (res == null)
                            continue;
                        return res;
                    }
                }
            }
            return null;
        }


        private void OnPropertyDisplayModePropertyChanged(PropertyGrid propertyGrid, AvaloniaPropertyChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject == null)
                return;
            propertyGrid.DoReload();
        }

        private void OnPropertyFilterChanged(PropertyGrid propertyGrid, AvaloniaPropertyChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject == null || !propertyGrid.HasCategories)
                return;

            foreach (var category in propertyGrid.Categories)
                category.ApplyFilter(new PropertyFilter(propertyGrid.PropertyFilter));
        }

        private void OnShowAttachedPropertiesChanged(PropertyGrid propertyGrid, AvaloniaPropertyChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject == null)
                return;
            if (propertyGrid.HasCategories | propertyGrid.HasProperties)
                propertyGrid.DoReload();
        }

        private void OnShowReadOnlyPropertiesChanged(PropertyGrid propertyGrid, AvaloniaPropertyChangedEventArgs e)
        {
            // Check whether any object was selected
            if (propertyGrid.SelectedObject == null)
                return;

            // Check whether categories or properties were created
            if (propertyGrid.HasCategories | propertyGrid.HasProperties)
                propertyGrid.DoReload();
        }

        private void OnLayoutChanged(PropertyGrid propertyGrid, AvaloniaPropertyChangedEventArgs e)
        {
            var layoutContainer = e.NewValue as Control;
            if (layoutContainer != null)
                layoutContainer.DataContext = propertyGrid;
        }

        private void OnCurrentDescriptionChanged(PropertyGrid o, AvaloniaPropertyChangedEventArgs e)
        {
            o.OnCurrentDescriptionChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the CurrentDescription property.
        /// </summary>
        protected virtual void OnCurrentDescriptionChanged(AvaloniaPropertyChangedEventArgs e)
        {
        }

        private void ShowDescription(object sender, RoutedEventArgs e)
        {
            if (e.Source == null || !(e.Source is StyledElement) ||
                (e.Source as StyledElement).DataContext == null ||
                !((e.Source as StyledElement).DataContext is PropertyItemValue) ||
                ((e.Source as StyledElement).DataContext as PropertyItemValue).ParentProperty == null)
                return;
            var descri = ((e.Source as StyledElement).DataContext as PropertyItemValue).ParentProperty.ToolTip;
            CurrentDescription = descri == null ? "" : descri.ToString();
        }






        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when property value changes.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }












        private void DoReload()
        {
            if (SelectedObject == null)
            {
                Categories = new GridEntryCollection<CategoryItem>();
                Properties = new GridEntryCollection<PropertyItem>();
            }
            else
            {
                // Collect BrowsableCategoryAttribute items
                var categoryAttributes = PropertyGridUtils.GetAttributes<BrowsableCategoryAttribute>(SelectedObject);
                browsableCategories = new List<BrowsableCategoryAttribute>(categoryAttributes);

                // Collect BrowsablePropertyAttribute items
                var propertyAttributes = PropertyGridUtils.GetAttributes<BrowsablePropertyAttribute>(SelectedObject);
                browsableProperties = new List<BrowsablePropertyAttribute>(propertyAttributes);

                // Collect categories and properties
                var properties = CollectProperties(currentObjects);

                // TODO: This needs more elegant implementation
                var categories = new GridEntryCollection<CategoryItem>(CollectCategories(properties));
                if (_categories != null && _categories.Count > 0)
                    CopyCategoryFrom(_categories, categories);

                // Fetch and apply category orders
                var categoryOrders = PropertyGridUtils.GetAttributes<CategoryOrderAttribute>(SelectedObject);
                foreach (var orderAttribute in categoryOrders)
                {
                    var category = categories[orderAttribute.Category];
                    // don't apply Order if it was applied before (Order equals zero or more), 
                    // so the first discovered Order value for the same category wins
                    if (category != null && category.Order < 0)
                        category.Order = orderAttribute.Order;
                }

                Categories = categories; //new CategoryCollection(CollectCategories(properties));
                Properties = new GridEntryCollection<PropertyItem>(properties);

            }
        }

        private static void CopyCategoryFrom(GridEntryCollection<CategoryItem> oldValue, IEnumerable<CategoryItem> newValue)
        {
            foreach (var category in newValue)
            {
                var prev = oldValue[category.Name];
                if (prev == null)
                    continue;

                category.IsExpanded = prev.IsExpanded;
            }
        }

        private void OnPropertyItemValueChanged(PropertyItem property, object oldValue, object newValue)
        {
            RaisePropertyValueChangedEvent(property, oldValue);
        }

        private void HookPropertyChanged(PropertyItem item)
        {
            if (item == null)
                return;
            item.ValueChanged += OnPropertyItemValueChanged;
        }

        private void UnhookPropertyChanged(PropertyItem item)
        {
            if (item == null)
                return;
            item.ValueChanged -= OnPropertyItemValueChanged;
        }

    }
}
