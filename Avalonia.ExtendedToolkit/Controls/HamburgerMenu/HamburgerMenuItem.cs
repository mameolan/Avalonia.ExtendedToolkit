using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuItem: ListBoxItem
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, string>(nameof(Text));

        public IImage Icon
        {
            get { return (IImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly StyledProperty<IImage> IconProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, IImage>(nameof(Icon));

        public IBrush SelectionIndicatorColor
        {
            get { return (IBrush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        public static readonly StyledProperty<IBrush> SelectionIndicatorColorProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, IBrush>(nameof(SelectionIndicatorColor));

        public ICommand SelectionCommand
        {
            get { return (ICommand)GetValue(SelectionCommandProperty); }
            set { SetValue(SelectionCommandProperty, value); }
        }

        public static readonly StyledProperty<ICommand> SelectionCommandProperty =
            AvaloniaProperty.Register<HamburgerMenuItem, ICommand>(nameof(SelectionCommand));

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);
        }
    }
}