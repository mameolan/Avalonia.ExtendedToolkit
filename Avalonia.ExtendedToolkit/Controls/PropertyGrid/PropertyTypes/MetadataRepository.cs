using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// MetadataRepository
    /// </summary>
    public static class MetadataRepository
    {
        private class PropertySet : Dictionary<string, PropertyData> { }

        private class AttributeSet : Dictionary<string, HashSet<Attribute>> { }

        private static readonly Dictionary<Type, PropertySet> Properties = new Dictionary<Type, PropertySet>();
        private static readonly Dictionary<Type, AttributeSet> PropertyAttributes = new Dictionary<Type, AttributeSet>();
        private static readonly Dictionary<Type, HashSet<Attribute>> TypeAttributes = new Dictionary<Type, HashSet<Attribute>>();

        private static readonly Attribute[] PropertyFilter = new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.SetValues | PropertyFilterOptions.UnsetValues | PropertyFilterOptions.Valid) };

        /// <summary>
        /// clears the property
        /// </summary>
        public static void Clear()
        {
            Properties.Clear();
            PropertyAttributes.Clear();
            TypeAttributes.Clear();
        }

        /// <summary>
        /// gets properties from the target
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyData> GetProperties(object target)
        {
            return DoGetProperties(target).ToList().AsReadOnly();
        }

        private static IEnumerable<PropertyData> DoGetProperties(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            PropertySet result;
            Properties.TryGetValue(target.GetType(), out result);

            if(result==null|| result.Count==0)
            {
                result = CollectProperties(target);
            }

            return result.Values;
        }

        /// <summary>
        /// gets the common properties
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyData> GetCommonProperties(IEnumerable<object> targets)
        {
            if (targets == null)
                return Enumerable.Empty<PropertyData>();

            IEnumerable<PropertyData> result = null;

            foreach (object target in targets)
            {
                var properties = DoGetProperties(target).Where(prop => prop.IsBrowsable && prop.IsMergable);
                result = (result == null) ? properties : result.Intersect(properties);
            }

            return (result != null) ? result : Enumerable.Empty<PropertyData>();
        }

        /// <summary>
        /// gets the property from target by propertyName
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyData GetProperty(object target, string propertyName)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            PropertySet propertySet = null;

            if (!Properties.TryGetValue(target.GetType(), out propertySet))
                propertySet = CollectProperties(target);

            PropertyData property;

            if (propertySet.TryGetValue(propertyName, out property))
                return property;

            return null;
        }

        private static PropertySet CollectProperties(object target)
        {
            Type targetType = target.GetType();
            PropertySet result;

            if (!Properties.TryGetValue(targetType, out result))
            {
                result = new PropertySet();

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(target)
                    /*, PropertyFilter)*/)
                {
                    result.Add(descriptor.Name, new PropertyData(descriptor));
                    CollectAttributes(target, descriptor);
                }

                Properties.Add(targetType, result);
            }

            return result;
        }

        /// <summary>
        /// gets the attriutes from the target
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetAttributes(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            return CollectAttributes(target).ToList().AsReadOnly();
        }

        private static HashSet<Attribute> CollectAttributes(object target)
        {
            Type targetType = target.GetType();
            HashSet<Attribute> attributes;

            if (!TypeAttributes.TryGetValue(targetType, out attributes))
            {
                attributes = new HashSet<Attribute>();

                foreach (Attribute attribute in TypeDescriptor.GetAttributes(target))
                    attributes.Add(attribute);

                TypeAttributes.Add(targetType, attributes);
            }

            return attributes;
        }

        private static HashSet<Attribute> CollectAttributes(object target, PropertyDescriptor descriptor)
        {
            Type targetType = target.GetType();
            AttributeSet attributeSet;

            if (!PropertyAttributes.TryGetValue(targetType, out attributeSet))
            {
                // Create an empty attribute sequence
                attributeSet = new AttributeSet();
                PropertyAttributes.Add(targetType, attributeSet);
            }

            HashSet<Attribute> attributes;

            if (!attributeSet.TryGetValue(descriptor.Name, out attributes))
            {
                attributes = new HashSet<Attribute>();

                foreach (Attribute attribute in descriptor.Attributes)
                    attributes.Add(attribute);

                attributeSet.Add(descriptor.Name, attributes);
            }

            return attributes;
        }

        /// <summary>
        /// gets attributes by target by property name
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetAttributes(object target, string propertyName)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            Type targetType = target.GetType();

            if (!PropertyAttributes.ContainsKey(targetType))
                CollectProperties(target);

            AttributeSet attributeSet;

            if (PropertyAttributes.TryGetValue(targetType, out attributeSet))
            {
                HashSet<Attribute> result;
                if (attributeSet.TryGetValue(propertyName, out result))
                    return result.ToList().AsReadOnly();
            }

            return Enumerable.Empty<Attribute>();
        }
    }
}
