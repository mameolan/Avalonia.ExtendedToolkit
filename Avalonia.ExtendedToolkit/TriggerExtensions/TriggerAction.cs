using Avalonia.Animation;
using Avalonia.Xaml.Interactivity;
using System;

namespace Avalonia.ExtendedToolkit.TriggerExtensions
{
    public abstract class TriggerAction :Animatable, IBehavior, IAction
    {
        private AvaloniaObject associatedObject;
        private Type associatedObjectTypeConstraint;

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly StyledProperty<bool> IsEnabledProperty =
            AvaloniaProperty.Register<TriggerAction, bool>(nameof(IsEnabled));

        public AvaloniaObject AssociatedObject => associatedObject;

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

        public void Attach(AvaloniaObject dependencyObject)
        {
            if (dependencyObject != this.AssociatedObject)
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException("Associated Object already set.");
                }

                // Ensure the type constraint is met
                if (dependencyObject != null && !this.AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()))
                {
                    throw new InvalidOperationException($"Cannot assign {this.GetType().Name} {dependencyObject.GetType().Name} {this.AssociatedObjectTypeConstraint.Name}");
                }

                this.associatedObject = dependencyObject;
                this.OnAttached();
            }
        }

        /// <summary>
        /// Detaches this instance from its associated object.
        /// </summary>
        public void Detach()
        {
            this.OnDetaching();
            this.associatedObject = null;
        }

        public object Execute(object sender, object parameter)
        {
            CallInvoke(parameter);

            return true;
        }
    }
}