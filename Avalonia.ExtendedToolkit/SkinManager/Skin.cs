using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit
{
    public class Skin
    {
        public SkinType SkinType { get; private set; } = SkinType.OfficeBlack;

        public string Name { get; private set; }

        public IStyle SkinStyle { get; set; }

        public Skin(SkinType skin, IStyle style)
        {
            SkinType = skin;
            Name = skin.ToString();
            SkinStyle = style;
        }
    }
}