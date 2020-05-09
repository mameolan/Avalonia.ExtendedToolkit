using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Internal;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    internal static class ObjectServices
    {
        private static readonly Type[] CultureInvariantTypes = new Type[]
        {
      typeof(CornerRadius),
      //typeof(Point3D),
      //typeof(Point4D),
      //typeof(Point3DCollection),
      //typeof(Matrix3D),
      //typeof(Quaternion),
      //typeof(Rect3D),
      //typeof(Size3D),
      //typeof(Vector3D),
      //typeof(Vector3DCollection),
      //typeof(PointCollection),
      //typeof(VectorCollection),
      typeof(Point),
      typeof(Rect),
      typeof(Size),
      typeof(Thickness),
      typeof(Vector)
        };

        private static readonly string[] StringConverterMembers = { "Content", "Header", "ToolTip", "Tag" };

        private static StringConverter _defaultStringConverter;

        public static StringConverter DefaultStringConverter
        {
            get
            {
                if (_defaultStringConverter == null)
                    _defaultStringConverter = new StringConverter();
                return _defaultStringConverter;
            }
        }

        private static FontStretchConverterDecorator _defaultFontStretchConverterDecorator;

        public static FontStretchConverterDecorator DefaultFontStretchConverterDecorator
        {
            get { return _defaultFontStretchConverterDecorator ?? (_defaultFontStretchConverterDecorator = new FontStretchConverterDecorator()); }
        }

        private static FontStyleConverterDecorator _DefaultFontStyleConverterDecorator;

        public static FontStyleConverterDecorator DefaultFontStyleConverterDecorator
        {
            get
            {
                if (_DefaultFontStyleConverterDecorator == null)
                    _DefaultFontStyleConverterDecorator = new FontStyleConverterDecorator();
                return _DefaultFontStyleConverterDecorator;
            }
        }

        private static FontWeightConverterDecorator _defaultFontWeightConverterDecorator;

        public static FontWeightConverterDecorator DefaultFontWeightConverterDecorator
        {
            get { return _defaultFontWeightConverterDecorator ?? (_defaultFontWeightConverterDecorator = new FontWeightConverterDecorator()); }
        }

        [Obsolete("This member will be superceded by PropertyItem.SerializationCulture in the next versions of component", false)]
        public static CultureInfo GetSerializationCulture(Type propertyType)
        {
            var currentCulture = CultureInfo.CurrentCulture;

            if (propertyType == null)
                return currentCulture;

            if ((Array.IndexOf(CultureInvariantTypes, propertyType) == -1) && !typeof(Geometry).IsAssignableFrom(propertyType))
                return currentCulture;

            return CultureInfo.InvariantCulture;
        }

        public static TypeConverter GetPropertyConverter(PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor == null)
                throw new ArgumentNullException(nameof(propertyDescriptor));

            if (StringConverterMembers.Contains(propertyDescriptor.Name)
              && propertyDescriptor.PropertyType.IsAssignableFrom(typeof(object)))
                return DefaultStringConverter;
            //if (typeof(FontStretch).IsAssignableFrom(propertyDescriptor.PropertyType))
            //    return DefaultFontStretchConverterDecorator;
            if (typeof(FontStyle).IsAssignableFrom(propertyDescriptor.PropertyType))
                return DefaultFontStyleConverterDecorator;
            if (typeof(FontWeight).IsAssignableFrom(propertyDescriptor.PropertyType))
                return DefaultFontWeightConverterDecorator;
            return propertyDescriptor.Converter;
        }

        internal static IEnumerable<PropertyDescriptor> GetMergedProperties(IEnumerable<object> targets)
        {
            var merged = new List<PropertyDescriptor>();
            var props = MetadataRepository.GetCommonProperties(targets);
            foreach (var pData in props)
            {
                var descriptors = targets.Select(target => MetadataRepository.GetProperty(target, pData.Name).Descriptor);
                merged.Add(new MergedPropertyDescriptor(descriptors.ToArray()));
            }

            return merged;
        }

        internal static object GetUnwrappedObject(object currentObject)
        {
            var customTypeDescriptor = currentObject as ICustomTypeDescriptor;
            return customTypeDescriptor != null ? customTypeDescriptor.GetPropertyOwner(null) : currentObject;
        }
    }
}
