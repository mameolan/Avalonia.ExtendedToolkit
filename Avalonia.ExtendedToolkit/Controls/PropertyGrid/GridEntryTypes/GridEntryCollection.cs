using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Represents a strongly typed collection of <see cref="GridEntry"/>-based items that can be accessed by index or name.
    /// Provides collection change notifications, methods to search, sort, and manipulate lists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class GridEntryCollection<T> : ObservableCollection<T> where T : GridEntry
    {
        private readonly Dictionary<string, T> _itemsMap = new Dictionary<string, T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GridEntryCollection&lt;T&gt;"/> class.
        /// </summary>
        public GridEntryCollection()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridEntryCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="collection"/> parameter cannot be null.
        /// </exception>
        public GridEntryCollection(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            CopyFrom(collection);
        }

        /// <summary>
        /// Searches the entire sorted GridEntryCollection&lt;T&gt; for an element using the specified comparer
        /// and returns the zero-based index of the element.
        /// </summary>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <returns>
        /// The zero-based index of item in the sorted GridEntryCollection&lt;T&gt;,
        /// if item is found; otherwise, a negative number that is the bitwise complement
        /// of the index of the next element that is larger than item or, if there is
        /// no larger element, the bitwise complement of GridEntryCollection&lt;T&gt;.Count.
        /// </returns>
        public int BinarySearch(T item)
        {
            return ((List<T>)Items).BinarySearch(item);
        }

        /// <summary>
        /// Searches the entire sorted GridEntryCollection&lt;T&gt; for an element using the specified comparer
        /// and returns the zero-based index of the element.
        /// </summary>
        /// <param name="item">The object to locate. The value can be null for reference types.</param>
        /// <param name="comparer">
        /// The System.Collections.Generic.IComparer&lt;T&gt; implementation to use when comparing elements.
        /// -or- null to use the default comparer System.Collections.Generic.Comparer&lt;T&gt;.Default.
        /// </param>
        /// <returns>
        /// The zero-based index of item in the sorted GridEntryCollection&lt;T&gt;,
        /// if item is found; otherwise, a negative number that is the bitwise complement
        /// of the index of the next element that is larger than item or, if there is
        /// no larger element, the bitwise complement of GridEntryCollection&lt;T&gt;.Count.
        /// </returns>
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return ((List<T>)Items).BinarySearch(item, comparer);
        }

        /// <summary>
        /// Sorts the elements in the entire collection using the specified comparer.
        /// </summary>
        /// <param name="comparer">
        /// The System.Collections.Generic.IComparer&lt;T&gt; implementation to use when comparing elements.
        /// -or- null to use the default comparer System.Collections.Generic.Comparer&lt;T&gt;.Default.
        /// </param>
        public void Sort(IComparer<T> comparer)
        {
            ((List<T>)Items).Sort(comparer);
            OnItemsChanged();
        }

        private void OnItemsChanged()
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null, -1));
        }

        /// <summary>
        /// Copies values from collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        protected void CopyFrom(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Add(enumerator.Current);
                    }
                }
            }
        }

        #region ObservableCollection implementation

        /// <summary>
        /// Inserts an item into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        protected override void InsertItem(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            EncacheItem(item);
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Removes the item at the specified index of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
        {
            T item = Items[index];
            DecacheItem(item);
            base.RemoveItem(index);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            _itemsMap.Clear();
            base.ClearItems();
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index.</param>
        protected override void SetItem(int index, T item)
        {
            DecacheItem(this[index]);
            EncacheItem(item);
            base.SetItem(index, item);
        }

        #endregion ObservableCollection implementation

        /// <summary>
        /// Gets the item with the specified name or null if no item with the name specified was found.
        /// </summary>
        /// <value></value>
        public T this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    return null;

                if (_itemsMap.ContainsKey(name))
                    return _itemsMap[name];
                else
                    return null;
            }
        }

        private void EncacheItem(T item)
        {
            if (_itemsMap.ContainsKey(item.Name))
                throw new ArgumentException(string.Format("The entry '{0}' is already added to collection!", item.Name));

            _itemsMap.Add(item.Name, item);
        }

        private void DecacheItem(T item)
        {
            if (_itemsMap.ContainsKey(item.Name))
                _itemsMap.Remove(item.Name);
        }
    }
}
