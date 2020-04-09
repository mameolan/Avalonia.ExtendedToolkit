using System;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Design
{
    /// <summary>
    /// Specialized UI container for a property entry.
    /// </summary>
    public class PropertyContainer : GridEntryContainer
    {
        public new Type StyleKey => typeof(PropertyContainer);

        /// <summary>
        /// Initializes the <see cref="PropertyContainer"/> class.
        /// </summary>
        static PropertyContainer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyContainer"/> class.
        /// </summary>
        public PropertyContainer()
        {
            SetParentContainer(this, this);

            //PreviewMouseDown+=(sender, args) =>  MessageBox.Show(Entry.Name);
        }
    }
}
