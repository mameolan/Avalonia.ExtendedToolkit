using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Internal;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
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

        #region DefaultStringConverter

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

        #endregion DefaultStringConverter

        #region DefaultFontStretchConverterDecorator

        private static FontStretchConverterDecorator _defaultFontStretchConverterDecorator;

        public static FontStretchConverterDecorator DefaultFontStretchConverterDecorator
        {
            get { return _defaultFontStretchConverterDecorator ?? (_defaultFontStretchConverterDecorator = new FontStretchConverterDecorator()); }
        }

        #endregion DefaultFontStretchConverterDecorator

        #region DefaultFontStyleConverterDecorator

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

        #endregion DefaultFontStyleConverterDecorator

        #region DefaultFontWeightConverterDecorator

        private static FontWeightConverterDecorator _defaultFontWeightConverterDecorator;

        public static FontWeightConverterDecorator DefaultFontWeightConverterDecorator
        {
            get { return _defaultFontWeightConverterDecorator ?? (_defaultFontWeightConverterDecorator = new FontWeightConverterDecorator()); }
        }

        #endregion DefaultFontWeightConverterDecorator

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
                throw new ArgumentNullException("propertyDescriptor");

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

        #region MultiSelected Objects Support

        // This is an obsolete code left for performance improvements demo. Will be removed in the future versions.
        /*
        static Func<PropertyDescriptor, bool> IsBrowsable = (prop) => prop.IsBrowsable;
        static Func<PropertyDescriptor, bool> IsMergable = (prop) =>
        {
          MergablePropertyAttribute attribute = prop.Attributes[typeof(MergablePropertyAttribute)] as MergablePropertyAttribute;
          return attribute != null ? attribute.AllowMerge : true;
        };

        static Func<PropertyDescriptor, bool> IsCommon = (prop) => IsBrowsable(prop) && IsMergable(prop);

        internal static IEnumerable<PropertyDescriptor> GetCommonProperties(IEnumerable<object> targets)
        {
          IEnumerable<PropertyDescriptor> result = null;

          foreach (object target in targets)
          {
            var items = TypeDescriptor.GetProperties(target).OfType<PropertyDescriptor>().Where(IsCommon);
            result = (result == null) ? items : result.Intersect(items);
          }

          return result;
        }
        */

        internal static IEnumerable<PropertyDescriptor> GetMergedProperties(IEnumerable<object> targets)
        {
            // This is an obsolete code left for performance improvements demo. Will be removed in the future versions.
            /*
            List<PropertyDescriptor> mergedProperties = new List<PropertyDescriptor>();

            IEnumerable<PropertyDescriptor> commonProperties = GetCommonProperties(targets);

            foreach (PropertyDescriptor descriptor in commonProperties)
            {
              List<PropertyDescriptor> descriptors = new List<PropertyDescriptor>();
              foreach (object target in targets)
              {
                descriptors.Add(TypeDescriptor.GetProperties(target)[descriptor.Name]);
              }
              mergedProperties.Add(new MergedPropertyDescriptor(descriptors.ToArray()));
            }

            return mergedProperties;
            */

            var merged = new List<PropertyDescriptor>();
            var props = MetadataRepository.GetCommonProperties(targets);
            foreach (var pData in props)
            {
                var descriptors = targets.Select(target => MetadataRepository.GetProperty(target, pData.Name).Descriptor);
                merged.Add(new MergedPropertyDescriptor(descriptors.ToArray()));
            }

            return merged;
        }

        #endregion MultiSelected Objects Support

        internal static object GetUnwrappedObject(object currentObject)
        {
            var customTypeDescriptor = currentObject as ICustomTypeDescriptor;
            return customTypeDescriptor != null ? customTypeDescriptor.GetPropertyOwner(null) : currentObject;
        }
    }
}
