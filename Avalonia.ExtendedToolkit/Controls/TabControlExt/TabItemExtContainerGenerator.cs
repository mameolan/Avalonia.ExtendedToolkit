//using System;
//using Avalonia.Controls;
//using Avalonia.Controls.Generators;
//using Avalonia.Controls.Primitives;
//using Avalonia.Data;
//.orig
//namespace Avalonia.ExtendedToolkit.Controls
//{
//    public class TabItemExtContainerGenerator: ItemContainerGenerator<TabItemExt>
//    {
//         public TabItemExtContainerGenerator(TabControlExt owner)
//            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
//        {
//            Owner = owner;
//        }

//        public new TabControlExt Owner { get; }

//        protected override Control CreateContainer(object item)
//        {
//            var tabItem = (TabItemExt)base.CreateContainer(item);

//            tabItem[~TabControl.TabStripPlacementProperty] = Owner[~TabControl.TabStripPlacementProperty];


//            SetupBinding(tabItem);



//            if (tabItem.HeaderTemplate == null)
//            {
//                tabItem[~HeaderedContentControl.HeaderTemplateProperty] = Owner[~ItemsControl.ItemTemplateProperty];
//            }

//            if (tabItem.Header == null)
//            {
//                if (item is Headered headered)
//                {
//                    tabItem.Header = headered.Header;
//                }
//                else
//                {
//                    if (!(tabItem.DataContext is Control))
//                    {
//                        tabItem.Header = tabItem.DataContext;
//                    }
//                }
//            }

//            if (!(tabItem.Content is Control))
//            {
//                tabItem[~ContentControl.ContentTemplateProperty] = Owner[~TabControl.ContentTemplateProperty];
//            }

//            return tabItem;
//        }

//        private void SetupBinding(TabItemExt tabItem)
//        {
//            //todo add binding
//            Binding binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControl.BackgroundProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.BackgroundProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.HeaderFontFamilyProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.HeaderFontFamilyProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.HeaderFontSizeProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.HeaderFontSizeProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.HeaderFontWeightProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.HeaderFontWeightProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.UnderlineBrushProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.UnderlineBrushProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.UnderlineMouseOverBrushProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.UnderlineMouseOverBrushProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.UnderlineMouseOverSelectedBrushProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.UnderlineMouseOverSelectedBrushProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.UnderlinePlacementProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.UnderlinePlacementProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.UnderlineSelectedBrushProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.UnderlineSelectedBrushProperty,binding);

//            binding=new Binding();
//            binding.Source=Owner;
//            binding.Path=TabControlExt.UnderlinedProperty.Name;
//            binding.Mode=BindingMode.OneWay;
//            tabItem.Bind(TabItemExt.UnderlinedProperty,binding);

//        }
//    }
//}
