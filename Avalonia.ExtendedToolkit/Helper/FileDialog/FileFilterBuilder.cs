using System.Collections.Generic;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// builder for creating FileDialogFilter
    /// </summary>
    public class FileFilterBuilder
    {
        private List<FileDialogFilter> filters = new List<FileDialogFilter>();
        private static FileFilterBuilder _fileFilterBuilder;

        /// <summary>
        /// sets up the builder
        /// </summary>
        public static FileFilterBuilder Setup()
        {
            if (_fileFilterBuilder == null)
            {
                _fileFilterBuilder = new FileFilterBuilder();
            }
            _fileFilterBuilder.filters.Clear();
            return _fileFilterBuilder;
        }

        /// <summary>
        /// adds all files
        /// </summary>
        /// <returns></returns>
        public FileFilterBuilder WithAllFiles()
        {
            _fileFilterBuilder.filters.Add(FileFilter.AllFileFilter);
            return _fileFilterBuilder;
        }

        /// <summary>
        /// adds image files
        /// </summary>
        /// <returns></returns>
        public FileFilterBuilder WithImageFilter()
        {
            _fileFilterBuilder.filters.Add(FileFilter.ImageFilter);
            return _fileFilterBuilder;
        }

        /// <summary>
        /// adds music filter
        /// </summary>
        /// <returns></returns>
        public FileFilterBuilder WithMusicFilter()
        {
            _fileFilterBuilder.filters.Add(FileFilter.MusicFilter);
            return _fileFilterBuilder;
        }

        /// <summary>
        /// adds video filter
        /// </summary>
        /// <returns></returns>
        public FileFilterBuilder WithVideoFilter()
        {
            _fileFilterBuilder.filters.Add(FileFilter.VideoFilter);
            return _fileFilterBuilder;
        }

        /// <summary>
        /// returns builded filters
        /// </summary>
        /// <returns></returns>
        public List<FileDialogFilter> Build()
        {
            return _fileFilterBuilder.filters;
        }
    }
}
