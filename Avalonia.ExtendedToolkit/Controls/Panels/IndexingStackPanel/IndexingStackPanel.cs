using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;

namespace Avalonia.ExtendedToolkit.Controls.Panels
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// stackpannel with indexing functionality
    /// </summary>
    public class IndexingStackPanel : StackPanel
    {
        /// <summary>
        /// AttachedProperty Index
        /// </summary>
        public static readonly AttachedProperty<int> IndexProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, int>("Index",
                typeof(IndexingStackPanel), defaultValue: default(int));

        /// <summary>
        /// get Index
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static int GetIndex(IndexingStackPanel element)
        {
            return element.GetValue(IndexProperty);
        }

        /// <summary>
        /// set Index
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIndex(IndexingStackPanel element, int value)
        {
            element.SetValue(IndexProperty, value);
        }

        /// <summary>
        /// SelectionLocation AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<SelectionLocation> SelectionLocationProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, SelectionLocation>
            ("SelectionLocation", typeof(IndexingStackPanel), defaultValue: default(SelectionLocation));

        /// <summary>
        /// get SelectionLocation
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static SelectionLocation GetSelectionLocation(IndexingStackPanel element)
        {
            return element.GetValue(SelectionLocationProperty);
        }

        /// <summary>
        /// set SelectionLocation
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSelectionLocation(IndexingStackPanel element, SelectionLocation value)
        {
            element.SetValue(SelectionLocationProperty, value);
        }

        /// <summary>
        /// StackLocation AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<StackLocation> StackLocationProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, StackLocation>("StackLocation"
                , typeof(IndexingStackPanel));

        /// <summary>
        /// get StackLocation
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static StackLocation GetStackLocation(IndexingStackPanel element)
        {
            return element.GetValue(StackLocationProperty);
        }

        /// <summary>
        /// set StackLocation
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetStackLocation(IndexingStackPanel element, StackLocation value)
        {
            element.SetValue(StackLocationProperty, value);
        }

        /// <summary>
        /// IndexOddEven AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IndexOddEven> IndexOddEvenProperty =
            AvaloniaProperty.RegisterAttached<IndexingStackPanel, IndexOddEven>
            ("IndexOddEven", typeof(IndexingStackPanel));

        /// <summary>
        /// get IndexOddEven
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IndexOddEven GetIndexOddEven(IndexingStackPanel element)
        {
            return element.GetValue(IndexOddEvenProperty);
        }

        /// <summary>
        /// set  IndexOddEven
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIndexOddEven(IndexingStackPanel element, IndexOddEven value)
        {
            element.SetValue(IndexOddEvenProperty, value);
        }

        /// <summary>
        /// measueres the controls
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            int index = 0;
            bool isEven = true;
            bool foundSelected = false;

            foreach (Control element in this.Children)
            {
                //if (this.IsItemsHost)
                //{
                SelectingItemsControl SelectorParent = this.TemplatedParent as SelectingItemsControl;
                var generator = SelectorParent?.ItemContainerGenerator as ItemContainerGenerator;
                if (SelectorParent != null && SelectorParent.SelectedItem is Control && generator != null)
                {
#warning is this correct?
                    //UIElement selectedElement = (SelectorParent.ItemContainerGenerator.ContainerFromItem(SelectorParent.SelectedItem) as UIElement);

                    var indexContainer = generator.IndexFromContainer(SelectorParent.SelectedItem as Control);

                    Control selectedElement = generator.ContainerFromIndex(indexContainer);

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
