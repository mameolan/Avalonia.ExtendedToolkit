using System;

namespace Avalonia.ExtendedToolkit.TriggerExtensions
{
    /// <summary>
    /// implements TriggerAction T have to be an AvaloniaObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TriggerAction<T>: TriggerAction where T: AvaloniaObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerAction&lt;T&gt;"/> class.
        /// </summary>
        protected TriggerAction()
            : base(typeof(T))
        {
        }

        /// <summary>
        /// Gets the object to which this <see cref="TriggerAction"/> is attached.
        /// </summary>
        /// <value>The associated object.</value>
        protected new T AssociatedObject
        {
            get
            {
                return (T)base.AssociatedObject;
            }
        }

        /// <summary>
        /// Gets the associated object type constraint.
        /// </summary>
        /// <value>The associated object type constraint.</value>
        protected sealed override Type AssociatedObjectTypeConstraint
        {
            get
            {
                return base.AssociatedObjectTypeConstraint;
            }
        }
    }
}
