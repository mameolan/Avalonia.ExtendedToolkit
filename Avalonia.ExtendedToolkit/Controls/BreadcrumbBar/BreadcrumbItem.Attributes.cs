using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media.Imaging;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// A breadcrumb item that is part of a BreadcrumbBar and
    /// contains a BreadcrumbButton and nested child BreadcrumbItems.
    /// </summary>
    public partial class BreadcrumbItem
    {
        private const string partHeader = "PART_Header";
        private const string partSelected = "PART_Selected";

        private Layoutable headerControl;
        private Layoutable selectedControl;

        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(BreadcrumbItem);

        /// <summary>
        /// Gets or sets wheter the dropdown button is pressed.
        /// </summary>
        public bool IsDropDownPressed
        {
            get { return (bool)GetValue(IsDropDownPressedProperty); }
            set { SetValue(IsDropDownPressedProperty, value); }
        }

        /// <summary>
        /// <see cref="IsDropDownPressed"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsDropDownPressedProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsDropDownPressed));

        /// <summary>
        /// Gets whether the breadcrumb item is overflowed which means it is not
        /// visible in the breadcrumb bar but in the
        /// drop down menu of the breadcrumb bar.
        /// </summary>
        public bool IsOverflow
        {
            get { return (bool)GetValue(IsOverflowProperty); }
            internal set { SetValue(IsOverflowProperty, value); }
        }

        /// <summary>
        /// <see cref="IsOverflow"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsOverflowProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsOverflow));

        /// <summary>
        /// Gets or sets whether the button is visible.
        /// </summary>
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsButtonVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsButtonVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsButtonVisible), defaultValue: true);

        /// <summary>
        /// Gets or sets whether the Image is visible.
        /// </summary>
        public bool IsImageVisible
        {
            get { return (bool)GetValue(IsImageVisibleProperty); }
            set { SetValue(IsImageVisibleProperty, value); }
        }

        /// <summary>
        /// <see cref="IsImageVisible"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsImageVisibleProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsImageVisible));

        /// <summary>
        /// Set to true, to collapse the item if SelectedItem is not null. otherwise false.
        /// </summary>
        public bool IsRoot
        {
            get { return (bool)GetValue(IsRootProperty); }
            set { SetValue(IsRootProperty, value); }
        }

        /// <summary>
        /// <see cref="IsRoot"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsRootProperty =
            AvaloniaProperty.Register<BreadcrumbItem, bool>(nameof(IsRoot));

        /// <summary>
        /// Gets the selected BreadcrumbItem.
        /// </summary>
        public BreadcrumbItem SelectedBreadcrumb
        {
            get { return (BreadcrumbItem)GetValue(SelectedBreadcrumbProperty); }
            internal set { SetValue(SelectedBreadcrumbProperty, value); }
        }

        /// <summary>
        /// <see cref="SelectedBreadcrumb"/>
        /// </summary>
        public static readonly StyledProperty<BreadcrumbItem> SelectedBreadcrumbProperty =
            AvaloniaProperty.Register<BreadcrumbItem, BreadcrumbItem>(nameof(SelectedBreadcrumb));

        /// <summary>
        /// get/sets OverflowItemTemplate
        /// </summary>
        public IDataTemplate OverflowItemTemplate
        {
            get { return (IDataTemplate)GetValue(OverflowItemTemplateProperty); }
            set { SetValue(OverflowItemTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="OverflowItemTemplate"/>
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> OverflowItemTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbItem, IDataTemplate>(nameof(OverflowItemTemplate));

        /// <summary>
        /// Gets or sets the image that is used to display this item.
        /// </summary>
        public Bitmap Image
        {
            get { return (Bitmap)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// <see cref="Image"/>
        /// </summary>
        public static readonly StyledProperty<Bitmap> ImageProperty =
            AvaloniaProperty.Register<BreadcrumbItem, Bitmap>(nameof(Image));

        /// <summary>
        /// Gets or sets the Trace of the breadcrumb
        /// </summary>
        public object Trace
        {
            get { return (object)GetValue(TraceProperty); }
            set { SetValue(TraceProperty, value); }
        }

        /// <summary>
        /// <see cref="Trace"/>
        /// </summary>
        public static readonly StyledProperty<object> TraceProperty =
            AvaloniaProperty.Register<BreadcrumbItem, object>(nameof(Trace));

        /// <summary>
        /// Gets or sets the header for the breadcrumb item.
        /// </summary>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// <see cref="Header"/>
        /// </summary>
        public static readonly StyledProperty<object> HeaderProperty =
            AvaloniaProperty.Register<BreadcrumbItem, object>(nameof(Header));

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        public IDataTemplate HeaderTemplate
        {
            get { return (IDataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderTemplate"/>
        /// 
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> HeaderTemplateProperty =
            AvaloniaProperty.Register<BreadcrumbItem, IDataTemplate>(nameof(HeaderTemplate));

        /// <summary>
        /// Gets the parent BreadcrumbBar container.
        /// </summary>
        public BreadcrumbBar BreadcrumbBar
        {
            get
            {
                ILogical current = this;

                while (current != null)
                {
                    current = LogicalExtensions.GetLogicalParent(current);
                    if (current is BreadcrumbBar)
                        return current as BreadcrumbBar;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the parent BreadcrumbItem, otherwise null.
        /// </summary>
        public BreadcrumbItem ParentBreadcrumbItem
        {
            get
            {
                BreadcrumbItem parent = LogicalExtensions.GetLogicalParent(this) as BreadcrumbItem;
                return parent;
            }
        }

        /// <summary>
        /// returns the DataContext if not null
        /// else this is returned
        /// </summary>
        public object Data
        {
            get
            {
                return DataContext != null ? DataContext : this;
            }
        }

        /// <summary>
        /// DataTemplate of the Breadcrumb item
        /// </summary>
        public DataTemplate BreadcrumbItemTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Binding to the Trace property. This is not a dependency property.
        /// </summary>
        public Binding TraceBinding { get; set; }

        /// <summary>
        /// Gets or sets the Binding to the Image property.  This is not a dependency property.
        /// </summary>
        public Binding ImageBinding { get; set; }

        /// <summary>
        /// Gets the Trace string from the Trace property.
        /// </summary>
        public string TraceValue
        {
            get
            {
                XmlNode xml = Trace as XmlNode;
                if (xml != null)
                {
                    return xml.Value;
                }

                if (Trace != null)
                    return Trace.ToString();
                if (Header != null)
                    return Header.ToString();
                return string.Empty;
            }
        }

        /// <summary>
        /// <see cref="DropDownPressedChanged"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<object>> DropDownPressedChangedEvent =
                    RoutedEvent.Register<BreadcrumbItem, RoutedPropertyChangedEventArgs<object>>(nameof(DropDownPressedChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the IsDropDownPressed property is changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<object> DropDownPressedChanged
        {
            add
            {
                AddHandler(DropDownPressedChangedEvent, value);
            }
            remove
            {
                RemoveHandler(DropDownPressedChangedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="TraceChanged"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedPropertyChangedEventArgs<object>> TraceChangedEvent =
                    RoutedEvent.Register<BreadcrumbItem, RoutedPropertyChangedEventArgs<object>>(nameof(TraceChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// Occurs when the Trace property is changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<object> TraceChanged
        {
            add
            {
                AddHandler(TraceChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TraceChangedEvent, value);
            }
        }

        /// <summary>
        /// <see cref="OverflowChangedEvent"/>
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> OverflowChangedEvent =
                    RoutedEvent.Register<BreadcrumbItem, RoutedEventArgs>(nameof(OverflowChangedEvent), RoutingStrategies.Bubble);

        /// <summary>
        /// occurs when the Overflow property is changed
        /// </summary>
        public event EventHandler OverflowChanged
        {
            add
            {
                AddHandler(OverflowChangedEvent, value);
            }
            remove
            {
                RemoveHandler(OverflowChangedEvent, value);
            }
        }
    }
}
