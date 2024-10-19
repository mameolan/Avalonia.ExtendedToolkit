using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    public partial class PropertyGrid
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(PropertyGrid);

        /// <summary>
        /// default properties filter
        /// </summary>
        private static Attribute[] DefaultPropertiesFilter = new Attribute[]
        {
            new PropertyFilterAttribute(
            PropertyFilterOptions.SetValues
            | PropertyFilterOptions.UnsetValues
            | PropertyFilterOptions.Valid)
        };

        private List<BrowsablePropertyAttribute> browsableProperties = new List<BrowsablePropertyAttribute>();
        private List<BrowsableCategoryAttribute> browsableCategories = new List<BrowsableCategoryAttribute>();



        /// <summary>
        /// Gets the editors collection.
        /// </summary>
        /// <value>The editors collection.</value>
        public EditorCollection Editors
        {
            get;
        } = new EditorCollection();

        /// <summary>
        /// <see cref="HasProperties"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, bool> HasPropertiesProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, bool>(
                    nameof(HasProperties),
                    o => o.HasProperties);

        /// <summary>
        /// Gets a value indicating whether this instance has properties.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has properties; otherwise, <c>false</c>.
        /// </value>
        public bool HasProperties
        {
            get { return _properties != null && _properties.Count > 0; }
        }

        /// <summary>
        /// <see cref="HasCategories"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, bool> HasCategoriesProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, bool>(
                    nameof(HasCategories),
                    o => o.HasCategories);

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

        /// <summary>
        /// get/set CurrentDescription
        /// </summary>
        public string CurrentDescription
        {
            get { return (string)GetValue(CurrentDescriptionProperty); }
            set { SetValue(CurrentDescriptionProperty, value); }
        }

        /// <summary>
        /// <see cref="CurrentDescription"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ItemsBackground"/>
        /// </summary>
        public static readonly StyledProperty<Brush> ItemsBackgroundProperty =
            AvaloniaProperty.Register<PropertyGrid, Brush>(nameof(ItemsBackground));

        /// <summary>
        /// Gets or sets the items foreground brush.
        /// </summary>
        /// <value>The items foreground brush.</value>
        public IBrush ItemsForeground
        {
            get { return (IBrush)GetValue(ItemsForegroundProperty); }
            set { SetValue(ItemsForegroundProperty, value); }
        }

        /// <summary>
        /// <see cref="ItemsForeground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> ItemsForegroundProperty =
            AvaloniaProperty.Register<PropertyGrid, IBrush>(nameof(ItemsForeground));

        /// <summary>
        /// Gets or sets the layout to be used to display properties.
        /// </summary>
        /// <value>The layout to be used to display properties.</value>
        //[Content]
        public Control Layout
        {
            get { return (Control)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        /// <summary>
        /// <see cref="Layout"/>
        /// </summary>
        public static readonly StyledProperty<Control> LayoutProperty =
            AvaloniaProperty.Register<PropertyGrid, Control>(nameof(Layout), defaultValue: default(AlphabeticalLayout));

        /// <summary>
        /// <see cref="SelectedObject"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, object> SelectedObjectProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, object>(
                    nameof(SelectedObject),
                    o => o.SelectedObject);

        private object _selectedObject;

        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get { return (_currentObjects != null && _currentObjects.Length != 0) ? _currentObjects[0] : null; }
            set
            {
                SetAndRaise(SelectedObjectProperty, ref _selectedObject, value);
                SelectedObjects = (value == null) ? new object[0] : new[] { value };
            }
        }

        /// <summary>
        /// <see cref="SelectedObjects"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, object[]> SelectedObjectsProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, object[]>(
                    nameof(SelectedObjects),
                    o => o.SelectedObjects);

        private object[] _currentObjects;

        /// <summary>
        /// Gets or sets the selected objects.
        /// </summary>
        /// <value>The selected objects.</value>
        public object[] SelectedObjects
        {
            get { return (_currentObjects == null) ? new object[0] : (object[])_currentObjects.Clone(); }
            set
            {
                object[] currentObjects = _currentObjects;
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

                    RaisePropertyChanged(SelectedObjectProperty, null, SelectedObject);

                    SetAndRaise(SelectedObjectsProperty, ref _currentObjects, currentObjects);

                    OnSelectedObjectsChanged();
                }
                else
                {
                    // TODO: Swap multiple objects here? Guess nothing can be done in this case...
                }
            }
        }

        /// <summary>
        /// <see cref="Properties"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, GridEntryCollection<PropertyItem>> PropertiesProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, GridEntryCollection<PropertyItem>>(
                    nameof(Properties),
                    o => o.Properties);

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

                SetAndRaise(PropertiesProperty, ref _properties, value);

                if (_properties != null)
                {
                    foreach (var item in _properties)
                    {
                        UnhookPropertyChanged(item);
                        //item.Dispose();
                    }
                }

                if (value != null)
                {
                    SetAndRaise(PropertiesProperty, ref _properties, value);

                    if (PropertyComparer != null)
                        _properties.Sort(PropertyComparer);

                    foreach (var item in _properties)
                        HookPropertyChanged(item);
                }
                //.orig, think many
                //OnPropertyChanged("Properties");

                //RaisePropertyChanged(HasPropertiesProperty, !HasProperties, HasProperties);
                //OnPropertyChanged("HasProperties");

                //RaisePropertyChanged(BrowsablePropertiesProperty, new Data.Optional<IEnumerable<PropertyItem>>(), new Data.BindingValue<IEnumerable<PropertyItem>>(BrowsableProperties));
                //OnPropertyChanged("BrowsableProperties");
            }
        }

        /// <summary>
        /// <see cref="BrowsableProperties"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, IEnumerable<PropertyItem>> BrowsablePropertiesProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, IEnumerable<PropertyItem>>(
                    nameof(BrowsableProperties),
                    o => o.BrowsableProperties);

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

        /// <summary>
        /// <see cref="PropertyComparer"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, IComparer<PropertyItem>> PropertyComparerProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, IComparer<PropertyItem>>(
                    nameof(PropertyComparer),
                    o => o.PropertyComparer);

        private IComparer<PropertyItem> _propertyComparer;

        /// <summary>
        /// Gets or sets the default property comparer.
        /// </summary>
        /// <value>The default property comparer.</value>
        public IComparer<PropertyItem> PropertyComparer
        {
            get { return _propertyComparer ?? (_propertyComparer = new PropertyItemComparer()); }
            private set
            {
                if (_propertyComparer == value)
                    return;

                SetAndRaise(PropertyComparerProperty, ref _propertyComparer, value);

                if (_properties != null)
                    _properties.Sort(_propertyComparer);
            }
        }

        /// <summary>
        /// <see cref="CategoryComparer"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, IComparer<CategoryItem>> CategoryComparerProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, IComparer<CategoryItem>>(
                    nameof(CategoryComparer),
                    o => o.CategoryComparer);

        private IComparer<CategoryItem> _categoryComparer;

        /// <summary>
        /// Gets or sets the default category comparer.
        /// </summary>
        /// <value>The default category comparer.</value>
        public IComparer<CategoryItem> CategoryComparer
        {
            get { return _categoryComparer ?? (_categoryComparer = new CategoryItemComparer()); }
            private set
            {
                if (_categoryComparer == value)
                    return;

                SetAndRaise(CategoryComparerProperty, ref _categoryComparer, value);

                if (_categories != null)
                    _categories.Sort(_categoryComparer);
            }
        }

        /// <summary>
        /// <see cref="Categories"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, GridEntryCollection<CategoryItem>> CategoriesProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, GridEntryCollection<CategoryItem>>(
                    nameof(Categories),
                    o => o.Categories);

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

                SetAndRaise(CategoriesProperty, ref _categories, value);

                if (CategoryComparer != null)
                    _categories.Sort(CategoryComparer);

                RaisePropertyChanged(HasCategoriesProperty, !HasCategories, HasCategories);
                //.orig, think many
                //RaisePropertyChanged(BrowsablePropertiesProperty, 
                //new Data.Optional<IEnumerable<PropertyItem>>(), 
                //            new Data.BindingValue<IEnumerable<PropertyItem>>(BrowsableProperties));
                //        OnPropertyChanged("BrowsableCategories");
            }
        }

        /// <summary>
        /// <see cref="BrowsableCategories"/>
        /// </summary>
        public static readonly DirectProperty<PropertyGrid, IEnumerable<CategoryItem>> BrowsableCategoriesProperty =
                AvaloniaProperty.RegisterDirect<PropertyGrid, IEnumerable<CategoryItem>>(
                    nameof(BrowsableCategories),
                    o => o.BrowsableCategories);

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

        /// <summary>
        /// <see cref="ShowReadOnlyProperties"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="ShowAttachedProperties"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="PropertyFilter"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="PropertyFilterIsVisible"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="PropertyDisplayMode"/>
        /// </summary>
        public static readonly StyledProperty<PropertyDisplayMode> PropertyDisplayModeProperty =
            AvaloniaProperty.Register<PropertyGrid, PropertyDisplayMode>(nameof(PropertyDisplayMode)
                , defaultValue: PropertyDisplayMode.All);

        /// <summary>
        /// Occurs when selected objects are changed.
        /// </summary>
        public event EventHandler SelectedObjectsChanged;

        /// <summary>
        /// <see cref="PropertyEditingStarted"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="PropertyEditingFinished"/>
        /// </summary>
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

        /// <summary>
        /// <see cref="PropertyValueChanged"/>
        /// </summary>
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
    }
}
