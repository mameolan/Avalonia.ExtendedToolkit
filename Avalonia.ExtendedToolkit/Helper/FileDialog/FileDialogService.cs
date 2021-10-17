using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// service for showing the build in file, directory dialogs
    /// </summary>
    public class FileDialogService : IFileDialogService
    {
        private OpenFolderDialog folderDialog;
        private OpenFileDialog fileDialog;

        private SaveFileDialog saveFileDialog;

        /// <inheritdoc/>
        public Task<string> OpenDirectoryDialog(Window parent, string directory = "", string title = "Choose a folder")
        {
            if (folderDialog == null)
            {
                folderDialog = new OpenFolderDialog();
            }

            folderDialog.Directory = string.IsNullOrEmpty(directory) ?
                                Directory.GetCurrentDirectory() : directory;
            folderDialog.Title = title;
            return folderDialog.ShowAsync(parent);
        }

        /// <inheritdoc/>
        public Task<string[]> OpenFileDialog(Window parent,
                                        string initialFileName = "",
                                        string baseDirectory = "",
                                        List<FileDialogFilter> filters = null,
                                        bool allowMultiple = true,
                                        string title = "Choose a file")
        {
            if (fileDialog == null)
            {
                fileDialog = new OpenFileDialog();
            }

            fileDialog.Title = title;
            fileDialog.InitialFileName = initialFileName;
            fileDialog.Directory = string.IsNullOrEmpty(baseDirectory) ?
                                  Directory.GetCurrentDirectory() : baseDirectory;
            fileDialog.AllowMultiple = allowMultiple;

            fileDialog.Filters = filters == null ?
                                        FileFilterBuilder.Setup().
                                            WithAllFiles().Build()
                                        : filters;

            return fileDialog.ShowAsync(parent);
        }

        /// <inheritdoc/>
        public Task<string> OpenSaveFileDialog(Window parent,
                                              string defaultExtension = "",
                                              string initialFileName = "",
                                              string baseDirectory = "",
                                              List<FileDialogFilter> filters = null,
                                              string title = "Save file")
        {
            if (saveFileDialog == null)
            {
                saveFileDialog = new SaveFileDialog();
            }

            saveFileDialog.DefaultExtension = defaultExtension;
            saveFileDialog.InitialFileName = initialFileName;
            saveFileDialog.Directory = string.IsNullOrEmpty(baseDirectory) ?
                                  Directory.GetCurrentDirectory() : baseDirectory;
            saveFileDialog.Filters = filters == null ?
                                        FileFilterBuilder.Setup().
                                            WithAllFiles().Build()
                                        : filters;
            saveFileDialog.Title = title;
            return saveFileDialog.ShowAsync(parent);
        }
    }
}
