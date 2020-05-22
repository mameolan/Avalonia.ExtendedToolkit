using System;
using System.Collections;
using System.ComponentModel;
using System.Numerics;
using Avalonia.Input;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Frequently used types cache used performance optimization.
    /// </summary>
    public static class KnownTypes
    {
        /// <summary>
        /// collection types
        /// </summary>
        public static class Collections
        {
            /// <summary>
            /// ilst type
            /// </summary>
            public static readonly Type IList = typeof(IList);
        }

        /// <summary>
        /// Attribute types
        /// </summary>
        public static class Attributes
        {
            /// <summary>
            /// EditorBrowsableAttribute Type
            /// </summary>
            public static readonly Type EditorBrowsableAttribute = typeof(EditorBrowsableAttribute);
            /// <summary>
            /// MergablePropertyAttribute Type
            /// </summary>
            public static readonly Type MergablePropertyAttribute = typeof(MergablePropertyAttribute);
            /// <summary>
            /// PropertyEditorAttribute Type
            /// </summary>
            public static readonly Type PropertyEditorAttribute = typeof(PropertyEditorAttribute);
            /// <summary>
            /// CategoryEditorAttribute Type
            /// </summary>
            public static readonly Type CategoryEditorAttribute = typeof(CategoryEditorAttribute);
            /// <summary>
            /// NotifyParentPropertyAttribute Type
            /// </summary>
            public static readonly Type NotifyParentPropertyAttribute = typeof(NotifyParentPropertyAttribute);
        }

        /// <summary>
        /// Avalonia types
        /// </summary>
        public static class Avalonia
        {
            /// <summary>
            /// Geometry type
            /// </summary>
            public static readonly Type Geometry = typeof(Geometry);
            /// <summary>
            /// CornerRadius type
            /// </summary>
            public static readonly Type CornerRadius = typeof(CornerRadius);

            //public static readonly Type Point3D = typeof(Point3D);
            //public static readonly Type Point4D = typeof(Point4D);
            //public static readonly Type Point3DCollection = typeof(Point3DCollection);
            //public static readonly Type Matrix3D = typeof(Matrix3D);
            /// <summary>
            /// Quaternion tyoe
            /// </summary>
            public static readonly Type Quaternion = typeof(Quaternion);

            //public static readonly Type Rect3D = typeof(Rect3D);
            //public static readonly Type Size3D = typeof(Size3D);
            //public static readonly Type Vector3D = typeof(Vector3D);
            //public static readonly Type Vector3DCollection = typeof(Vector3DCollection);
            //public static readonly Type PointCollection = typeof(PointCollection);
            //public static readonly Type VectorCollection = typeof(VectorCollection);
            /// <summary>
            /// Point type
            /// </summary>
            public static readonly Type Point = typeof(Point);

            /// <summary>
            /// rect type
            /// </summary>
            public static readonly Type Rect = typeof(Rect);
            /// <summary>
            /// Size type
            /// </summary>
            public static readonly Type Size = typeof(Size);
            /// <summary>
            /// Thickness type
            /// </summary>
            public static readonly Type Thickness = typeof(Thickness);
            /// <summary>
            /// Vector type
            /// </summary>
            public static readonly Type Vector = typeof(Vector);

            //public static readonly Type FontStretch = typeof(FontStretch);
            /// <summary>
            /// FontStyle type
            /// </summary>
            public static readonly Type FontStyle = typeof(FontStyle);

            /// <summary>
            /// FontWeight type
            /// </summary>
            public static readonly Type FontWeight = typeof(FontWeight);

            /// <summary>
            /// FontFamily type
            /// </summary>
            public static readonly Type FontFamily = typeof(FontFamily);
            /// <summary>
            /// Cursor type
            /// </summary>
            public static readonly Type Cursor = typeof(Cursor);
            /// <summary>
            /// Brush type
            /// </summary>
            public static readonly Type Brush = typeof(Brush);
        }

        /// <summary>
        /// property grid types
        /// </summary>
        public static class Wpg
        {
            /// <summary>
            /// Editor
            /// </summary>
            public static readonly Type Editor = typeof(Editor);
        }
    }
}
