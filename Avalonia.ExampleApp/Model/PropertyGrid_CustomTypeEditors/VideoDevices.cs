using System.ComponentModel;

namespace Avalonia.ExampleApp.Model
{
    public enum VideoDevices
    {
        UNSPECIFIED,

        [Description("Microsoft VX 6000")]
        MICROSOFT_VX_6000,

        [Description("Logitech Pro 5000")]
        LOGITECH_PRO_5000
    };
}
