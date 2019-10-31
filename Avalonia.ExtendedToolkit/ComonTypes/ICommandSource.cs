using Avalonia.Input;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public interface ICommandSource
    {
        ICommand Command { get; }
        object CommandParameter { get; }
        IInputElement CommandTarget { get; }
    }
}