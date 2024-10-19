using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;

namespace Avalonia.Controlz.Controls
{
    /// <summary>
    /// status bar 
    /// </summary>
    public class StatusBar : ItemsControl
    {

#warning finish implementation
        private static readonly FuncTemplate<Panel> DefaultPanel =
           new FuncTemplate<Panel>(() => new DockPanel());

        static StatusBar()
        {
            ItemsPanelProperty.OverrideDefaultValue<StatusBar>(DefaultPanel);
        }

        //public bool UsesItemContainerTemplate
        //{
        //    get { return (bool)GetValue(UsesItemContainerTemplateProperty); }
        //    set { SetValue(UsesItemContainerTemplateProperty, value); }
        //}

        //public static readonly StyledProperty<bool> UsesItemContainerTemplateProperty =
        //    AvaloniaProperty.Register<StatusBar, bool>(nameof(UsesItemContainerTemplate));

        /// <summary>
        /// creates an item container of an <see cref="StatusBarItem"/>
        /// </summary>
        /// <returns></returns>
        //.orig
        //protected override ItemContainerGenerator CreateItemContainerGenerator()
        //{
        //    var itemContainer = new ItemContainerGenerator<StatusBarItem>(
        //       this,
        //       StatusBarItem.ContentProperty,
        //       StatusBarItem.ContentTemplateProperty);

        //    return itemContainer;
        //}
    }
}
