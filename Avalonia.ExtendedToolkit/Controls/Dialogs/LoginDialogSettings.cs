using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.Dialogs
{
    public class LoginDialogSettings : MetroDialogSettings
    {
        private const string DefaultUsernameWatermark = "Username...";
        private const string DefaultPasswordWatermark = "Password...";
        private const string DefaultRememberCheckBoxText = "Remember";

        public LoginDialogSettings()
        {
            this.UsernameWatermark = DefaultUsernameWatermark;
            this.UsernameCharacterCasing = CharacterCasing.Normal;
            this.PasswordWatermark = DefaultPasswordWatermark;
            this.ShouldHideUsername = false;
            this.AffirmativeButtonText = "Login";
            this.EnablePasswordPreview = false;
            this.RememberCheckBoxText = DefaultRememberCheckBoxText;
            this.RememberCheckBoxChecked = false;
        }

        public string InitialUsername { get; set; }

        public string InitialPassword { get; set; }

        public string UsernameWatermark { get; set; }

        public CharacterCasing UsernameCharacterCasing { get; set; }

        public bool ShouldHideUsername { get; set; }

        public string PasswordWatermark { get; set; }

        public bool IsNegativeButtonVisible { get; set; }

        public bool EnablePasswordPreview { get; set; }

        public bool IsRememberCheckBoxVisible { get; set; }

        public string RememberCheckBoxText { get; set; }

        public bool RememberCheckBoxChecked { get; set; }
    }
}
