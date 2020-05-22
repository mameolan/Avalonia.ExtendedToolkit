using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// For the moment this class is a wrapper around PropertyDescriptor. 
    /// Later on it will be migrated into a separate independent unit.
    /// It will be able in future creating dynamic objects without using reflection
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class PropertyData : IEquatable<PropertyData>
    {
        private static readonly List<Type> CultureInvariantTypes = new List<Type>
    {
      KnownTypes.Avalonia.CornerRadius,
      //KnownTypes.Avalonia.Point3D,
      //KnownTypes.Avalonia.Point4D,
      //KnownTypes.Avalonia.Point3DCollection,
      //KnownTypes.Avalonia.Matrix3D,
      KnownTypes.Avalonia.Quaternion,
      //KnownTypes.Avalonia.Rect3D,
      //KnownTypes.Avalonia.Size3D,
      //KnownTypes.Avalonia.Vector3D,
      //KnownTypes.Avalonia.Vector3DCollection,
      //KnownTypes.Avalonia.PointCollection,
      //KnownTypes.Avalonia.VectorCollection,
      KnownTypes.Avalonia.Point,
      KnownTypes.Avalonia.Rect,
      KnownTypes.Avalonia.Size,
      KnownTypes.Avalonia.Thickness,
      KnownTypes.Avalonia.Vector
    };

        private static readonly string[] StringConverterMembers = { "Content", "Header", "ToolTip", "Tag" };

        /// <summary>
        /// get/sets Descriptor
        /// </summary>
        public PropertyDescriptor Descriptor { get; private set; }

        /// <summary>
        /// gets Descriptor.Name
        /// </summary>
        public string Name
        {
            get { return Descriptor.Name; }
        }

        /// <summary>
        /// gets Descriptor.DisplayName
        /// </summary>
        public string DisplayName
        {
            get { return Descriptor.DisplayName; }
        }

        /// <summary>
        /// gets Descriptor.Description
        /// </summary>
        public string Description
        {
            get { return Descriptor.Description; }
        }

        /// <summary>
        /// gets Descriptor.Category
        /// </summary>
        public string Category
        {
            get { return Descriptor.Category; }
        }

        /// <summary>
        /// gets Descriptor.PropertyType
        /// </summary>
        public Type PropertyType
        {
            get { return Descriptor.PropertyType; }
        }

        /// <summary>
        /// gets Descriptor.ComponentType
        /// </summary>
        public Type ComponentType
        {
            get { return Descriptor.ComponentType; }
        }

        /// <summary>
        /// gets Descriptor.IsBrowsable
        /// </summary>
        public bool IsBrowsable
        {
            get { return Descriptor.IsBrowsable; }
        }

        /// <summary>
        /// gets Descriptor.IsReadOnly
        /// </summary>
        public bool IsReadOnly
        {
            get { return Descriptor.IsReadOnly; }
        }


        /// <summary>
        /// gets IsMergable
        /// TODO: Cache value?
        /// </summary>
        public bool IsMergable
        {
            get { return MergablePropertyAttribute.Yes.Equals(Descriptor.Attributes[KnownTypes.Attributes.MergablePropertyAttribute]); }
        }


        /// <summary>
        /// gets IsAdvanced
        /// TODO: Cache value?
        /// </summary>
        public bool IsAdvanced
        {
            get
            {
                var attr = Descriptor.Attributes[KnownTypes.Attributes.EditorBrowsableAttribute] as EditorBrowsableAttribute;
                return attr != null && attr.State == EditorBrowsableState.Advanced;
            }
        }

        /// <summary>
        /// gets Descriptor.IsLocalizable
        /// </summary>
        public bool IsLocalizable
        {
            get { return Descriptor.IsLocalizable; }
        }

        /// <summary>
        /// gets IsCollection
        /// </summary>
        public bool IsCollection
        {
            get { return KnownTypes.Collections.IList.IsAssignableFrom(this.PropertyType); }
        }

        /// <summary>
        /// gets Descriptor.SerializationVisibility
        /// </summary>
        public DesignerSerializationVisibility SerializationVisibility
        {
            get { return Descriptor.SerializationVisibility; }
        }

        private CultureInfo _serializationCulture;

        /// <summary>
        /// gets SerializationCulture
        /// </summary>
        public CultureInfo SerializationCulture
        {
            get
            {
                if (_serializationCulture == null)
                {
                    _serializationCulture = (CultureInvariantTypes.Contains(PropertyType)
                        || KnownTypes.Avalonia.Geometry.IsAssignableFrom(PropertyType))
                      ? CultureInfo.InvariantCulture
                      : CultureInfo.CurrentCulture;
                }

                return _serializationCulture;
            }
        }

        /// <summary>
        /// sets Descriptor
        /// </summary>
        /// <param name="descriptor"></param>
        public PropertyData(PropertyDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        /// <summary>
        /// gets Descriptor.GetHashCode()
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Descriptor.GetHashCode();
        }

        /// <summary>
        /// check if Descriptor equals obj
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            PropertyData data = obj as PropertyData;
            return (data != null) ? Descriptor.Equals(data.Descriptor) : false;
        }

        /// <summary>
        /// check if Descriptor equals other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PropertyData other)
        {
            return Descriptor.Equals(other.Descriptor);
        }
    }
}
