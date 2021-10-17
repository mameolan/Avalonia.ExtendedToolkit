using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// interface for the File, Folder Dialogs
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// shows the opendirectory dialog
        /// </summary>
        Task<string> OpenDirectoryDialog(Window parent,
                                        string directory = "",
                                        string title = "Choose a folder");

        /// <summary>
        /// opens the openfile dialog
        /// </summary>
        Task<string[]> OpenFileDialog(Window parent,
                              string initialFileName = "",
                              string baseDirectory = "",
                              List<FileDialogFilter> filters = null,
                              bool allowMultiple = true,
                              string title = "Choose a file");

        /// <summary>
        /// opens the opensave dialog
        /// </summary>
        Task<string> OpenSaveFileDialog(Window parent,
                               string defaultExtension = "",
                               string initialFileName = "",
                               string baseDirectory = "",
                               List<FileDialogFilter> filters = null,
                               string title = "Save file");
    }
}
