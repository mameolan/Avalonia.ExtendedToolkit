//using Avalonia.Controls;
//using Avalonia.Controls.Generators;
//using Avalonia.Controls.Primitives;
//.orig
//namespace Avalonia.ExtendedToolkit.Controls
//{
//    /// <summary>
//    /// FlyoutContainerGenerator
//    /// </summary>
//    public class FlyoutContainerGenerator : ItemContainerGenerator
//    {
//        /// <summary>
//        /// sets the owner
//        /// </summary>
//        /// <param name="owner"></param>
//        public FlyoutContainerGenerator(FlyoutsControl owner)
//            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
//        {
//            Owner = owner;
//        }

//        /// <summary>
//        /// gets FlyoutsControl
//        /// </summary>
//        public new FlyoutsControl Owner { get; }

//        /// <summary>
//        /// sets the item and attach the handlers
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        //.orig
//        //protected override Control CreateContainer(object item)
//        //{
//        //    var flyout = (Flyout)item;

//        //    var headerTemplate = flyout?.HeaderTemplate;

//        //    if (headerTemplate != null)
//        //    {
//        //        flyout.SetValue(HeaderedContentControl.HeaderTemplateProperty, (object)headerTemplate);
//        //    }

//        //    if (ItemTemplate != null && null == flyout.ContentTemplate)
//        //    {
//        //        flyout.SetValue(HeaderedContentControl.ContentTemplateProperty, (object)ItemTemplate);
//        //    }

//        //    //if (flyout.HeaderTemplate == null)
//        //    //{
//        //    //    flyout[~HeaderedContentControl.HeaderTemplateProperty] = Owner[~ItemsControl.ItemTemplateProperty];
//        //    //}

//        //    //if (flyout.Header == null)
//        //    //{
//        //    //    if (item is IHeadered headered)
//        //    //    {
//        //    //        flyout.Header = headered.Header;
//        //    //    }
//        //    //    else
//        //    //    {
//        //    //        if (!(flyout.DataContext is IControl))
//        //    //        {
//        //    //            flyout.Header = flyout.DataContext;
//        //    //        }
//        //    //    }
//        //    //}

//        //    //if (!(flyout.Content is IControl))
//        //    //{
//        //    //    flyout[~ContentControl.ContentTemplateProperty] = Owner[~TabControl.ContentTemplateProperty];
//        //    //}

//        //    Owner.AttachHandlers(flyout);

//        //    return flyout;
//        //}
//    }
//}
