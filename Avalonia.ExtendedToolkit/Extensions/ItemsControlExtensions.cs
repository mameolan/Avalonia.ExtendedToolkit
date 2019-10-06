using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace Avalonia.ExtendedToolkit.Extensions
{
    public static class ItemsControlExtensions
    {
        public static ItemsControl ItemsControlFromItemContainer(AvaloniaObject container)
        {
            ILogical uiLogical = container as ILogical;
            if (uiLogical == null)
                return null;

            // ui appeared in items collection
            ItemsControl ic = LogicalExtensions.GetLogicalParent(uiLogical) as ItemsControl;
            if (ic != null)
            {
                // this is the right ItemsControl as long as the item
                // is (or is eligible to be) its own container
                //IGeneratorHost host = ic as IGeneratorHost;
                //if (host.IsItemItsOwnContainer(ui))
                //    return ic;
                //else
                //    return null;
            }

            IVisual uiVisual = container as IVisual;

            uiVisual = VisualTree.VisualExtensions.GetVisualParent<IVisual>(uiVisual);

            return GetItemsOwner(uiLogical as AvaloniaObject);
        }

        internal static ItemsPresenter FromPanel(Panel panel)
        {
            if (panel == null)
                return null;

            return panel.TemplatedParent as ItemsPresenter;
        }

        public static ItemsControl GetItemsOwner(AvaloniaObject element)
        {
            ItemsControl container = null;
            Panel panel = element as Panel;

            if (panel != null /*&& panel.Isi*/)
            {
                // see if element was generated for an ItemsPresenter
                ItemsPresenter ip = FromPanel(panel);

                if (ip != null)
                {
                    // if so use the element whose style begat the ItemsPresenter
                    container = ip.Parent as ItemsControl;
                }
                else
                {
                    // otherwise use element's templated parent
                    container = panel.TemplatedParent as ItemsControl;
                }
            }

            return container;
        }
    }
}