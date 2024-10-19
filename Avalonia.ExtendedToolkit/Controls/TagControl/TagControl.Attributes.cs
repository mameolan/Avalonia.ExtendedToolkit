using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class TagControl
    {
        private const string AddTagButton = "btnAddTagButton";
        private Button _addTagButton;

        /// <summary>
        /// Defines the ItemsSource direct property.
        /// </summary>
        internal static readonly DirectProperty<TagControl, AvaloniaList<TagItem>> ItemsSourceProperty =
        AvaloniaProperty.RegisterDirect<TagControl, AvaloniaList<TagItem>>(
        nameof(ItemsSource),
        o => o.ItemsSource);

        private AvaloniaList<TagItem> _itemsSource = new AvaloniaList<TagItem>();

        /// <summary>
        /// Gets or sets ItemsSource.
        /// </summary>
        internal AvaloniaList<TagItem> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                SetAndRaise(ItemsSourceProperty, ref _itemsSource, value);
            }
        }

        /// <summary>
        /// Defines the Items direct property.
        /// </summary>
        public static readonly DirectProperty<TagControl, IList<string>> ItemsProperty =
        AvaloniaProperty.RegisterDirect<TagControl, IList<string>>(
        nameof(Items),
        o => o.Items, (o, e) => o.Items = e);

        private IList<string> _items;

        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public IList<string> Items
        {
            get { return _items; }
            set
            {
                SetAndRaise(ItemsProperty, ref _items, value);
            }
        }

        /// <summary>
        /// Gets or sets ItemsPanel.
        /// </summary>
        public ITemplate<Panel> ItemsPanel
        {
            get { return (ITemplate<Panel>)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        /// <summary>
        /// Defines the ItemsPanel property.
        /// </summary>
        public static readonly StyledProperty<ITemplate<Panel>> ItemsPanelProperty =
        AvaloniaProperty.Register<TagControl, ITemplate<Panel>>(nameof(ItemsPanel));

        /// <summary>
        /// Defines the <see cref="SelectedItem"/> property.
        /// </summary>
        public static readonly DirectProperty<TagControl, object> SelectedItemProperty =
            SelectingItemsControl.SelectedItemProperty.AddOwner<TagControl>(
                o => o.SelectedItem,
                (o, v) => o.SelectedItem = v);

        private object _selectedItem;

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetAndRaise(SelectedItemProperty, ref _selectedItem, value);
            }
        }

        /// <summary>
        /// Defines the SuggestedTags direct property.
        /// </summary>
        public static readonly DirectProperty<TagControl, IList<string>> SuggestedTagsProperty =
        AvaloniaProperty.RegisterDirect<TagControl, IList<string>>(
        nameof(SuggestedTags),
        o => o.SuggestedTags,
        (o, v) => o.SuggestedTags = v);

        private IList<string> _suggestedTags = new List<string>();

        /// <summary>
        /// Gets or sets SuggestedTags.
        /// </summary>

        public IList<string> SuggestedTags
        {
            get { return _suggestedTags; }
            set
            {
                SetAndRaise(SuggestedTagsProperty, ref _suggestedTags, value);
            }
        }

        /// <summary>
        /// returns a sugested item list
        /// </summary>
        /// <value></value>
        public IList<string> PossibleSuggestedTags
        {
            get
            {
                var tagItems = ItemsSource;
                if (!ReferenceEquals(tagItems, null) && tagItems.Any())
                {
                    var tokenizedTagItems = tagItems;
                    var typedTags = tokenizedTagItems.Select(item => item.Text);
                    return SuggestedTags?
                        .Except(typedTags)
                        .ToList();
                }
                return SuggestedTags;
            }
        }

        /// <summary>
        /// Defines the IsAnyItemInEditMode direct property.
        /// </summary>
        public static readonly DirectProperty<TagControl, bool> IsAnyItemInEditModeProperty =
        AvaloniaProperty.RegisterDirect<TagControl, bool>(
        nameof(IsAnyItemInEditMode),
        o => o.IsAnyItemInEditMode);

        /// <summary>
        /// Gets or sets IsAnyItemInEditMode.
        /// </summary>
        public bool IsAnyItemInEditMode
        {
            get { return ItemsSource?.Any(x => x.IsInEditMode) == true; }
        }

        /// <summary>
        /// Defines the TagAdded routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> TagAddedEvent =
        RoutedEvent.Register<TagControl, RoutedEventArgs>(nameof(TagAddedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets TagAdded eventhandler.
        /// </summary>
        public event EventHandler TagAdded
        {
            add
            {
                AddHandler(TagAddedEvent, value);
            }
            remove
            {
                RemoveHandler(TagAddedEvent, value);
            }
        }

        /// <summary>
        /// Defines the TagRemoved routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> TagRemovedEvent =
        RoutedEvent.Register<TagControl, RoutedEventArgs>(nameof(TagRemovedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets TagRemoved eventhandler.
        /// </summary>
        public event EventHandler TagRemoved
        {
            add
            {
                AddHandler(TagRemovedEvent, value);
            }
            remove
            {
                RemoveHandler(TagRemovedEvent, value);
            }
        }

        /// <summary>
        /// Defines the TagEdited routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> TagEditedEvent =
        RoutedEvent.Register<TagControl, RoutedEventArgs>(nameof(TagEditedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets TagEdited eventhandler.
        /// </summary>
        public event EventHandler TagEdited
        {
            add
            {
                AddHandler(TagEditedEvent, value);
            }
            remove
            {
                RemoveHandler(TagEditedEvent, value);
            }
        }

        /// <summary>
        /// Gets or sets TagRemovedCommand.
        /// </summary>
        public ICommand TagRemovedCommand
        {
            get { return (ICommand)GetValue(TagRemovedCommandProperty); }
            set { SetValue(TagRemovedCommandProperty, value); }
        }

        /// <summary>
        /// Defines the TagRemovedCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> TagRemovedCommandProperty =
        AvaloniaProperty.Register<TagControl, ICommand>(nameof(TagRemovedCommand));

        /// <summary>
        /// Gets or sets TagAddedCommand.
        /// </summary>
        public ICommand TagAddedCommand
        {
            get { return (ICommand)GetValue(TagAddedCommandProperty); }
            set { SetValue(TagAddedCommandProperty, value); }
        }

        /// <summary>
        /// Defines the TagAddedCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> TagAddedCommandProperty =
        AvaloniaProperty.Register<TagControl, ICommand>(nameof(TagAddedCommand));

        /// <summary>
        /// Gets or sets TagAddedCommandParameter.
        /// </summary>
        public object TagAddedCommandParameter
        {
            get { return (object)GetValue(TagAddedCommandParameterProperty); }
            set { SetValue(TagAddedCommandParameterProperty, value); }
        }

        /// <summary>
        /// Defines the TagAddedCommandParameter property.
        /// </summary>
        public static readonly StyledProperty<object> TagAddedCommandParameterProperty =
        AvaloniaProperty.Register<TagControl, object>(nameof(TagAddedCommandParameter));

        /// <summary>
        /// Gets or sets TagEditedCommand.
        /// </summary>
        public ICommand TagEditedCommand
        {
            get { return (ICommand)GetValue(TagEditedCommandProperty); }
            set { SetValue(TagEditedCommandProperty, value); }
        }

        /// <summary>
        /// Defines the TagEditedCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> TagEditedCommandProperty =
        AvaloniaProperty.Register<TagControl, ICommand>(nameof(TagEditedCommand));

        /// <summary>
        /// Gets or sets ShowAddButton.
        /// </summary>
        public bool ShowAddButton
        {
            get { return (bool)GetValue(ShowAddButtonProperty); }
            set { SetValue(ShowAddButtonProperty, value); }
        }

        /// <summary>
        /// Defines the ShowAddButton property.
        /// </summary>
        public static readonly StyledProperty<bool> ShowAddButtonProperty =
        AvaloniaProperty.Register<TagControl, bool>(nameof(ShowAddButton), defaultValue: false);

        /// <summary>
        /// Gets or sets TagMargin.
        /// </summary>
        public Thickness TagMargin
        {
            get { return (Thickness)GetValue(TagMarginProperty); }
            set { SetValue(TagMarginProperty, value); }
        }

        /// <summary>
        /// Defines the TagMargin property.
        /// </summary>
        public static readonly StyledProperty<Thickness> TagMarginProperty =
        AvaloniaProperty.Register<TagControl, Thickness>(nameof(TagMargin), defaultValue: new Thickness(5));

        private bool _suggestedValuesOnly = false;

        public static readonly DirectProperty<TagControl, bool> SuggestedValuesOnlyProperty =
        AvaloniaProperty.RegisterDirect<TagControl, bool>(
        nameof(_suggestedValuesOnly),
        o => o._suggestedValuesOnly,
        (o, v) => o._suggestedValuesOnly = v);

        public bool SuggestedValuesOnly
        {
            get { return _suggestedValuesOnly; }
            set
            {
                SetAndRaise(SuggestedValuesOnlyProperty, ref _suggestedValuesOnly, value);
            }
        }
    }
}
