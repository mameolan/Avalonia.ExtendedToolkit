using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Special grid entry that provides information about property
    /// category and gives access to underlying properties.
    /// </summary>
    public class CategoryItem : GridEntry
    {
        /// <summary>
        /// Gets or sets the attribute the category was created with.
        /// </summary>
        /// <value>The attribute.</value>
        public Attribute Attribute { get; set; }

        public static readonly DirectProperty<CategoryItem, int> OrderProperty =
                AvaloniaProperty.RegisterDirect<CategoryItem, int>(
                    nameof(Order),
                    o => o.Order, unsetValue: -1);

        private int _order = -1;

        /// <summary>
        /// Gets or sets the order of the category.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                //if (_order == value)
                //    return;
                SetAndRaise(OrderProperty, ref _order, value);
            }
        }

        public static readonly DirectProperty<CategoryItem, bool> IsExpandedProperty =
                AvaloniaProperty.RegisterDirect<CategoryItem, bool>(
                    nameof(IsExpanded),
                    o => o.IsExpanded, unsetValue: false);

        private bool _isExpanded;

        /// <summary>
        /// Gets or sets a value indicating whether this category is expanded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this category is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                //if (_isExpanded == value)
                //    return;
                SetAndRaise(IsExpandedProperty, ref _isExpanded, value);
            }
        }

        private readonly GridEntryCollection<PropertyItem> _properties = new GridEntryCollection<PropertyItem>();

        /// <summary>
        /// Get all the properties in the category.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An enumerable collection of all the properties in the category.
        /// </returns>
        public ReadOnlyObservableCollection<PropertyItem> Properties
        {
            get { return new ReadOnlyObservableCollection<PropertyItem>(_properties); }
        }

        /// <summary>
        /// Gets the <see cref="PropertyItem"/> with the specified property name.
        /// </summary>
        /// <value></value>
        public PropertyItem this[string propertyName]
        {
            get { return _properties[propertyName]; }
        }

        public static readonly DirectProperty<CategoryItem, IComparer<PropertyItem>> ComparerProperty =
                AvaloniaProperty.RegisterDirect<CategoryItem, IComparer<PropertyItem>>(
                    nameof(Comparer),
                    o => o.Comparer);

        private IComparer<PropertyItem> _comparer = new PropertyItemComparer();

        /// <summary>
        /// Gets or sets the comparer used to sort properties.
        /// </summary>
        /// <value>The comparer. </value>
        public IComparer<PropertyItem> Comparer
        {
            get { return _comparer; }
            set
            {
                //if (_comparer == value)
                //    return;
                SetAndRaise(ComparerProperty, ref _comparer, value);
                _properties.Sort(_comparer);
            }
        }

        public static readonly DirectProperty<CategoryItem, bool> HasVisiblePropertiesProperty =
                AvaloniaProperty.RegisterDirect<CategoryItem, bool>(
                    nameof(HasVisibleProperties),
                    o => o.HasVisibleProperties, unsetValue: true);

        private bool _hasVisibleProperties;

        /// <summary>
        /// Gets or sets a value indicating whether this instance has visible properties.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has visible properties; otherwise, <c>false</c>.
        /// </value>
        public bool HasVisibleProperties
        {
            get { return _hasVisibleProperties; }
            private set
            {
                //if (_hasVisibleProperties == value)
                //    return;
                SetAndRaise(HasVisiblePropertiesProperty, ref _hasVisibleProperties, value);
            }
        }

        public static new readonly DirectProperty<CategoryItem, bool> IsVisibleProperty =
                AvaloniaProperty.RegisterDirect<CategoryItem, bool>(
                    nameof(IsVisible),
                    o => o.IsVisible, unsetValue: true);

        /// <summary>
        /// Gets a value indicating whether this instance should be visible.
        /// </summary>
        public new bool IsVisible
        {
            get { return base.IsVisible && HasVisibleProperties; }
        }

        //prop.IsBrowsable && prop.MatchesFilter

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryItem"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="name">The name.</param>
        public CategoryItem(PropertyGrid owner, string name)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            this.FilterApplied+= (o, e) =>
            {
                RaisePropertyChanged(IsBrowsableProperty, !IsBrowsable, IsBrowsable);
                RaisePropertyChanged(HasVisiblePropertiesProperty, !HasVisibleProperties, HasVisibleProperties);
                RaisePropertyChanged(MatchesFilterProperty, !MatchesFilter, MatchesFilter);
            };
            Owner = owner;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryItem"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="category">The category.</param>
        public CategoryItem(PropertyGrid owner, CategoryAttribute category)
          : this(owner, category.Category)
        {
            Attribute = category;
        }

        private static readonly Func<PropertyItem, bool> IsPropertyVisible = prop =>
                                                           prop.IsBrowsable && prop.MatchesFilter;

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="property">The property.</param>
        public void AddProperty(PropertyItem property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (_properties.Contains(property))
                throw new ArgumentException("Cannot add a duplicated property " + property.Name);

            int index = _properties.BinarySearch(property, _comparer);
            if (index < 0)
                index = ~index;

            _properties.Insert(index, property);

            if (property.IsBrowsable)
                HasVisibleProperties = true;
            else
                HasVisibleProperties = _properties.Any(IsPropertyVisible);

            property.BrowsableChanged += PropertyBrowsableChanged;
        }

        private void PropertyBrowsableChanged(object sender, EventArgs e)
        {
            HasVisibleProperties = _properties.Any(IsPropertyVisible);
        }

        /// <summary>
        /// Checks whether the entry matches the filtering predicate.
        /// </summary>
        /// <param name="predicate">The filtering predicate.</param>
        /// <returns>
        /// 	<c>true</c> if entry matches predicate; otherwise, <c>false</c>.
        /// </returns>
        public override bool MatchesPredicate(PropertyFilterPredicate predicate)
        {
            return _properties.All(property => property.MatchesPredicate(predicate));
        }

        /// <summary>
        /// Applies the filter for the entry.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public override void ApplyFilter(PropertyFilter filter)
        {
            bool propertiesMatch = false;
            foreach (var entry in Properties)
            {
                if (PropertyMatchesFilter(filter, entry))
                    propertiesMatch = true;
            }

            HasVisibleProperties = _properties.Any(IsPropertyVisible);
            MatchesFilter = propertiesMatch;

            if (propertiesMatch && !IsExpanded)
                IsExpanded = true;

            OnFilterApplied(filter);
        }

        private static bool PropertyMatchesFilter(PropertyFilter filter, PropertyItem entry)
        {
            entry.ApplyFilter(filter);
            return entry.MatchesFilter;
        }
    }
}
