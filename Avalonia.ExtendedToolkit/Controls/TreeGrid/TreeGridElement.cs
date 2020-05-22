using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// treegrid element
    /// </summary>
    public class TreeGridElement : TemplatedControl
    {
        private const string NullItemError = "The item added to the collection cannot be null.";

        /// <summary>
        /// gets has children
        /// </summary>
        public bool HasChildren
        {
            get { return (bool)GetValue(HasChildrenProperty); }
            set { SetValue(HasChildrenProperty, value); }
        }

        /// <summary>
        /// <see cref="HasChildren"/>
        /// </summary>
        public static readonly StyledProperty<bool> HasChildrenProperty =
            AvaloniaProperty.Register<TreeGridElement, bool>(nameof(HasChildren));

        /// <summary>
        /// get/sets IsExpanded
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsExpanded"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<TreeGridElement, bool>(nameof(IsExpanded));

        /// <summary>
        /// get/sets Level
        /// </summary>
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        /// <summary>
        /// <see cref="Level"/>
        /// </summary>
        public static readonly StyledProperty<int> LevelProperty =
            AvaloniaProperty.Register<TreeGridElement, int>(nameof(Level), defaultValue: 0);

        /// <summary>
        /// gets the Parent
        /// </summary>
        public TreeGridElement Parent { get; private set; }
        
        /// <summary>
        /// gets the Model
        /// </summary>
        public TreeGridModel Model { get; private set; }

        /// <summary>
        /// collection of chilren
        /// </summary>
        public ObservableCollection<TreeGridElement> Children
        { 
            get; 
            private set; 
        } = new ObservableCollection<TreeGridElement>();

        /// <summary>
        /// <see cref="Expanding"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ExpandingEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(ExpandingEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/sets Expanding handler
        /// </summary>
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

        /// <summary>
        /// <see cref="Expanded"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> ExpandedEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(ExpandedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/set Expanded handler
        /// </summary>
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

        /// <summary>
        /// <see cref="Collapsing"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> CollapsingEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(CollapsingEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/sets Collapsing handler
        /// </summary>
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

        /// <summary>
        /// <see cref="Collapsed"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> CollapsedEvent =
                    RoutedEvent.Register<TreeGridElement, RoutedEventArgs>(nameof(CollapsedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// get/sets Collapsed handler
        /// </summary>
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

        /// <summary>
        /// registers changed handlers
        /// </summary>
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

        /// <summary>
        /// Execute derived expanding handler
        /// </summary>
        protected virtual void OnExpanding()
        {
        }

        /// <summary>
        /// Execute derived expanded handler
        /// </summary>
        protected virtual void OnExpanded()
        {
        }

        /// <summary>
        /// Execute derived collapsing handler
        /// </summary>
        protected virtual void OnCollapsing()
        {
        }

        /// <summary>
        /// Execute derived collapsed handler
        /// </summary>
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
