using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class HamburgerMenu: ContentControl
    {
        private Button _hamburgerButton;
        private ListBox _buttonsListView;
        private ListBox _optionsListView;


        public HamburgerMenu()
        {
            OpenPaneLengthProperty.Changed.AddClassHandler<HamburgerMenu>((o, e) => OpenPaneLengthPropertyChanged(o, e));
            CompactPaneLengthProperty.Changed.AddClassHandler<HamburgerMenu>((o, e) => CompactPaneLengthPropertyChanged(o, e));
            IsPaneOpenProperty.Changed.AddClassHandler<HamburgerMenu>((o, e) => IsPaneOpenChanged(o, e));


        }

        private void IsPaneOpenChanged(HamburgerMenu o, AvaloniaPropertyChangedEventArgs args)
        {
            if (args.NewValue != args.OldValue)
            {
                o.ChangeItemFocusVisualStyle();
            }
        }

        private void CompactPaneLengthPropertyChanged(HamburgerMenu o, AvaloniaPropertyChangedEventArgs args)
        {
            if (args.NewValue != args.OldValue)
            {
                o.ChangeItemFocusVisualStyle();
            }
        }

        private void OpenPaneLengthPropertyChanged(HamburgerMenu o, AvaloniaPropertyChangedEventArgs args)
        {
            if (args.NewValue != args.OldValue)
            {
                o.ChangeItemFocusVisualStyle();
            }
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            if (_hamburgerButton != null)
            {
                _hamburgerButton.Click -= this.OnHamburgerButtonClick;
            }

            if (_buttonsListView != null)
            {
                _buttonsListView.SelectionChanged -= ButtonsListView_SelectionChanged;
            }

            if (_optionsListView != null)
            {
                _optionsListView.SelectionChanged -= OptionsListView_SelectionChanged;
            }

            _hamburgerButton = e.NameScope.Find<Button>("HamburgerButton");
            _buttonsListView = e.NameScope.Find<ListBox>("ButtonsListView");
            _optionsListView = e.NameScope.Find<ListBox>("OptionsListView");

            if (_hamburgerButton != null)
            {
                _hamburgerButton.Click += this.OnHamburgerButtonClick;
            }

            if (_buttonsListView != null)
            {
                _buttonsListView.SelectionChanged += ButtonsListView_SelectionChanged;
            }

            if (_optionsListView != null)
            {
                _optionsListView.SelectionChanged += OptionsListView_SelectionChanged;
            }

            ChangeItemFocusVisualStyle();

            //Loaded -= HamburgerMenu_Loaded;
            //Loaded += HamburgerMenu_Loaded;
            Initialized -= HamburgerMenu_Loaded;
            Initialized += HamburgerMenu_Loaded;



            base.OnTemplateApplied(e);
        }

        private void HamburgerMenu_Loaded(object sender, EventArgs e)
        {
            if (GetValue(ContentProperty) != null)
            {
                return;
            }

            if (RaiseItemEvents(_buttonsListView?.SelectedItem))
            {
                return;
            }

            if (RaiseOptionsItemEvents(_optionsListView?.SelectedItem))
            {
                return;
            }

            var selectedItem = _buttonsListView?.SelectedItem ?? _optionsListView?.SelectedItem;
            if (selectedItem != null)
            {
                SetValue(ContentProperty, selectedItem);
            }
        }
    }
}
