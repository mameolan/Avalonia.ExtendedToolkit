using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Internal
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    [DebuggerDisplay("{Name}")]
    internal class MergedPropertyDescriptor : PropertyDescriptor
    {
        // Fields
        private TriState canReset;

        private MultiMergeCollection collection;
        private PropertyDescriptor[] descriptors;
        private TriState localizable;
        private TriState readOnly;
        private Hashtable handlers;
        private bool internalValueSet = false;

        // Methods
        public MergedPropertyDescriptor(PropertyDescriptor[] descriptors)
          : base(descriptors[0].Name, null)
        {
            this.descriptors = descriptors;
        }

        public sealed override void AddValueChanged(object component, EventHandler handler)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            Array targets = component as Array;
            if (targets == null)
                throw new ArgumentException("Descriptor expects an array of objects!");

            if (handlers == null)
                handlers = new Hashtable();

            for (int i = 0; i < descriptors.Length; i++)
            {
                object target = targets.GetValue(i);

                descriptors[i].AddValueChanged(target, OnValueChanged);
                EventHandler h = (EventHandler)this.handlers[target];
                this.handlers[target] = Delegate.Combine(h, handler);
            }
        }

        public sealed override void RemoveValueChanged(object component, EventHandler handler)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            Array targets = component as Array;
            if (targets == null)
                throw new ArgumentException("Descriptor expects an array of objects!");

            for (int i = 0; i < descriptors.Length; i++)
            {
                object target = targets.GetValue(i);

                descriptors[i].AddValueChanged(target, OnValueChanged);
                if (handlers != null)
                {
                    EventHandler h = (EventHandler)this.handlers[target];
                    h = (EventHandler)Delegate.Remove(h, handler);

                    if (h != null)
                        this.handlers[target] = h;
                    else
                        this.handlers.Remove(target);
                }
            }
        }

        protected override void OnValueChanged(object component, EventArgs e)
        {
            if (internalValueSet)
                return;

            if (component != null && this.handlers != null)
            {
                EventHandler handler = (EventHandler)this.handlers[component];
                if (handler != null)
                    handler(component, e);
            }
        }

        public override bool CanResetValue(object component)
        {
            if (this.canReset == TriState.Unknown)
            {
                this.canReset = TriState.Yes;
                Array a = (Array)component;
                for (int i = 0; i < this.descriptors.Length; i++)
                {
                    if (!this.descriptors[i].CanResetValue(this.GetPropertyOwnerForComponent(a, i)))
                    {
                        this.canReset = TriState.No;
                        break;
                    }
                }
            }
            return (this.canReset == TriState.Yes);
        }

        private object CopyValue(object value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                if (type.IsValueType)
                {
                    return value;
                }
                object obj2 = null;
                ICloneable cloneable = value as ICloneable;
                if (cloneable != null)
                {
                    obj2 = cloneable.Clone();
                }
                if (obj2 == null)
                {
                    // TODO: Reuse ObjectServices here?
                    TypeConverter converter = TypeDescriptor.GetConverter(value);
                    if (converter.CanConvertTo(typeof(InstanceDescriptor)))
                    {
                        InstanceDescriptor descriptor = (InstanceDescriptor)converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(InstanceDescriptor));
                        if ((descriptor != null) && descriptor.IsComplete)
                        {
                            obj2 = descriptor.Invoke();
                        }
                    }
                    if (((obj2 == null) && converter.CanConvertTo(typeof(string))) && converter.CanConvertFrom(typeof(string)))
                    {
                        object obj3 = converter.ConvertToInvariantString(value);
                        obj2 = converter.ConvertFromInvariantString((string)obj3);
                    }
                }
                if ((obj2 == null) && type.IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream serializationStream = new MemoryStream();
                    formatter.Serialize(serializationStream, value);
                    serializationStream.Position = 0L;
                    obj2 = formatter.Deserialize(serializationStream);
                }
                if (obj2 != null)
                {
                    return obj2;
                }
            }
            return value;
        }

        protected override AttributeCollection CreateAttributeCollection()
        {
            IEnumerable<Attribute> attributes = null;
            Attribute[] buffer = null;

            foreach (PropertyDescriptor descriptor in descriptors)
            {
                buffer = new Attribute[descriptor.Attributes.Count];
                descriptor.Attributes.CopyTo(buffer, 0);
                attributes = (attributes == null) ? buffer : attributes.Intersect(buffer);
            }

            return new AttributeCollection(attributes.ToArray());
        }

        public override object GetEditor(Type editorBaseType)
        {
            return this.descriptors[0].GetEditor(editorBaseType);
        }

        private object GetPropertyOwnerForComponent(Array a, int i)
        {
            object propertyOwner = a.GetValue(i);
            if (propertyOwner is ICustomTypeDescriptor)
            {
                propertyOwner = ((ICustomTypeDescriptor)propertyOwner).GetPropertyOwner(this.descriptors[i]);
            }
            return propertyOwner;
        }

        public override object GetValue(object component)
        {
            bool flag;
            return this.GetValue((Array)component, out flag);
        }

        public object GetValue(Array components, out bool allEqual)
        {
            allEqual = true;
            object obj2 = this.descriptors[0].GetValue(this.GetPropertyOwnerForComponent(components, 0));
            if (obj2 is ICollection)
            {
                if (this.collection == null)
                    this.collection = new MultiMergeCollection((ICollection)obj2);
                else
                {
                    if (this.collection.Locked)
                        return this.collection;

                    this.collection.SetItems((ICollection)obj2);
                }
            }
            for (int i = 1; i < this.descriptors.Length; i++)
            {
                object obj3 = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(components, i));
                if (this.collection != null)
                {
                    if (!this.collection.MergeCollection((ICollection)obj3))
                    {
                        allEqual = false;
                        return null;
                    }
                }
                else if (((obj2 != null) || (obj3 != null)) && ((obj2 == null) || !obj2.Equals(obj3)))
                {
                    allEqual = false;
                    return null;
                }
            }

            if ((allEqual && (this.collection != null)) && (this.collection.Count == 0))
                return null;

            if (this.collection == null)
                return obj2;

            return this.collection;
        }

        internal object[] GetValues(Array components)
        {
            object[] objArray = new object[components.Length];

            for (int i = 0; i < components.Length; i++)
                objArray[i] = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(components, i));

            return objArray;
        }

        public override void ResetValue(object component)
        {
            Array a = (Array)component;

            for (int i = 0; i < this.descriptors.Length; i++)
                this.descriptors[i].ResetValue(this.GetPropertyOwnerForComponent(a, i));
        }

        private void SetCollectionValues(Array a, IList listValue)
        {
            try
            {
                if (this.collection != null)
                    this.collection.Locked = true;

                object[] array = new object[listValue.Count];
                listValue.CopyTo(array, 0);
                for (int i = 0; i < this.descriptors.Length; i++)
                {
                    IList list = this.descriptors[i].GetValue(this.GetPropertyOwnerForComponent(a, i)) as IList;
                    if (list != null)
                    {
                        list.Clear();
                        foreach (object obj2 in array)
                            list.Add(obj2);
                    }
                }
            }
            finally
            {
                if (this.collection != null)
                    this.collection.Locked = false;
            }
        }

        public override void SetValue(object component, object value)
        {
            Array a = (Array)component;

            if ((value is IList) && typeof(IList).IsAssignableFrom(this.PropertyType))
            {
                //TODO: Check whether internalValueSet should be configured here too...
                this.SetCollectionValues(a, (IList)value);
            }
            else
            {
                internalValueSet = true;
                for (int i = 0; i < this.descriptors.Length; i++)
                {
                    object obj2 = this.CopyValue(value);
                    object owner = this.GetPropertyOwnerForComponent(a, i);
                    this.descriptors[i].SetValue(owner, obj2);

                    //OnValueChanged(owner, new PropertyChangedEventArgs(descriptors[i].Name));
                }
                internalValueSet = false;
                OnValueChanged(component, new PropertyChangedEventArgs(this.Name));
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            Array a = (Array)component;
            for (int i = 0; i < this.descriptors.Length; i++)
            {
                if (!this.descriptors[i].ShouldSerializeValue(this.GetPropertyOwnerForComponent(a, i)))
                    return false;
            }
            return true;
        }

        // Properties
        public override Type ComponentType
        {
            get { return this.descriptors[0].ComponentType; }
        }

        public override TypeConverter Converter
        {
            get { return this.descriptors[0].Converter; }
        }

        public override string DisplayName
        {
            get { return this.descriptors[0].DisplayName; }
        }

        public override bool IsLocalizable
        {
            get
            {
                if (this.localizable == TriState.Unknown)
                {
                    this.localizable = TriState.Yes;
                    foreach (PropertyDescriptor descriptor in this.descriptors)
                    {
                        if (!descriptor.IsLocalizable)
                        {
                            this.localizable = TriState.No;
                            break;
                        }
                    }
                }
                return (this.localizable == TriState.Yes);
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                if (this.readOnly == TriState.Unknown)
                {
                    this.readOnly = TriState.No;
                    foreach (PropertyDescriptor descriptor in this.descriptors)
                    {
                        if (descriptor.IsReadOnly)
                        {
                            this.readOnly = TriState.Yes;
                            break;
                        }
                    }
                }
                return (this.readOnly == TriState.Yes);
            }
        }

        public PropertyDescriptor this[int index]
        {
            get { return this.descriptors[index]; }
        }

        public override Type PropertyType
        {
            get { return this.descriptors[0].PropertyType; }
        }

        private class MultiMergeCollection : ICollection, IEnumerable
        {
            private object[] items;
            private bool locked;

            public MultiMergeCollection(ICollection original)
            {
                this.SetItems(original);
            }

            public void CopyTo(Array array, int index)
            {
                if (this.items != null)
                    Array.Copy(this.items, 0, array, index, this.items.Length);
            }

            public IEnumerator GetEnumerator()
            {
                if (this.items != null)
                    return this.items.GetEnumerator();

                return new object[0].GetEnumerator();
            }

            public bool MergeCollection(ICollection newCollection)
            {
                if (!this.locked)
                {
                    if (this.items.Length != newCollection.Count)
                    {
                        this.items = new object[0];
                        return false;
                    }

                    object[] array = new object[newCollection.Count];
                    newCollection.CopyTo(array, 0);
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (((array[i] == null) != (this.items[i] == null)) || ((this.items[i] != null) && !this.items[i].Equals(array[i])))
                        {
                            this.items = new object[0];
                            return false;
                        }
                    }
                }
                return true;
            }

            public void SetItems(ICollection collection)
            {
                if (!this.locked)
                {
                    this.items = new object[collection.Count];
                    collection.CopyTo(this.items, 0);
                }
            }

            public int Count
            {
                get
                {
                    if (this.items != null)
                        return this.items.Length;

                    return 0;
                }
            }

            public bool Locked
            {
                get { return this.locked; }
                set { this.locked = value; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return this; }
            }
        }

        private enum TriState
        {
            Unknown,
            Yes,
            No
        }
    }
}
