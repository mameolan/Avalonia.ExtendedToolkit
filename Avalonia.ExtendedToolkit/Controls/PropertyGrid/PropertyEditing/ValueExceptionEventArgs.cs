using System;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyTypes;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.PropertyEditing
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Contains event data related to value exceptions.
    /// </summary>
    public sealed class ValueExceptionEventArgs : EventArgs
    {
        private readonly Exception _exception;
        private readonly string _message;
        private readonly ValueExceptionSource _source;
        private readonly PropertyItemValue _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        /// <param name="exception">The exception.</param>
        public ValueExceptionEventArgs(string message, PropertyItemValue value, ValueExceptionSource source, Exception exception)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));
            _message = message;
            _value = value;
            _source = source;
            _exception = exception;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get { return _exception; }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return _message; }
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <value>The property value.</value>
        public PropertyItemValue PropertyValue
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        public ValueExceptionSource Source
        {
            get { return _source; }
        }
    }
}
