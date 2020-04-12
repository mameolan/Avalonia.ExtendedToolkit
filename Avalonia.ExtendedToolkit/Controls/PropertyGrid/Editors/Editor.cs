using System;
using System.Linq;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Editors
{
    /// <summary>
    /// A base class for all value editors.
    /// </summary>
    public abstract class Editor : TemplatedControl
    {
        public Type StyleKey => typeof(Editor);

        /// <summary>
        /// Gets or sets the inline template.
        /// Can be either ControlTemplate or string which is resolved as a ControlTemplate.
        /// </summary>
        /// <value>The inline template.</value>
        public object InlineTemplate
        {
            get { return (object)GetValue(InlineTemplateProperty); }
            set {

                if (value == null)
                    return;

                if(value is string|| value is ControlTemplate)
                {
                    SetValue(InlineTemplateProperty, value);
                }
                else
                {
                    throw new NotSupportedException("InlineTemplate have to be a string or a ControlTemplate");
                }
            }
        }

        public static readonly StyledProperty<object> InlineTemplateProperty =
            AvaloniaProperty.Register<Editor, object>(nameof(InlineTemplate));

        /// <summary>
        /// Gets or sets the extended template.
        /// Can be either DataTemplate or string which is resolved as a datatemplate.
        /// </summary>
        /// <value>The extended template.</value>
        public object ExtendedTemplate
        {
            get { return (object)GetValue(ExtendedTemplateProperty); }
            set
            {
                if (value == null)
                    return;

                if (value is string || value is ControlTemplate)
                {
                    SetValue(ExtendedTemplateProperty, value);
                }
                else
                {
                    throw new NotSupportedException("ExtendedTemplate have to be a string or a ControlTemplate");
                }
            }
                
        }

        /// <summary>
        /// if the template parameter is string the template is searched in the resource
        /// if the template is Control Template the template is returned
        /// else an exception is thrown
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        internal ControlTemplate GetEditorTemplate(object template)
        {
            if (template == null)
                return null;

            if (template is string)
            {
                var genericStyle = Application.Current.Styles.OfType<StyleInclude>().FirstOrDefault(styleInclude => styleInclude.
                             Source.AbsoluteUri.StartsWith("avares://Avalonia.ExtendedToolkit/Styles/Generic.xaml"));

                var result = (genericStyle.Loaded as Styles)?.OfType<StyleInclude>()
                        .FirstOrDefault(styleInclude => styleInclude.
                             Source.AbsoluteUri.EndsWith("PropertyGrid/Editor/EditorResources.xaml"));

                result.Loaded.TryGetResource(template, out object resourceValue);

                ControlTemplate controlTemplate = resourceValue as ControlTemplate;


                return controlTemplate;// inlineTemplate;
            }
            else
            {
                if (template is ControlTemplate)
                {
                    return (ControlTemplate)template;
                }
                else
                {
                    throw new NotSupportedException($"inlineTemplate must be a string or a ControlTemplate");
                }
            }
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

        public Editor()
        {
            InlineTemplateProperty.Changed.AddClassHandler<Editor>((o, e) => OnEditorTemplateChanged(o, e));
        }

        private void OnEditorTemplateChanged(Editor editor, AvaloniaPropertyChangedEventArgs e)
        {
            editor.OnTemplateChanged(e);
        }
    }
}
