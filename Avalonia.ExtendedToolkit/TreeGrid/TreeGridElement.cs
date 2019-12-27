using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Avalonia.ExtendedToolkit
{
    public class TreeGridElement : TemplatedControl
    {
        private const string NullItemError = "The item added to the collection cannot be null.";

        public bool HasChildren
        {
            get { return (bool)GetValue(HasChildrenProperty); }
            set { SetValue(HasChildrenProperty, value); }
        }

        public static readonly AvaloniaProperty HasChildrenProperty =
            AvaloniaProperty.Register<TreeGridElement, bool>(nameof(HasChildren));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly AvaloniaProperty IsExpandedProperty =
            AvaloniaProperty.Register<TreeGridElement, bool>(nameof(IsExpanded));

        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        public static readonly AvaloniaProperty LevelProperty =
            AvaloniaProperty.Register<TreeGridElement, int>(nameof(Level), defaultValue: 0);

        public TreeGridElement Parent { get; private set; }
        public TreeGridModel Model { get; private set; }

        public ObservableCollection<TreeGridElement> Children
        { get; private set; } = new ObservableCollection<TreeGridElement>();

        public static RoutedEvent<RoutedEventArgs> ExpandingEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(ExpandingEvent), RoutingStrategies.Bubble);

        public event EventHandler Expanding
        {
            add
            {
                AddHandler(ExpandingEvent, value);
            }
            remove
            {
                RemoveHandler(ExpandingEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> ExpandedEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(ExpandedEvent), RoutingStrategies.Bubble);

        public event EventHandler Expanded
        {
            add
            {
                AddHandler(ExpandedEvent, value);
            }
            remove
            {
                RemoveHandler(ExpandedEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> CollapsingEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(CollapsingEvent), RoutingStrategies.Bubble);

        public event EventHandler Collapsing
        {
            add
            {
                AddHandler(CollapsingEvent, value);
            }
            remove
            {
                RemoveHandler(CollapsingEvent, value);
            }
        }

        public static RoutedEvent<RoutedEventArgs> CollapsedEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(CollapsedEvent), RoutingStrategies.Bubble);

        public event EventHandler Collapsed
        {
            add
            {
                AddHandler(CollapsedEvent, value);
            }
            remove
            {
                RemoveHandler(CollapsedEvent, value);
            }
        }

        public TreeGridElement()
        {
            IsExpandedProperty.Changed.AddClassHandler<TreeGridElement>((o, e) => OnIsExpandedChanged(o, e));
            // Attach events
            Children.CollectionChanged += OnChildrenChanged;
        }

        internal void SetModel(TreeGridModel model, TreeGridElement parent = null)
        {
            // Set the element information
            Model = model;
            Parent = parent;
            Level = ((parent != null) ? parent.Level + 1 : 0);

            // Iterate through all child elements
            foreach (TreeGridElement child in Children)
            {
                // Set the model for the child
                child.SetModel(model, this);
            }
        }

        protected virtual void OnExpanding()
        {
        }

        protected virtual void OnExpanded()
        {
        }

        protected virtual void OnCollapsing()
        {
        }

        protected virtual void OnCollapsed()
        {
        }

        private void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            // Process the event
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    // Process added child
                    OnChildAdded(args.NewItems[0]);
                    break;

                case NotifyCollectionChangedAction.Replace:

                    // Process replaced child
                    OnChildReplaced((TreeGridElement)args.OldItems[0], args.NewItems[0], args.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:

                    // Process removed child
                    OnChildRemoved((TreeGridElement)args.OldItems[0]);
                    break;

                case NotifyCollectionChangedAction.Reset:

                    // Process cleared children
                    OnChildrenCleared(args.OldItems);
                    break;
            }
        }

        private void OnChildAdded(object item)
        {
            // Verify the new child
            TreeGridElement child = VerifyItem(item);

            // Set the model for the child
            child.SetModel(Model, this);

            // Notify the model that a child was added to the item
            Model?.OnChildAdded(child);
        }

        private void OnChildReplaced(TreeGridElement oldChild, object item, int index)
        {
            // Verify the new child
            TreeGridElement child = VerifyItem(item);

            // Clear the model for the old child
            oldChild.SetModel(null);

            // Notify the model that a child was replaced
            Model?.OnChildReplaced(oldChild, child, index);
        }

        private void OnChildRemoved(TreeGridElement child)
        {
            // Clear the model for the child
            child.SetModel(null);

            // Notify the model that a child was removed from the item
            Model?.OnChildRemoved(child);
        }

        private void OnChildrenCleared(IList children)
        {
            // Iterate through all of the children
            foreach (TreeGridElement child in children)
            {
                // Clear the model for the child
                child.SetModel(null);
            }

            // Notify the model that all of the children were removed from the item
            Model?.OnChildrenRemoved(this, children);
        }

        internal static TreeGridElement VerifyItem(object item)
        {
            // Is the item valid?
            if (item == null)
            {
                // The item is not valid
                throw new ArgumentNullException(nameof(item), NullItemError);
            }

            // Return the element
            return (TreeGridElement)item;
        }

        private void OnIsExpandedChanged(TreeGridElement element, AvaloniaPropertyChangedEventArgs args)
        {
            TreeGridElement item = element;

            // Is the item being expanded?
            if ((bool)args.NewValue)
            {
                // Raise expanding event
                item.RaiseEvent(new RoutedEventArgs(ExpandingEvent, item));

                // Execute derived expanding handler
                item.OnExpanding();

                // Expand the item in the model
                item.Model?.Expand(item);

                // Raise expanded event
                item.RaiseEvent(new RoutedEventArgs(ExpandedEvent, item));

                // Execute derived expanded handler
                item.OnExpanded();
            }
            else
            {
                // Raise collapsing event
                item.RaiseEvent(new RoutedEventArgs(CollapsingEvent, item));

                // Execute derived collapsing handler
                item.OnCollapsing();

                // Collapse the item in the model
                item.Model?.Collapse(item);

                // Raise collapsed event
                item.RaiseEvent(new RoutedEventArgs(CollapsedEvent, item));

                // Execute derived collapsed handler
                item.OnCollapsed();
            }
        }
    }
}