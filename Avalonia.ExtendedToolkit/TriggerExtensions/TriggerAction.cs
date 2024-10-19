using System;
using Avalonia.Animation;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.ExtendedToolkit.TriggerExtensions
{
    /// <summary>
    /// class which implements <see cref="IBehavior"/> and <see cref="IAction"/>
    /// </summary>
    public abstract class TriggerAction :Animatable, IBehavior, IAction
    {
        private AvaloniaObject _associatedObject;
        private Type associatedObjectTypeConstraint;

        /// <summary>
        /// get/sets IsEnabled
        /// </summary>
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        /// <summary>
        /// <see cref="IsEnabled"/>
        /// </summary>
        public static readonly StyledProperty<bool> IsEnabledProperty =
            AvaloniaProperty.Register<TriggerAction, bool>(nameof(IsEnabled));

        /// <summary>
        /// returns the AssociatedObject
        /// </summary>
        public AvaloniaObject AssociatedObject => _associatedObject;

        /// <summary>
        /// Gets the associated object type constraint.
        /// </summary>
        /// <value>The associated object type constraint.</value>
        protected virtual Type AssociatedObjectTypeConstraint
        {
            get
            {
                return associatedObjectTypeConstraint;
            }
        }

        AvaloniaObject IBehavior.AssociatedObject => throw new NotImplementedException();

        internal TriggerAction(Type associatedObjectTypeConstraint)
        {
            this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
        }

        /// <summary>
        /// Attempts to invoke the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        internal void CallInvoke(object parameter)
        {
            if (this.IsEnabled)
            {
                this.Invoke(parameter);
            }
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected abstract void Invoke(object parameter);

        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected virtual void OnDetaching()
        {
        }

        /// <summary>
        /// tries to set the AssociatedObject
        /// </summary>
        /// <param name="avaloniaObject"></param>
        public void Attach(AvaloniaObject avaloniaObject)
        {
            if (avaloniaObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException("Associated Object already set.");
                }

                // Ensure the type constraint is met
                if (avaloniaObject != null && !this.AssociatedObjectTypeConstraint.IsAssignableFrom(avaloniaObject.GetType()))
                {
                    throw new InvalidOperationException($"Cannot assign {this.GetType().Name} {avaloniaObject.GetType().Name} {this.AssociatedObjectTypeConstraint.Name}");
                }

                this._associatedObject = avaloniaObject;
                this.OnAttached();
            }
        }

        /// <summary>
        /// Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            this.OnDetaching();
            this._associatedObject = null;
        }

        /// <summary>
        /// executes the command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object Execute(object sender, object parameter)
        {
            CallInvoke(parameter);

            return true;
        }
    }
}
