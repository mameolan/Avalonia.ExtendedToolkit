using Avalonia.Interactivity;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// event if the DragFinished is execute on the <see cref="RotateThumb"/>
    /// </summary>
    public class PositionChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// left position
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// top position
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// right position
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// bottom position
        /// </summary>
        /// <value></value>
        public double Bottom { get; set; }


        public double Width {get{return Right-Left;}}

        public double Height {get{return Bottom-Top;}}

        public override string ToString()
        {
            return $"position left: {Left} top: {Top} right: {Right} botton: {Bottom} Width: {Width} Height: {Height}";
        }
        



    }
}
