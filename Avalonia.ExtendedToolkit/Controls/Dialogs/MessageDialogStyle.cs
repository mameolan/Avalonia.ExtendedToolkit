using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.Dialogs
{
    /// <summary>
    /// An enum representing the different button states for a Message Dialog.
    /// </summary>
    public enum MessageDialogStyle
    {
        /// <summary>
        /// Just "OK"
        /// </summary>
        Affirmative = 0,
        /// <summary>
        /// "OK" and "Cancel"
        /// </summary>
        AffirmativeAndNegative = 1,
        AffirmativeAndNegativeAndSingleAuxiliary = 2,
        AffirmativeAndNegativeAndDoubleAuxiliary = 3
    }
}
