using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DynamicData.Kernel;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Control which hold tag items
    /// base on the work of: https://github.com/niieani/TokenizedInputCs
    /// </summary>
    public partial class TagControl : TemplatedControl
    {
        /// <summary>
        /// deselects the other items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnTagControlSelected(object sender, EventArgs e)
        {
            if (sender is TagItem tagItem)
            {
                if (ItemsSource.Any())
                {
                    foreach (var item in ItemsSource.Where(x => x != tagItem))
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
                    SelectedItem = null;
                }
            }
        }

        /// <summary>
        /// updates <see cref="IsAnyItemInEditMode"/>
        /// </summary>
        private void OnTagItemPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            RaisePropertyChanged<bool>(IsAnyItemInEditModeProperty, new bool(), ItemsSource.Any(x => x.IsInEditMode));
        }

        /// <summary>
        /// raises the <see cref="TagAddedEvent"/>
        /// and fires <see cref="TagAddedCommand"/>
        /// </summary>
        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTagItem();
        }

        /// <summary>
        /// - Raises the <see cref="TagAdded"/>
        /// - Creates a new <see cref="TagItem"/> in edit mode
        /// - executes <see cref="TagAddedCommand"/>
        /// </summary>
        internal void CreateNewTagItem()
        {
            RaiseEvent(new RoutedEventArgs(TagAddedEvent, this));

            var item = TagItem.
                                CreateTagItem(string.Empty,
                                              OnTagControlClosed,
                                              OnTagControlSelected,
                                              OnTagItemPropertyChanged,
                                              OnAcceptEdit,
                                              TagMargin);
            item.IsInEditMode = true;
            ItemsSource.Add(item);

            if (TagAddedCommand?.CanExecute(TagAddedCommandParameter) == true)
            {
                TagAddedCommand.Execute(TagAddedCommandParameter);
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
            if (sender is TagItem tagItem)
            {
                tagItem.IsSelected = true;
                SelectedItem=tagItem.Text;

                RemoveTag(tagItem);
            }
        }

        /// <summary>
        /// sets the selection on left or right button click
        /// </summary>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            var prop = e.GetCurrentPoint(this).Properties;

            if (prop.IsLeftButtonPressed || prop.IsRightButtonPressed)
            {
                e.Handled = UpdateSelectionFromEventSource(
                    //.edited
                    (Interactive)e.Source,
                    true);
            }
        }

        /// <summary>
        /// updates the selection from container
        /// </summary>
        protected bool UpdateSelectionFromEventSource(
            Interactive eventSource,
            bool select = true
            )
        {
            var container = GetContainerFromEventSource(eventSource);

            if (container != null)
            {
                UpdateSelectionFromContainer(container, select);
                return true;
            }

            return false;
        }

        /// <summary>
        /// tries to find the TagItem from eventsource
        /// </summary>
        protected TagItem GetContainerFromEventSource(Interactive eventSource)
        {
            var item = ((Visual)eventSource).GetSelfAndVisualAncestors()
                .OfType<TagItem>()
                .FirstOrDefault();
            return item;
        }

        /// <summary>
        /// sets the <see cref="SelectedItem"/>
        /// and sets the <see cref="TagItem.IsInEditMode"/> to false
        /// </summary>
        /// <param name="container"></param>
        /// <param name="isSelect"></param>
        protected void UpdateSelectionFromContainer(
            Control container,
            bool isSelect = true
            )
        {
            if (container is TagItem item)
            {
                List<object> oldItems = new List<object>();
                var tagItens = ItemsSource;
                tagItens.ToList().ForEach(x => x.IsInEditMode = false);

                var lastItem = tagItens.Where(x => x.Text == SelectedItem?.ToString()).FirstOrDefault();
                if (lastItem != null)
                {
                    lastItem.IsSelected = false;
                    oldItems.Add(lastItem);
                }

                if (oldItems.Contains(item) == false)
                {
                    item.IsSelected = isSelect;
                    SelectedItem = item.Text;
                }
                else
                {
                    //remove selection if the item was already selected
                    SelectedItem = null;
                }

                var changed = new SelectionChangedEventArgs(
                    SelectingItemsControl.SelectionChangedEvent,
                    new List<object> { SelectedItem },
                    oldItems);
                RaiseEvent(changed);
            }
        }

        /// <summary>
        /// adds or inserts the TagItem to the <see cref="Items"/>
        /// </summary>
        /// <param name="tagItem"></param>
        private void OnAcceptEdit(TagItem tagItem)
        {
            if (ItemsSource.Count != Items.Count)
            {
                Items.Add(tagItem.Text);
            }
            else
            {
                var index = ItemsSource.IndexOf(tagItem);
                Items[index] = tagItem.Text;
            }

            RaiseEvent(new RoutedEventArgs(TagEditedEvent, tagItem));

            if (TagEditedCommand?.CanExecute(tagItem.Text) == true)
            {
                TagEditedCommand?.Execute(tagItem.Text);
            }
        }

        /// <summary>
        /// removes the TagItem and executes the <see cref="TagRemovedCommand"/>
        /// if cancel event is false
        /// </summary>
        internal void RemoveTag(TagItem tag, bool cancelEvent = false)
        {
            if (Items != null)
            {
                if (Items.Count == ItemsSource.Count)
                {
                    int index = ItemsSource.IndexOf(tag);
                    Items.RemoveAt(index);
                }

                tag.UnregisterEvents(
                                    OnTagControlClosed,
                                    OnTagControlSelected,
                                    OnTagItemPropertyChanged,
                                    OnAcceptEdit);

                ItemsSource.Remove(tag);

                if (!cancelEvent)
                {
                    RaiseEvent(new RoutedEventArgs(TagRemovedEvent, tag));

                    if (TagRemovedCommand?.CanExecute(tag.Text) == true)
                    {
                        TagRemovedCommand?.Execute(tag.Text);
                    }
                }
            }
        }

        /// <summary>
        /// registers the ItemsProperty changes event
        /// </summary>
        public TagControl()
        {
            ItemsProperty.Changed.AddClassHandler<TagControl>((o, e) => OnItemsChanged(o, e));
        }

        /// <summary>
        /// updates the <see cref="ItemsSource"/>
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnItemsChanged(TagControl o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is IList<string> list)
            {
                foreach (var item in list)
                {
                    ItemsSource.Add(TagItem.
                            CreateTagItem(item,
                                          OnTagControlClosed,
                                          OnTagControlSelected,
                                          OnTagItemPropertyChanged,
                                          OnAcceptEdit,
                                          TagMargin));
                }
                RaisePropertyChanged<bool>(IsAnyItemInEditModeProperty, new  bool(), ItemsSource.Any(x => x.IsInEditMode));
            }
        }

        /// <summary>
        /// checks if a tag already exists in the item source
        /// </summary>
        /// <param name="itemToIgnore"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        internal bool ContainsTagText(TagItem itemToIgnore, string compareTo)
        {
            var tagItems = ItemsSource.Where(x => x != itemToIgnore);

            return tagItems
                    .Any(item => item.Text == compareTo);
        }

        /// <summary>
        /// gets the needed template controls
        /// </summary>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _addTagButton = e.NameScope.Find<Button>(AddTagButton);
            _addTagButton.Click += AddTagButton_Click;
        }
    }
}
