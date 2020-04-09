﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    [DebuggerDisplay("{Name}")]
    // For the moment this class is a wrapper around PropertyDescriptor. Later on it will be migrated into a separate independent unit.
    // It will be able in future creating dynamic objects without using reflection
    public class PropertyData : IEquatable<PropertyData>
    {
        private static readonly List<Type> CultureInvariantTypes = new List<Type>
    {
      KnownTypes.Wpf.CornerRadius,
      //KnownTypes.Wpf.Point3D,
      //KnownTypes.Wpf.Point4D,
      //KnownTypes.Wpf.Point3DCollection,
      //KnownTypes.Wpf.Matrix3D,
      KnownTypes.Wpf.Quaternion,
      //KnownTypes.Wpf.Rect3D,
      //KnownTypes.Wpf.Size3D,
      //KnownTypes.Wpf.Vector3D,
      //KnownTypes.Wpf.Vector3DCollection,
      //KnownTypes.Wpf.PointCollection,
      //KnownTypes.Wpf.VectorCollection,
      KnownTypes.Wpf.Point,
      KnownTypes.Wpf.Rect,
      KnownTypes.Wpf.Size,
      KnownTypes.Wpf.Thickness,
      KnownTypes.Wpf.Vector
    };

        private static readonly string[] StringConverterMembers = { "Content", "Header", "ToolTip", "Tag" };

        public PropertyDescriptor Descriptor { get; private set; }

        public string Name
        {
            get { return Descriptor.Name; }
        }

        public string DisplayName
        {
            get { return Descriptor.DisplayName; }
        }

        public string Description
        {
            get { return Descriptor.Description; }
        }

        public string Category
        {
            get { return Descriptor.Category; }
        }

        public Type PropertyType
        {
            get { return Descriptor.PropertyType; }
        }

        public Type ComponentType
        {
            get { return Descriptor.ComponentType; }
        }

        public bool IsBrowsable
        {
            get { return Descriptor.IsBrowsable; }
        }

        public bool IsReadOnly
        {
            get { return Descriptor.IsReadOnly; }
        }

        // TODO: Cache value?
        public bool IsMergable
        {
            get { return MergablePropertyAttribute.Yes.Equals(Descriptor.Attributes[KnownTypes.Attributes.MergablePropertyAttribute]); }
        }

        // TODO: Cache value?
        public bool IsAdvanced
        {
            get
            {
                var attr = Descriptor.Attributes[KnownTypes.Attributes.EditorBrowsableAttribute] as EditorBrowsableAttribute;
                return attr != null && attr.State == EditorBrowsableState.Advanced;
            }
        }

        public bool IsLocalizable
        {
            get { return Descriptor.IsLocalizable; }
        }

        public bool IsCollection
        {
            get { return KnownTypes.Collections.IList.IsAssignableFrom(this.PropertyType); }
        }

        public DesignerSerializationVisibility SerializationVisibility
        {
            get { return Descriptor.SerializationVisibility; }
        }

        private CultureInfo _SerializationCulture;

        public CultureInfo SerializationCulture
        {
            get
            {
                if (_SerializationCulture == null)
                {
                    _SerializationCulture = (CultureInvariantTypes.Contains(PropertyType) || KnownTypes.Wpf.Geometry.IsAssignableFrom(PropertyType))
                      ? CultureInfo.InvariantCulture
                      : CultureInfo.CurrentCulture;
                }

                return _SerializationCulture;
            }
        }

        public PropertyData(PropertyDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public override int GetHashCode()
        {
            return Descriptor.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PropertyData data = obj as PropertyData;
            return (data != null) ? Descriptor.Equals(data.Descriptor) : false;
        }

        public bool Equals(PropertyData other)
        {
            return Descriptor.Equals(other.Descriptor);
        }
    }
}
