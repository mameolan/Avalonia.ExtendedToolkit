using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ContextMenuExt : ContextMenu
    {
        public Type StyleKey => typeof(ContextMenu);

        private Popup _popup;

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<ContentControlEx, bool>(nameof(IsOpen));

        public new void Open(Control control)
        {
            if (IsOpen)
            {
                return;
            }

            if (_popup == null)
            {
                _popup = new Popup
                {
                    PlacementMode = PlacementMode.Pointer,
                    PlacementTarget = control,
                    StaysOpen = false,
                    ObeyScreenEdges = true
                };

                _popup.Opened += PopupOpened;
                _popup.Closed += PopupClosed;
            }

            ((ISetLogicalParent)_popup).SetParent(null);
            _popup.SetValue(Popup.ChildProperty, null);

            ((ISetLogicalParent)_popup).SetParent(control);

            _popup.Child = this;
            _popup.IsOpen = true;

            IsOpen = true;

            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = MenuOpenedEvent,
                Source = this,
            });
        }

        private void PopupOpened(object sender, EventArgs e)
        {
            Focus();
        }

        private void PopupClosed(object sender, EventArgs e)
        {
            var contextMenu = (sender as Popup)?.Child as ContextMenuExt;

            if (contextMenu != null)
            {
                foreach (var i in contextMenu.GetLogicalChildren().OfType<MenuItem>())
                {
                    i.IsSubMenuOpen = false;
                }

                contextMenu.CloseCore();
            }
        }

        private void CloseCore()
        {
            SelectedIndex = -1;
            IsOpen = false;

            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = MenuClosedEvent,
                Source = this,
            });
        }
    }
}
