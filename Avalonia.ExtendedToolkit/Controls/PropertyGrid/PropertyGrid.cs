using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils;
using Avalonia.Input;
using Avalonia.Interactivity;

//
// ported from https://github.com/DenisVuyka/WPG
//

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// property grid control
    /// </summary>
    public partial class PropertyGrid : TemplatedControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGrid"/> class.
        /// </summary>
        public PropertyGrid()
        {
            this.GotFocus += (o, e) =>
            {
                ShowDescription(o, e);
            };

            // Assign Layout to be Alphabetical by default
            Layout = new AlphabeticalLayout();

            // Wire command bindings
            InitializeCommandBindings();

            CurrentDescriptionProperty.Changed.AddClassHandler<PropertyGrid>((Action<PropertyGrid, AvaloniaPropertyChangedEventArgs>)((o,     e) => OnCurrentDescriptionChanged(o, e)));
            LayoutProperty.Changed.AddClassHandler<PropertyGrid>((Action<PropertyGrid, AvaloniaPropertyChangedEventArgs>)((o,                 e) => OnLayoutChanged(o, e)));
            ShowReadOnlyPropertiesProperty.Changed.AddClassHandler<PropertyGrid>((Action<PropertyGrid, AvaloniaPropertyChangedEventArgs>)((o, e) => OnShowReadOnlyPropertiesChanged(o, e)));
            ShowAttachedPropertiesProperty.Changed.AddClassHandler<PropertyGrid>((Action<PropertyGrid, AvaloniaPropertyChangedEventArgs>)((o, e) => OnShowAttachedPropertiesChanged(o, e)));
            PropertyFilterProperty.Changed.AddClassHandler<PropertyGrid>((Action<PropertyGrid, AvaloniaPropertyChangedEventArgs>)((o,         e) => OnPropertyFilterChanged(o, e)));
            PropertyDisplayModeProperty.Changed.AddClassHandler<PropertyGrid>((Action<PropertyGrid, AvaloniaPropertyChangedEventArgs>)((o,    e) => OnPropertyDisplayModePropertyChanged(o, e)));
        }

        private void RaisePropertyValueChangedEvent(PropertyItem property, object oldValue)
        {
            
            var args = new PropertyValueChangedEventArgs(PropertyValueChangedEvent, property, oldValue);
            RaiseEvent(args);
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
            categoryItem.Editor = GetEditor(categoryItem);
            categoryItem.IsBrowsable = ShouldDisplayCategory(categoryItem.Name);

            // Return resulting item
            return categoryItem;
        }

        private PropertyItem CreatePropertyItem(PropertyDescriptor descriptor)
        {
            // Check browsable restrictions
            //if (!ShoudDisplayProperty(descriptor)) return null;

            //DependencyPropertyDescriptor.FromProperty(descriptor);
            var dpDescriptor = TypeDescriptor.GetProperties(this.SelectedObject).OfType<PropertyDescriptor>().
                FirstOrDefault(x => x.Name == descriptor.Name && x.PropertyType == descriptor.PropertyType);

            //try again with SelectedObjects
            if(descriptor==null)
            {
                dpDescriptor = TypeDescriptor.GetProperties(this.SelectedObjects).OfType<PropertyDescriptor>().
                FirstOrDefault(x => x.Name == descriptor.Name && x.PropertyType == descriptor.PropertyType);
            }

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

            var item = (SelectedObjects.Length > 1)
              ? new PropertyItem(this, SelectedObjects, descriptor)
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
                {
                    category = categories[property.CategoryName];

                    if (category.Name.Contains("Account"))
                    {

                    }

                }
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
                throw new ArgumentNullException(nameof(components));

            // TODO: PropertyItem is to be wired with PropertyData rather than pure PropertyDescriptor in the next version!
            var descriptors = (components.Length == 1)
              ? MetadataRepository.GetProperties(components[0]).Select(prop => prop.Descriptor)
              : ObjectServices.GetMergedProperties(components);

            IList<PropertyItem> propertyCollection = new List<PropertyItem>();

            foreach (var propertyDescriptor in descriptors)
            {
                var item = CreatePropertyItem(propertyDescriptor);
                if (item != null)
                    propertyCollection.Add(item);
            }

            return propertyCollection;
        }

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

        /// <summary>
        /// tabbing over property editors
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab && e.Source is AvaloniaObject)//tabbing over the property editors
            {
                var source = e.Source as Control;
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
        private Control GetTabElement(Control source, int delta)
        {
            if (source == null)
                return null;
            PropertyContainer container = null;
            if (source is SearchTextBox && HasCategories)
            {
                var itemspres = this.FindVisualChild<ItemsPresenter>();
                if (itemspres != null)
                {
                    //var children = itemspres.FindVisualChildren<Control>().Where(x => x is CategoryContainer);
                    //List<PropertyContainer> propContainers = new List<PropertyContainer>();
                    //foreach (var child in children)
                    //{
                    //    var test = child.FindVisualChildren<Control>();

                    //    var propcontainer = child.FindVisualChild<PropertyContainer>();
                    //    if (propcontainer != null)
                    //    {
                    //        propContainers.Add(propcontainer);
                    //    }

                    //}

                    var catcontainer = itemspres.FindVisualChild<CategoryContainer>();

                    if (catcontainer != null)
                    {
                        container = catcontainer.FindVisualChild<PropertyContainer>();
                    }
                }
            }
            else
                container = source.FindVisualParent<PropertyContainer>();

            var spanel = container.FindVisualParent<StackPanel>();
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

                var grid = next.FindVisualChild<Grid>();
                if (grid != null && grid.Children.Count > 1)
                {
                    var pecp = grid.Children[1] as PropertyEditorContentPresenter;
                    var final = VisualTree.VisualExtensions.GetVisualChildren(pecp).ElementAt(0);
                    //VisualTreeHelper.GetChild(pecp, 0);
                    if ((final as Control).IsEnabled && (final as Control).Focusable && !(next.DataContext as PropertyItem).IsReadOnly)
                        return final as Control;
                    else
                        return GetTabElement(final as Control, delta);
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
            var layoutContainer = e.NewValue as TemplatedControl;
            if (layoutContainer != null)
            {
                layoutContainer.DataContext = propertyGrid;
            }
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
        /// reloads the property grid
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            this.InvalidateMeasure();
            this.InvalidateArrange();
            this.InvalidateVisual();
            //.orig, think many
            //RaisePropertyChanged(LayoutProperty, new Data.Optional<Control>()
            //            , new Data.BindingValue<Control>(Layout));
            DoReload();
        }

        
        internal void DoReload()
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
                var properties = CollectProperties(SelectedObjects);

                // TODO: This needs more elegant implementation
                var categories = new GridEntryCollection<CategoryItem>(CollectCategories(properties));
                if (Categories != null && Categories.Count > 0)
                    CopyCategoryFrom(Categories, categories);

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

        /// <summary>
        /// Called when selected objects were changed.
        /// </summary>
        protected virtual void OnSelectedObjectsChanged()
        {
            var handler = SelectedObjectsChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
