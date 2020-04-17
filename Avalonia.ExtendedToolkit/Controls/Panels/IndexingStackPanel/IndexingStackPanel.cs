using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System.Linq;
using Avalonia.Controls.Generators;

namespace Avalonia.ExtendedToolkit.Controls.Panels
{
    public class IndexingStackPanel : StackPanel
    {


        public static readonly AttachedProperty<int> IndexProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, int>("Index", 
                typeof(IndexingStackPanel), defaultValue:default(int));

        public static int GetIndex(IndexingStackPanel element)
        {
            return element.GetValue(IndexProperty);
        }

        public static void SetIndex(IndexingStackPanel element, int value)
        {
            element.SetValue(IndexProperty, value);
        }



        public static readonly AttachedProperty<SelectionLocation> SelectionLocationProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, SelectionLocation>
            ("SelectionLocation", typeof(IndexingStackPanel), defaultValue: default(SelectionLocation));

        public static SelectionLocation GetSelectionLocation(IndexingStackPanel element)
        {
            return element.GetValue(SelectionLocationProperty);
        }

        public static void SetSelectionLocation(IndexingStackPanel element, SelectionLocation value)
        {
            element.SetValue(SelectionLocationProperty, value);
        }



        public static readonly AttachedProperty<StackLocation> StackLocationProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, StackLocation>("StackLocation"
                , typeof(IndexingStackPanel));

        public static StackLocation GetStackLocation(IndexingStackPanel element)
        {
            return element.GetValue(StackLocationProperty);
        }

        public static void SetStackLocation(IndexingStackPanel element, StackLocation value)
        {
            element.SetValue(StackLocationProperty, value);
        }



        public static readonly AttachedProperty<IndexOddEven> IndexOddEvenProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel,  IndexOddEven>
            ("IndexOddEven", typeof(IndexingStackPanel));

        public static IndexOddEven GetIndexOddEven(IndexingStackPanel element)
        {
            return element.GetValue(IndexOddEvenProperty);
        }

        public static void SetIndexOddEven(IndexingStackPanel element, IndexOddEven value)
        {
            element.SetValue(IndexOddEvenProperty, value);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            int index = 0;
            bool isEven = true;
            bool foundSelected = false;

            foreach (IControl element in this.Children)
            {

                //if (this.IsItemsHost)
                //{
                SelectingItemsControl SelectorParent = this.TemplatedParent as SelectingItemsControl;
                var generator = SelectorParent?.ItemContainerGenerator as ItemContainerGenerator;
                if (SelectorParent != null&& SelectorParent.SelectedItem is IControl&& generator!=null)
                {
#warning is this correct?
                    //UIElement selectedElement = (SelectorParent.ItemContainerGenerator.ContainerFromItem(SelectorParent.SelectedItem) as UIElement);

                    var indexContainer = generator.IndexFromContainer(SelectorParent.SelectedItem as IControl);

                    IControl selectedElement = generator.ContainerFromIndex(indexContainer);

                    if (selectedElement != null)
                    {
                        if (element == selectedElement)
                        {
                            element.SetValue(SelectionLocationProperty, SelectionLocation.Selected);
                            foundSelected = true;
                        }
                        else if (foundSelected)
                        {
                            element.SetValue(SelectionLocationProperty, SelectionLocation.After);
                        }
                        else
                        {
                            element.SetValue(SelectionLocationProperty, SelectionLocation.Before);
                        }
                    }
                }
                //}

                // StackLocation

                if (Children.Count - 1 == 0)
                {
                    element.SetValue(StackLocationProperty, StackLocation.FirstAndLast);
                }
                else if (index == 0)
                {
                    element.SetValue(StackLocationProperty, StackLocation.First);
                }
                else if (index == Children.Count - 1)
                {
                    element.SetValue(StackLocationProperty, StackLocation.Last);
                }
                else
                {
                    element.SetValue(StackLocationProperty, StackLocation.Middle);
                }

                // IndexOddEven

                if (isEven)
                {
                    element.SetValue(IndexOddEvenProperty, IndexOddEven.Even);
                }
                else
                {
                    element.SetValue(IndexOddEvenProperty, IndexOddEven.Odd);
                }

                element.SetValue(IndexProperty, index);
                index++;

            }

            return base.MeasureOverride(constraint);
        }





    }
}
