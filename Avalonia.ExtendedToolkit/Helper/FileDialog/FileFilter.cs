using System.Collections.Generic;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// file filters
    /// </summary>
    public class FileFilter
    {
        /// <summary>
        /// all files
        /// </summary>
        public static readonly string AllFiles_Extension = "*";

        /// <summary>
        /// jpeg extension
        /// </summary>
        public static readonly string JPEG_Extension = "jpg";

        /// <summary>
        /// png extension
        /// </summary>
        public static readonly string PNG_Extension = "png";

        /// <summary>
        /// bmp extension
        /// </summary>
        public static readonly string BMP_Extension = "bmp";

        /// <summary>
        /// wav extension
        /// </summary>
        public static readonly string WAV_Extension = "wav";

        /// <summary>
        /// mp3 extension
        /// </summary>
        public static readonly string MP3_Extension = "mp3";

        /// <summary>
        /// wma extension
        /// </summary>
        public static readonly string WMA_Extension = "wma";

        /// <summary>
        /// ogg extension
        /// </summary>
        public static readonly string OGG_Extension = "ogg";

        /// <summary>
        /// mpeg extension
        /// </summary>
        public static readonly string MPEG_Extension = "mpeg";

        /// <summary>
        /// mp4 extension
        /// </summary>
        public static readonly string MP4_Extension = "mp4";

        /// <summary>
        /// FileDialogFilter all files
        /// </summary>
        public static FileDialogFilter AllFileFilter = new FileDialogFilter
        {
            Name = "All Files (*.*)",
            Extensions = new List<string> { AllFiles_Extension }
        };

        /// <summary>
        /// FileDialogFilter image files
        /// </summary>
        public static FileDialogFilter ImageFilter = new FileDialogFilter
        {
            Name = "Image Files (*.jpg, *.png, *.bmp)",
            Extensions = new List<string> { JPEG_Extension, PNG_Extension, BMP_Extension }
        };

        /// <summary>
        /// FileDialogFilter music files
        /// </summary>
        public static FileDialogFilter MusicFilter = new FileDialogFilter
        {
            Name = "Image Files (*.wav, *.mp3, *.wma, *.ogg)",
            Extensions = new List<string> { WAV_Extension, MP3_Extension, WMA_Extension, OGG_Extension }
        };

        /// <summary>
        /// FileDialogFilter video files
        /// </summary>
        public static FileDialogFilter VideoFilter = new FileDialogFilter
        {
            Name = "Image Files (*.mpeg, *.mp4, *.ogg)",
            Extensions = new List<string> { MPEG_Extension, MP4_Extension, OGG_Extension }
        };
    }
}
