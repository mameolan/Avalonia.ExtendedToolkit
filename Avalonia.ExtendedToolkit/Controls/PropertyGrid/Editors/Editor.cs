using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// A base class for all value editors.
    /// </summary>
    public abstract class Editor : TemplatedControl
    {
        public Type StyleKey => typeof(Editor);


        /// <summary>
        /// Gets or sets the inline template. 
        /// Can be either DataTemplate or ComponentResourceKey object.
        /// </summary>
        /// <value>The inline template.</value>
        public object InlineTemplate
        {
            get { return (object)GetValue(InlineTemplateProperty); }
            set { SetValue(InlineTemplateProperty, value); }
        }


        public static readonly StyledProperty<object> InlineTemplateProperty =
            AvaloniaProperty.Register<Editor, object>(nameof(InlineTemplate));


        /// <summary>
        /// Gets or sets the extended template. 
        /// Can be either DataTemplate or ComponentResourceKey object.
        /// </summary>
        /// <value>The extended template.</value>
        public object ExtendedTemplate
        {
            get { return (object)GetValue(ExtendedTemplateProperty); }
            set { SetValue(ExtendedTemplateProperty, value); }
        }


        public static readonly StyledProperty<object> ExtendedTemplateProperty =
            AvaloniaProperty.Register<Editor, object>(nameof(ExtendedTemplate));


        /// <summary>
        /// Gets or sets the dialog template.
        /// </summary>
        /// <value>The dialog template.</value>
        public object DialogTemplate
        {
            get { return (object)GetValue(DialogTemplateProperty); }
            set { SetValue(DialogTemplateProperty, value); }
        }


        public static readonly StyledProperty<object> DialogTemplateProperty =
            AvaloniaProperty.Register<Editor, object>(nameof(DialogTemplate));


        /// <summary>
        /// Shows the dialog for editing property value.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="commandSource">The command source.</param>
        public virtual void ShowDialog(PropertyItemValue propertyValue, IInputElement commandSource)
        {
        }


    }
}
