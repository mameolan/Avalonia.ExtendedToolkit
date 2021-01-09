using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Extensions;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// a control with a grouped list and
    /// buttons to switch to this grouped section
    /// </summary>
    public class IndexItemsControl : TemplatedControl, IDisposable
    {
        /// <summary>
        /// default index entries
        /// </summary>
        private static string[] _defaultIndexes = new string[]
        {"#","A","B","C","D","E","F","G","H",
         "I","J","K","L","M","N","O","P","Q",
         "R","S","T","U","V","W","X","Y","Z"};

        private IObservable<Func<IndexItemModel, bool>> _filter;
        private IObservable<IChangeSet<IndexItemModel>> _items;
        private IndexItemModels _indexEntries = new IndexItemModels();
        
        private CompositeDisposable _disposables;


        private IndexList _contentIndexList;

        /// <summary>
        /// Gets or sets SelectedItem.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            private set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Defines the SelectedItem property.
        /// </summary>
        public static readonly StyledProperty<object> SelectedItemProperty =
        AvaloniaProperty.Register<IndexItemsControl, object>(nameof(SelectedItem));

        /// <summary>
        /// Defines the IndexEntries direct property.
        /// </summary>
        public static readonly DirectProperty<IndexItemsControl, ReadOnlyObservableCollection<IndexItemModel>> IndexItemsProperty =
                AvaloniaProperty.RegisterDirect<IndexItemsControl, ReadOnlyObservableCollection<IndexItemModel>>(
                nameof(IndexItems),
                o => o.IndexItems);

        private ReadOnlyObservableCollection<IndexItemModel> _indexItems;

        /// <summary>
        /// Gets or sets IndexEntries.
        /// </summary>
        public ReadOnlyObservableCollection<IndexItemModel> IndexItems
        {
            get { return _indexItems; }
            private set
            {
                SetAndRaise(IndexItemsProperty, ref _indexItems, value);
            }
        }

        /// <summary>
        /// Gets or sets ShowEmptyItems.
        /// </summary>
        public bool ShowEmptyItems
        {
            get { return (bool)GetValue(ShowEmptyItemsProperty); }
            set { SetValue(ShowEmptyItemsProperty, value); }
        }

        /// <summary>
        /// Defines the ShowEmptyItems property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowEmptyItemsProperty =
        AvaloniaProperty.Register<IndexItemsControl, bool>(nameof(ShowEmptyItems), defaultValue: true);

        /// <summary>
        /// Defines the PressedCommand direct property.
        /// </summary>
        internal static readonly DirectProperty<IndexItemsControl, ICommand> PressedCommandProperty =
        AvaloniaProperty.RegisterDirect<IndexItemsControl, ICommand
        >(nameof(PressedCommand),
        o => o.PressedCommand);

        /// <summary>
        /// Gets or sets PressedCommand.
        /// </summary>
        internal ICommand PressedCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Defines the IndexSectionItems direct property.
        /// </summary>
        public static readonly DirectProperty<IndexItemsControl, ObservableCollection<string>> IndexSectionItemsProperty =
                        AvaloniaProperty.RegisterDirect<IndexItemsControl, ObservableCollection<string>>(
                        nameof(IndexSectionItems),
                        o => o.IndexSectionItems);

        private ObservableCollection<string> _indexSectionItems
                    = new ObservableCollection<string>(_defaultIndexes);

        /// <summary>
        /// Gets or sets IndexSectionItems.
        /// </summary>
        public ObservableCollection<string> IndexSectionItems
        {
            get { return _indexSectionItems; }
            set
            {
                SetAndRaise(IndexSectionItemsProperty, ref _indexSectionItems, value);
            }
        }

        /// <summary>
        /// Gets or sets ShowSearch.
        /// </summary>
        public bool ShowSearch
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }

        /// <summary>
        /// Defines the ShowSearch property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowSearchProperty =
        AvaloniaProperty.Register<IndexItemsControl, bool>(nameof(ShowSearch), defaultValue: true);

        /// <summary>
        /// Gets or sets SearchText.
        /// </summary>
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        /// <summary>
        /// Defines the SearchText property.
        /// </summary>
        public static readonly StyledProperty<string> SearchTextProperty =
        AvaloniaProperty.Register<IndexItemsControl, string>(nameof(SearchText));

        /// <summary>
        /// initilaizes som changed events
        /// and initilize the pressed command
        /// </summary>
        public IndexItemsControl()
        {
            PressedCommand = ReactiveCommand.Create<IndexItemModel>(x => ExecutePressedCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            DataContextProperty.Changed.AddClassHandler<IndexItemsControl>((o, e) => OnDataContextChanged(o, e));

            IndexSectionItemsProperty.Changed.AddClassHandler<IndexItemsControl>((o, e) => OnIndexSectionItemsChanged(o, e));

            ShowEmptyItemsProperty.Changed.AddClassHandler<IndexItemsControl>((o, e) => OnShowEmptyItemsChanged(o, e));

            _filter = this.WhenAnyValue(x => x.SearchText)
                        .Delay(TimeSpan.FromMilliseconds(100))
                        .Select(BuildFilter);

            this.WhenAnyValue(x => x.ShowSearch).Subscribe((showSearch) =>
            {
                //reset the filter if showSearch is false
                if (showSearch == false && string.IsNullOrEmpty(SearchText) == false)
                {
                    SearchText = string.Empty;
                }
            });
        }

        /// <summary>
        /// filter for the search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private Func<IndexItemModel, bool> BuildFilter(string searchText)
        {
            return entry => entry.ApplyFilter(searchText);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnShowEmptyItemsChanged(IndexItemsControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (_contentIndexList == null || e.NewValue == null)
            {
                return;
            }

            _contentIndexList.ShowEmptyItems = (bool)e.NewValue;
        }

        /// <summary>
        /// clears the <see cref="_indexEntries"/>
        /// and raises the datacontext changed event
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnIndexSectionItemsChanged(IndexItemsControl o, object e)
        {
            CreateBaseSections();
            UpdateData(DataContext);
        }

        /// <summary>
        /// creates the alphabeticall entries
        /// </summary>
        private void CreateBaseSections()
        {
            _indexEntries.Clear();

            IndexSectionItems.ForEach(item => _indexEntries.Add(new IndexItemModel { Text = item }));
        }

        /// <summary>
        /// scrolls the item into the view
        /// </summary>
        /// <param name="item"></param>
        private void ExecutePressedCommand(IndexItemModel item)
        {
            _contentIndexList.ScrollIntoView(item);
        }

        /// <summary>
        /// if indexentries.count is zero return
        /// else fills the index entries
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnDataContextChanged(IndexItemsControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (_indexEntries.Count == 0)
            {
                CreateBaseSections();
            }

            UpdateData(e.NewValue);
        }

        /// <summary>
        /// resets the <see cref="IndexItems"/>
        /// </summary>
        /// <param name="datacontext"></param>
        private void UpdateData(object datacontext)
        {
            if (datacontext is IEnumerable<IndexItemModel> items)
            {
                var tempItems = items.OrderBy(x => x.Text, new AlphanumComparatorFast())
                                .Select(item => item.Text.First())
                                .Distinct()
                                .ToDictionary(l => l.ToString(), l => items.Where(x => x.Text.StartsWith(l.ToString())));
                foreach (var item in tempItems)
                {
                    IndexItemModel mainIndex = null;

                    if (char.IsDigit(item.Key.First()))
                    {
                        mainIndex = _indexEntries.Where(x => x.Text == "#").FirstOrDefault();
                    }
                    else
                    {
                        mainIndex = _indexEntries.Where(x => x.Text == item.Key).FirstOrDefault();
                    }

                    if (mainIndex == null)
                    {
                        Debug.WriteLine($"WARNING: {item.Key} not found");
                        continue;
                    }

                    item.Value.ForEach(it => mainIndex.SubItems.Add(it));
                }

                var comparer = SortExpressionComparer<IndexItemModel>.Ascending(x => x.Compare(_indexSectionItems));

                if (_disposables != null)
                {
                    _disposables.Dispose();
                }

                var sourceList = new SourceList<IndexItemModel>();
                sourceList.AddRange(_indexEntries);
                _items = sourceList.Connect();
                _items.Filter(_filter)
                .Sort(comparer) /*need this here because after filtering the list is incorrect ordered*/
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _indexItems)
                .DisposeMany()
                .Subscribe();

                _disposables = new CompositeDisposable(sourceList);

                RaisePropertyChanged(IndexItemsProperty, null, _indexItems);
            }
        }



        /// <summary>
        /// gets the needed controls
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            _contentIndexList = e.NameScope.Find<IndexList>("contentListBox");
            _contentIndexList.ShowEmptyItems = ShowEmptyItems;
            _contentIndexList.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        /// sets the <see cref="SelectedItem"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = e.AddedItems.OfType<object>().FirstOrDefault();
        }

        /// <summary>
        /// calls <see cref="Dispose(bool)"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// disposes the items
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposables?.Dispose();
            }
        }

    }
}
