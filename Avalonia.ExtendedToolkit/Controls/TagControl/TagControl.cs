using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Itemscontrol which hold tag items
    /// </summary>
    public class TagControl : ListBox
    {
        private const string AddTagButton = "btnAddTagButton";
        private const string PART_ItemsPresenter = "PART_ItemsPresenter";
        private Button _addTagButton;

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

        public IList<string> PossibleSuggestedTags
        {
            get
            {
                var tagItems = GetTagItems();
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
        /// deselcts the other items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnTagControlSelected(object sender, EventArgs e)
        {
            if (sender is TagItem tagItem)
            {
                var ctr = FindInContainer(ItemContainerGenerator, SelectedItem);
                if (ctr.Any())
                {
                    foreach (var item in ctr.Where(x => x != tagItem))
                    {
                        item.Selected -= OnTagControlSelected;
                        item.IsInEditMode = item.IsSelected = false;
                        item.Selected += OnTagControlSelected;
                    }
                }

                if (tagItem.IsSelected)
                {
                    SelectedItem = tagItem.DataContext;
                }
                else
                {
                }
            }
        }

        private IEnumerable<TagItem> FindInContainer(IItemContainerGenerator containerGenerator, object item)
        {
            IEnumerable<ItemContainerInfo> containers = containerGenerator.Containers;
            var result = new List<TagItem>();
            foreach (ItemContainerInfo container in containers)
            {
                if (container.Item == item)
                {
                    result.Add(container.ContainerControl as TagItem);
                }
            }

            return result;
        }

        internal IEnumerable<TagItem> GetTagItems()
        {
            var result = new List<TagItem>();
            foreach (ItemContainerInfo container in ItemContainerGenerator.Containers)
            {
                if (container.Item is TagItem tagItem)
                {
                    result.Add(tagItem);
                }
            }

            return result;
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            var result = new TagControlItemsGenerator(this,
                                                    TagItem.TextProperty,
                                                    TagItem.TemplateProperty);

            return result;
        }

        /// <summary>
        /// raises the <see cref="AddTagButtonClickEvent"/>
        /// and fires <see cref="AddTagCommand"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(AddTagButtonClickEvent, this));

            if (AddTagCommand?.CanExecute(AddTagCommandParameter) == true)
            {
                AddTagCommand.Execute(AddTagCommandParameter);
            }

            this.InvalidateVisual();
        }

        /// <summary>
        /// removes the item from the collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void OnTagControlClosed(object sender, EventArgs args)
        {
            if (sender is TagItem tagControl)
            {
                tagControl.IsSelected = true;
            }

            if (CloseTagCommand?.CanExecute(CloseTagCommandParameter) == true)
            {
                CloseTagCommand?.Execute(CloseTagCommandParameter);
            }
        }

        /// <summary>
        /// Defines the AddTagButtonClick routed event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> AddTagButtonClickEvent =
        RoutedEvent.Register<TagControl, RoutedEventArgs>(nameof(AddTagButtonClickEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Gets or sets AddTagButtonClick eventhandler.
        /// </summary>
        public event EventHandler AddTagButtonClick
        {
            add
            {
                AddHandler(AddTagButtonClickEvent, value);
            }
            remove
            {
                RemoveHandler(AddTagButtonClickEvent, value);
            }
        }

        /// <summary>
        /// Gets or sets CloseTagCommand.
        /// </summary>
        public ICommand CloseTagCommand
        {
            get { return (ICommand)GetValue(CloseTagCommandProperty); }
            set { SetValue(CloseTagCommandProperty, value); }
        }

        /// <summary>
        /// Defines the CloseTagCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> CloseTagCommandProperty =
        AvaloniaProperty.Register<TagControl, ICommand>(nameof(CloseTagCommand));

        /// <summary>
        /// Gets or sets CloseTagCommandParameter.
        /// </summary>
        public object CloseTagCommandParameter
        {
            get { return (object)GetValue(CloseTagCommandParameterProperty); }
            set { SetValue(CloseTagCommandParameterProperty, value); }
        }

        /// <summary>
        /// Defines the CloseTagCommandParameter property.
        /// </summary>
        public static readonly StyledProperty<object> CloseTagCommandParameterProperty =
        AvaloniaProperty.Register<TagControl, object>(nameof(CloseTagCommandParameter));

        /// <summary>
        /// Gets or sets AddTagCommand.
        /// </summary>
        public ICommand AddTagCommand
        {
            get { return (ICommand)GetValue(AddTagCommandProperty); }
            set { SetValue(AddTagCommandProperty, value); }
        }

        /// <summary>
        /// Defines the AddTagCommand property.
        /// </summary>
        public static readonly StyledProperty<ICommand> AddTagCommandProperty =
        AvaloniaProperty.Register<TagControl, ICommand>(nameof(AddTagCommand));

        /// <summary>
        /// Gets or sets AddTagCommandParameter.
        /// </summary>
        public object AddTagCommandParameter
        {
            get { return (object)GetValue(AddTagCommandParameterProperty); }
            set { SetValue(AddTagCommandParameterProperty, value); }
        }

        /// <summary>
        /// Defines the AddTagCommandParameter property.
        /// </summary>
        public static readonly StyledProperty<object> AddTagCommandParameterProperty =
        AvaloniaProperty.Register<TagControl, object>(nameof(AddTagCommandParameter));

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

        internal void RemoveTag(TagItem tag, bool cancelEvent = false)
        {
            if (Items != null)
            {
                ((IList)Items).Remove(tag.Text);
                if (!cancelEvent)
                {
                    //RaiseTagRemoved(tag);
                }
            }
        }

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

        /// <summary>
        /// sets the ItemsPanelProperty
        /// </summary>
        static TagControl()
        {
            //ItemsPanelProperty.OverrideDefaultValue<TagPanel>(DefaultPanel);
            SelectionModeProperty.OverrideDefaultValue<TagControl>(SelectionMode.Single);
        }

        public TagControl()
        {
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
            ItemsPresenter itemsPresenter = e.NameScope.Find<ItemsPresenter>(PART_ItemsPresenter);

            _addTagButton = e.NameScope.Find<Button>(AddTagButton);
            _addTagButton.Click += AddTagButton_Click;
        }
    }
}