using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// represents a skin entry
    /// </summary>
    public class Skin
    {
        /// <summary>
        /// typeof the skin
        /// </summary>
        public SkinType SkinType { get; private set; } = SkinType.OfficeBlack;

        /// <summary>
        /// skin name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// IStyle representiv of the skin
        /// </summary>
        public IStyle SkinStyle { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="skin"></param>
        /// <param name="style"></param>
        public Skin(SkinType skin, IStyle style)
        {
            SkinType = skin;
            Name = skin.ToString();
            SkinStyle = style;
        }
    }
}