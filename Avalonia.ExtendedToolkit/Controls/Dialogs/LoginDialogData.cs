using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.Dialogs
{
    public class LoginDialogData
    {
        public SecureString SecurePassword { get; internal set; }

        public bool ShouldRemember { get; internal set; }
    }
}
