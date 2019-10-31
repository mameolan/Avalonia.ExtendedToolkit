using Avalonia.Markup.Xaml.Templates;
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


        public object OptionsItemsSource
        {
            get { return (object)GetValue(OptionsItemsSourceProperty); }
            set { SetValue(OptionsItemsSourceProperty, value); }
        }


        public static readonly AvaloniaProperty OptionsItemsSourceProperty =
            AvaloniaProperty.Register<HamburgerMenu, object>(nameof(OptionsItemsSource));



        public IStyle OptionsItemContainerStyle
        {
            get { return (IStyle)GetValue(OptionsItemContainerStyleProperty); }
            set { SetValue(OptionsItemContainerStyleProperty, value); }
        }


        public static readonly AvaloniaProperty OptionsItemContainerStyleProperty =
            AvaloniaProperty.Register<HamburgerMenu, IStyle>(nameof(OptionsItemContainerStyle));



        public DataTemplate OptionsItemTemplate
        {
            get { return (DataTemplate)GetValue(OptionsItemTemplateProperty); }
            set { SetValue(OptionsItemTemplateProperty, value); }
        }


        public static readonly AvaloniaProperty OptionsItemTemplateProperty =
            AvaloniaProperty.Register<HamburgerMenu, DataTemplate>(nameof(OptionsItemTemplate));

        //OptionsItemTemplateSelector missing



        public bool OptionsIsVisible
        {
            get { return (bool)GetValue(OptionsIsVisibleProperty); }
            set { SetValue(OptionsIsVisibleProperty, value); }
        }


        public static readonly AvaloniaProperty OptionsIsVisibleProperty =
            AvaloniaProperty.Register<HamburgerMenu, bool>(nameof(OptionsIsVisible), defaultValue: true);



        public object SelectedOptionsItem
        {
            get { return (object)GetValue(SelectedOptionsItemProperty); }
            set { SetValue(SelectedOptionsItemProperty, value); }
        }


        public static readonly AvaloniaProperty SelectedOptionsItemProperty =
            AvaloniaProperty.Register<HamburgerMenu, object>(nameof(SelectedOptionsItem), defaultBindingMode: Data.BindingMode.TwoWay);



        public int SelectedOptionsIndex
        {
            get { return (int)GetValue(SelectedOptionsIndexProperty); }
            set { SetValue(SelectedOptionsIndexProperty, value); }
        }


        public static readonly AvaloniaProperty SelectedOptionsIndexProperty =
            AvaloniaProperty.Register<HamburgerMenu, int>(nameof(SelectedOptionsIndex), defaultValue: -1, defaultBindingMode: Data.BindingMode.TwoWay);



        public ICommand OptionsItemCommand
        {
            get { return (ICommand)GetValue(OptionsItemCommandProperty); }
            set { SetValue(OptionsItemCommandProperty, value); }
        }


        public static readonly AvaloniaProperty OptionsItemCommandProperty =
            AvaloniaProperty.Register<HamburgerMenu, ICommand>(nameof(OptionsItemCommand));



        public object OptionsItemCommandParameter
        {
            get { return (object)GetValue(OptionsItemCommandParameterProperty); }
            set { SetValue(OptionsItemCommandParameterProperty, value); }
        }


        public static readonly AvaloniaProperty OptionsItemCommandParameterProperty =
            AvaloniaProperty.Register<HamburgerMenu, object>(nameof(OptionsItemCommandParameter));

        public IEnumerable OptionsItems
        {
            get
            {
                if (_optionsListView == null)
                {
                    throw new Exception("OptionsListView is not defined yet. Please use OptionsItemsSource instead.");
                }

                return _optionsListView?.Items;
            }
        }

        /// <summary>
        /// Executes the options item command which can be set by the user.
        /// </summary>
        public void RaiseOptionsItemCommand()
        {
            var command = OptionsItemCommand;
            var commandParameter = OptionsItemCommandParameter ?? this;
            if (command != null && command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }
        }


    }
}
