using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class HamburgerMenu
    {
        private ControlTemplate _defaultItemFocusVisualTemplate = null;



        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }


        public static readonly StyledProperty<double> OpenPaneLengthProperty =
            AvaloniaProperty.Register<HamburgerMenu, double>(nameof(OpenPaneLength), defaultValue: 240.0);



        public SplitViewPanePlacement PanePlacement
        {
            get { return (SplitViewPanePlacement)GetValue(PanePlacementProperty); }
            set { SetValue(PanePlacementProperty, value); }
        }


        public static readonly StyledProperty<SplitViewPanePlacement> PanePlacementProperty =
            AvaloniaProperty.Register<HamburgerMenu, SplitViewPanePlacement>(nameof(PanePlacement), defaultValue: SplitViewPanePlacement.Left);



        public SplitViewDisplayMode DisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }


        public static readonly StyledProperty<SplitViewDisplayMode> DisplayModeProperty =
            AvaloniaProperty.Register<HamburgerMenu, SplitViewDisplayMode>(nameof(DisplayMode), defaultValue: SplitViewDisplayMode.CompactInline);




        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }


        public static readonly StyledProperty<double> CompactPaneLengthProperty =
            AvaloniaProperty.Register<HamburgerMenu, double>(nameof(CompactPaneLength), defaultValue: 48.0);



        public Thickness PaneMargin
        {
            get { return (Thickness)GetValue(PaneMarginProperty); }
            set { SetValue(PaneMarginProperty, value); }
        }


        public static readonly StyledProperty<Thickness> PaneMarginProperty =
            AvaloniaProperty.Register<HamburgerMenu, Thickness>(nameof(PaneMargin));



        public Thickness PaneHeaderMargin
        {
            get { return (Thickness)GetValue(PaneHeaderMarginProperty); }
            set { SetValue(PaneHeaderMarginProperty, value); }
        }


        public static readonly StyledProperty<Thickness> PaneHeaderMarginProperty =
            AvaloniaProperty.Register<HamburgerMenu, Thickness>(nameof(PaneHeaderMargin));



        public IBrush PaneBackground
        {
            get { return (IBrush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }


        public static readonly StyledProperty<IBrush> PaneBackgroundProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(PaneBackground));



        public IBrush PaneForeground
        {
            get { return (IBrush)GetValue(PaneForegroundProperty); }
            set { SetValue(PaneForegroundProperty, value); }
        }


        public static readonly StyledProperty<IBrush> PaneForegroundProperty =
            AvaloniaProperty.Register<HamburgerMenu, IBrush>(nameof(PaneForeground));



        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }


        public static readonly StyledProperty<bool> IsPaneOpenProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(IsPaneOpen));




        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public static readonly StyledProperty<object> ItemsSourceProperty =
            AvaloniaProperty.Register<HamburgerMenu, object>(nameof(ItemsSource));



        public IStyle ItemContainerStyle
        {
            get { return (IStyle)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }


        public static readonly StyledProperty<IStyle> ItemContainerStyleProperty =
            AvaloniaProperty.Register<HamburgerMenu, IStyle>(nameof(ItemContainerStyle));



        public IStyle SeparatorItemContainerStyle
        {
            get { return (IStyle)GetValue(SeparatorItemContainerStyleProperty); }
            set { SetValue(SeparatorItemContainerStyleProperty, value); }
        }


        public static readonly StyledProperty<IStyle> SeparatorItemContainerStyleProperty =
            AvaloniaProperty.Register<HamburgerMenu, IStyle>(nameof(SeparatorItemContainerStyle));




        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }


        public static readonly StyledProperty<DataTemplate> ItemTemplateProperty =
            AvaloniaProperty.Register<HamburgerMenu, DataTemplate>(nameof(ItemTemplate));

        //ItemTemplateSelector missing




        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }


        public static readonly StyledProperty<object> SelectedItemProperty =
            AvaloniaProperty.Register<HamburgerMenu, object>(nameof(SelectedItem), defaultBindingMode: Data.BindingMode.TwoWay);




        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }


        public static readonly StyledProperty<int> SelectedIndexProperty =
            AvaloniaProperty.Register<HamburgerMenu, int>(nameof(SelectedIndex), defaultValue:-1, defaultBindingMode: Data.BindingMode.TwoWay);




        public TransitionType ContentTransition
        {
            get { return (TransitionType)GetValue(ContentTransitionProperty); }
            set { SetValue(ContentTransitionProperty, value); }
        }


        public static readonly StyledProperty<TransitionType> ContentTransitionProperty =
            AvaloniaProperty.Register<HamburgerMenu, TransitionType>(nameof(ContentTransition), defaultValue: TransitionType.Normal);



        public ICommand ItemCommand
        {
            get { return (ICommand)GetValue(ItemCommandProperty); }
            set { SetValue(ItemCommandProperty, value); }
        }


        public static readonly StyledProperty<ICommand> ItemCommandProperty =
            AvaloniaProperty.Register<HamburgerMenu, ICommand>(nameof(ItemCommand));



        public object ItemCommandParameter
        {
            get { return (object)GetValue(ItemCommandParameterProperty); }
            set { SetValue(ItemCommandParameterProperty, value); }
        }


        public static readonly StyledProperty<object> ItemCommandParameterProperty =
            AvaloniaProperty.Register<HamburgerMenu, object>(nameof(ItemCommandParameter));



        public bool VerticalScrollBarOnLeftSide
        {
            get { return (bool)GetValue(VerticalScrollBarOnLeftSideProperty); }
            set { SetValue(VerticalScrollBarOnLeftSideProperty, value); }
        }


        public static readonly StyledProperty<bool> VerticalScrollBarOnLeftSideProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(VerticalScrollBarOnLeftSide));



        public bool ShowSelectionIndicator
        {
            get { return (bool)GetValue(ShowSelectionIndicatorProperty); }
            set { SetValue(ShowSelectionIndicatorProperty, value); }
        }


        public static readonly StyledProperty<bool> ShowSelectionIndicatorProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(ShowSelectionIndicator));



        public IStyle ItemFocusVisualStyle
        {
            get { return (IStyle)GetValue(ItemFocusVisualStyleProperty); }
            set { SetValue(ItemFocusVisualStyleProperty, value); }
        }


        public static readonly StyledProperty<IStyle> ItemFocusVisualStyleProperty =
            AvaloniaProperty.Register<HamburgerMenu, IStyle>(nameof(ItemFocusVisualStyle));

        /// <summary>
        /// Gets the collection used to generate the content of the items list.
        /// </summary>
        /// <exception cref="Exception">
        /// Exception thrown if ButtonsListView is not yet defined.
        /// </exception>
        public IEnumerable Items
        {
            get
            {
                if (_buttonsListView == null)
                {
                    throw new Exception("ButtonsListView is not defined yet. Please use ItemsSource instead.");
                }

                return _buttonsListView.Items;
            }
        }

        /// <summary>
        /// Executes the item command which can be set by the user.
        /// </summary>
        public void RaiseItemCommand()
        {
            var command = ItemCommand;
            var commandParameter = ItemCommandParameter ?? this;
            if (command != null && command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }
        }

        private void ChangeItemFocusVisualStyle()
        {
            
            _defaultItemFocusVisualTemplate = _defaultItemFocusVisualTemplate ?? this.FindResource("MahApps.Templates.HamburgerMenuItem.FocusVisual") as ControlTemplate;
            if (_defaultItemFocusVisualTemplate != null)
            {
                var focusVisualStyle = new Style();
                focusVisualStyle.Setters.Add(new Setter(TemplatedControl.TemplateProperty, _defaultItemFocusVisualTemplate));
                focusVisualStyle.Setters.Add(new Setter(Control.WidthProperty, IsPaneOpen ? OpenPaneLength : CompactPaneLength));
                focusVisualStyle.Setters.Add(new Setter(Control.HorizontalAlignmentProperty, Layout.HorizontalAlignment.Left));
                //focusVisualStyle.Seal();

                SetValue(ItemFocusVisualStyleProperty, focusVisualStyle);
            }
        }



    }
}
