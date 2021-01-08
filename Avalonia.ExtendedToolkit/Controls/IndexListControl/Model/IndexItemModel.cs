using System;
using System.Linq;
using Avalonia.ExtendedToolkit.Extensions;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// item for the index items control
    /// </summary>
    public class IndexItemModel : ReactiveObject
    {
        private bool _isVisible = true;

        /// <summary>
        /// property which is used to filter out this item
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isVisible, value);
            }
        }

        private string _text;

        /// <summary>
        /// text to display
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { this.RaiseAndSetIfChanged(ref _text, value); }
        }

        private object _itemData;

        /// <summary>
        /// data context of this item
        /// </summary>
        public object ItemData
        {
            get { return _itemData; }
            set
            {
                this.RaiseAndSetIfChanged(ref _itemData, value);
            }
        }

        private IndexItemModels _subItems = new IndexItemModels();

        /// <summary>
        /// subitems for this item (only one level)
        /// </summary>
        public IndexItemModels SubItems
        {
            get { return _subItems; }
            set { this.RaiseAndSetIfChanged(ref _subItems, value); }
        }

        /// <summary>
        /// filters this item or subitems
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        internal bool ApplyFilter(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                IsVisible = true;
                SubItems.ForEach(item => item.IsVisible = true);
                return true;
            }

            IsVisible = Text.Contains(searchText, StringComparison.OrdinalIgnoreCase);

            if (SubItems.Count > 0)
            {
                var items = SubItems.Where(x => x.ApplyFilter(searchText));
                items.ForEach(items => items.IsVisible = true);

                IsVisible = items.Any();
            }

            return IsVisible;
        }
    }
}
