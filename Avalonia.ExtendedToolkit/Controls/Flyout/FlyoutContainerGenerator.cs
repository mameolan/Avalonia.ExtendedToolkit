using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class FlyoutContainerGenerator : ItemContainerGenerator<Flyout>
    {
        public FlyoutContainerGenerator(FlyoutsControl owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
        {
            Owner = owner;
        }

        public new FlyoutsControl Owner { get; }

        protected override IControl CreateContainer(object item)
        {
            var flyout = (Flyout)item;

            var headerTemplate = flyout?.HeaderTemplate;

            if (headerTemplate != null)
            {
                flyout.SetValue(HeaderedContentControl.HeaderTemplateProperty, (object)headerTemplate);
            }

            if (ItemTemplate != null && null == flyout.ContentTemplate)
            {
                flyout.SetValue(HeaderedContentControl.ContentTemplateProperty, (object)ItemTemplate);
            }

            //if (flyout.HeaderTemplate == null)
            //{
            //    flyout[~HeaderedContentControl.HeaderTemplateProperty] = Owner[~ItemsControl.ItemTemplateProperty];
            //}

            //if (flyout.Header == null)
            //{
            //    if (item is IHeadered headered)
            //    {
            //        flyout.Header = headered.Header;
            //    }
            //    else
            //    {
            //        if (!(flyout.DataContext is IControl))
            //        {
            //            flyout.Header = flyout.DataContext;
            //        }
            //    }
            //}

            //if (!(flyout.Content is IControl))
            //{
            //    flyout[~ContentControl.ContentTemplateProperty] = Owner[~TabControl.ContentTemplateProperty];
            //}

            Owner.AttachHandlers(flyout);

            return flyout;
        }
    }
}
