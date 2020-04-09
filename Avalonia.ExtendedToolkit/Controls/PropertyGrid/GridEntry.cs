using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    public abstract class GridEntry : ContentControl, /*INotifyPropertyChanged,*/ IPropertyFilterTarget, IDisposable
    {
        /// <summary>
        /// Gets the name of the encapsulated item.
        /// </summary>
        public new string Name { get; protected set; }

        //private bool _isBrowsable;
        ///// <summary>
        ///// Gets or sets a value indicating whether this instance is browsable.
        ///// </summary>
        ///// <value>
        ///// 	<c>true</c> if this instance is browsable; otherwise, <c>false</c>.
        ///// </value>
        //public bool IsBrowsable
        //{
        //    get { return _isBrowsable; }
        //    set
        //    {
        //        if (_isBrowsable == value)
        //            return;
        //        _isBrowsable = value;
        //        OnPropertyChanged();
        //        OnPropertyChanged(nameof(IsVisible));
        //        OnBrowsableChanged();
        //    }
        //}

        public static readonly DirectProperty<GridEntry, bool> IsBrowsableProperty =
                AvaloniaProperty.RegisterDirect<GridEntry, bool>(
                    nameof(IsBrowsable),
                    o => o.IsBrowsable);

        private bool _isBrowsable;

        public bool IsBrowsable
        {
            get { return _isBrowsable; }
            set
            {
                SetAndRaise(IsBrowsableProperty, ref _isBrowsable, value);
                RaisePropertyChanged(IsVisibleProperty, !IsVisible, IsVisible);
                OnBrowsableChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance should be visible.
        /// </summary>
        //public new virtual bool IsVisible
        //{
        //    get { return IsBrowsable && MatchesFilter; }
        //}

        public static new readonly DirectProperty<GridEntry, bool> IsVisibleProperty =
                AvaloniaProperty.RegisterDirect<GridEntry, bool>(
                    nameof(IsVisible),
                    o => o.IsVisible);

        public new bool IsVisible
        {
            get { return IsBrowsable && MatchesFilter; }
        }

        /// <summary>
        /// Gets or sets the owner of the item.
        /// </summary>
        /// <value>The owner of the item.</value>
        public PropertyGrid Owner { get; protected set; }

        //private Editor _editor;
        ///// <summary>
        ///// Gets or sets the editor.
        ///// </summary>
        ///// <value>The editor.</value>
        //public virtual Editor Editor
        //{
        //    get
        //    {
        //        if (_editor == null && Owner != null)
        //            _editor = Owner.GetEditor(this);
        //        return _editor;
        //    }
        //    set
        //    {
        //        _editor = value;
        //        OnPropertyChanged();
        //    }
        //}

        public static readonly DirectProperty<GridEntry, Editor> EditorProperty =
                AvaloniaProperty.RegisterDirect<GridEntry, Editor>(
                    nameof(Editor),
                    o => o.Editor);

        private Editor _editor;

        public Editor Editor
        {
            get
            {
                if (_editor == null && Owner != null)
                    Editor = Owner.GetEditor(this);

                return _editor;
            }

            internal set { SetAndRaise(EditorProperty, ref _editor, value); }
        }

        private bool _disposed;

        /// <summary>
        /// Gets a value indicating whether this <see cref="PropertyItem"/> is disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        protected bool Disposed
        {
            get { return _disposed; }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="PropertyItem"/> is reclaimed by garbage collection.
        /// </summary>
        ~GridEntry()
        {
            Dispose(false);
        }

        /// <summary>
        /// Occurs when filter is applied for the entry.
        /// </summary>
        public event EventHandler<PropertyFilterAppliedEventArgs> FilterApplied;

        /// <summary>
        /// Called when filter was applied for the entry.
        /// </summary>
        /// <param name="filter">The filter.</param>
        protected virtual void OnFilterApplied(PropertyFilter filter)
        {
            var handler = FilterApplied;
            if (handler != null)
                handler(this, new PropertyFilterAppliedEventArgs(filter));
        }

        /// <summary>
        /// Applies the filter for the entry.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public abstract void ApplyFilter(PropertyFilter filter);

        /// <summary>
        /// Checks whether the entry matches the filtering predicate.
        /// </summary>
        /// <param name="predicate">The filtering predicate.</param>
        /// <returns><c>true</c> if entry matches predicate; otherwise, <c>false</c>.</returns>
        public abstract bool MatchesPredicate(PropertyFilterPredicate predicate);

        //private bool _matchesFilter = true;
        ///// <summary>
        ///// Gets or sets a value indicating whether the entry matches filter.
        ///// </summary>
        ///// <value><c>true</c> if entry matches filter; otherwise, <c>false</c>.</value>
        //public bool MatchesFilter
        //{
        //    get { return _matchesFilter; }
        //    protected set
        //    {
        //        if (_matchesFilter == value)
        //            return;
        //        _matchesFilter = value;
        //        OnPropertyChanged();
        //        OnPropertyChanged(nameof(IsVisible));
        //    }
        //}

        public static readonly DirectProperty<GridEntry, bool> MatchesFilterProperty =
                AvaloniaProperty.RegisterDirect<GridEntry, bool>(
                    nameof(MatchesFilter),
                    o => o.MatchesFilter);

        private bool _matchesFilter;

        public bool MatchesFilter
        {
            get { return _matchesFilter; }
            protected set
            {
                if (_matchesFilter == value)
                    return;
                SetAndRaise(MatchesFilterProperty, ref _matchesFilter, value);

                RaisePropertyChanged(IsVisibleProperty, !IsVisible, IsVisible);
            }
        }

        /// <summary>
        /// Occurs when visibility state of the property is changed.
        /// </summary>
        public event EventHandler BrowsableChanged;

        /// <summary>
        /// Called when visibility state of the property is changed.
        /// </summary>
        protected virtual void OnBrowsableChanged()
        {
            var handler = BrowsableChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        //public new event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
