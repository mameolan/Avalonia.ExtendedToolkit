using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from: https://github.com/punker76/MahApps.Metro.SimpleChildWindow/ 

    /// <summary>
    /// A static class to show ChildWindow's
    /// </summary>
    public static class ChildWindowManager
    {
        /// <summary>
        /// Shows the given child window on the MetroWindow dialog container in an asynchronous way.
        /// </summary>
        /// <param name="window">The owning window with a container of the child window.</param>
        /// <param name="dialog">A child window instance.</param>
        /// <param name="overlayFillBehavior">The overlay fill behavior.</param>
        /// <returns>
        /// A task representing the operation.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The provided child window can not add, the container can not be found.
        /// or
        /// The provided child window is already visible in the specified window.
        /// </exception>
        public static Task ShowChildWindowAsync(this Window window, ChildWindow dialog, OverlayFillBehavior overlayFillBehavior = OverlayFillBehavior.WindowContent)
        {
            return window.ShowChildWindowAsync<object>(dialog, overlayFillBehavior);
        }

        /// <summary>
        /// Shows the given child window on the MetroWindow dialog container in an asynchronous way.
        /// When the dialog was closed it returns a result.
        /// </summary>
        /// <param name="window">The owning window with a container of the child window.</param>
        /// <param name="dialog">A child window instance.</param>
        /// <param name="overlayFillBehavior">The overlay fill behavior.</param>
        /// <returns>
        /// A task representing the operation.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The provided child window can not add, the container can not be found.
        /// or
        /// The provided child window is already visible in the specified window.
        /// </exception>
        public static async Task<TResult> ShowChildWindowAsync<TResult>(this Window window, ChildWindow dialog, OverlayFillBehavior overlayFillBehavior = OverlayFillBehavior.WindowContent)
        {
            var tcs = new TaskCompletionSource<TResult>();

            //window.Dispatcher.VerifyAccess();

            Dispatcher.UIThread.VerifyAccess();

            var metroDialogContainer = (window.Template as IControl).FindControl<Grid>("PART_MetroActiveDialogContainer");
            metroDialogContainer = metroDialogContainer ?? (window.Template as IControl).FindControl<Grid>("PART_MetroInactiveDialogsContainer");
            if (metroDialogContainer == null)
            {
                throw new InvalidOperationException("The provided child window can not add, there is no container defined.");
            }

            if (metroDialogContainer.Children.Contains(dialog))
            {
                throw new InvalidOperationException("The provided child window is already visible in the specified window.");
            }

            if (overlayFillBehavior == OverlayFillBehavior.WindowContent)
            {
                metroDialogContainer.SetValue(Grid.RowProperty, (int)metroDialogContainer.GetValue(Grid.RowProperty) + 1);
                metroDialogContainer.SetValue(Grid.RowSpanProperty, 1);
            }

            return await OpenDialogAsync(dialog, metroDialogContainer, tcs);
        }

        /// <summary>
        /// Shows the given child window on the given container in an asynchronous way.
        /// When the dialog was closed it returns a result.
        /// </summary>
        /// <param name="window">The owning window with a container of the child window.</param>
        /// <param name="dialog">A child window instance.</param>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        /// The provided child window can not add, there is no container defined.
        /// or
        /// The provided child window is already visible in the specified window.
        /// </exception>
        public static Task ShowChildWindowAsync(this Window window, ChildWindow dialog, Panel container)
        {
            return window.ShowChildWindowAsync<object>(dialog, container);
        }

        /// <summary>
        /// Shows the given child window on the given container in an asynchronous way.
        /// </summary>
        /// <param name="window">The owning window with a container of the child window.</param>
        /// <param name="dialog">A child window instance.</param>
        /// <param name="container">The container.</param>
        /// <returns />
        /// <exception cref="System.InvalidOperationException">
        /// The provided child window can not add, there is no container defined.
        /// or
        /// The provided child window is already visible in the specified window.
        /// </exception>
        public static async Task<TResult> ShowChildWindowAsync<TResult>(this Window window, ChildWindow dialog, Panel container)
        {
            var tcs = new TaskCompletionSource<TResult>();

            //window.Dispatcher.VerifyAccess();

            Dispatcher.UIThread.VerifyAccess();

            if (container == null)
            {
                throw new InvalidOperationException("The provided child window can not add, there is no container defined.");
            }

            if (container.Children.Contains(dialog))
            {
                throw new InvalidOperationException("The provided child window is already visible in the specified window.");
            }

            return await OpenDialogAsync(dialog, container, tcs);//.ConfigureAwait(true);
        }

        private static async Task<TResult> OpenDialogAsync<TResult>(ChildWindow dialog, Panel container, TaskCompletionSource<TResult> tcs)
        {
            container.Children.Add(dialog);

            void OnDialogClosingFinished(object sender, EventArgs args)
            {
                dialog.ClosingFinished -= OnDialogClosingFinished;
                container.Children.Remove(dialog);
                tcs.TrySetResult(dialog.ChildWindowResult is TResult result ? result :
                (dialog.ClosedBy is TResult closedBy ? closedBy : default));
            }

            dialog.ClosingFinished += OnDialogClosingFinished;

            //dialog.SetValue(ChildWindow.IsOpenProperty, true);
            //await Dispatcher.UIThread.InvokeAsync(()=> dialog.IsOpen = true).ConfigureAwait(true);

            dialog.IsOpen = true;

            return await tcs.Task;//.ConfigureAwait(true);
        }
    }
}
