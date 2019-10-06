using Avalonia.Controls.Primitives;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using System.Linq;

namespace Avalonia.ExtendedToolkit.Extensions
{
    public static class MetroWindowExtensions
    {
        public static void HandleWindowCommandsForFlyouts(this MetroWindow window,  IEnumerable<Flyout> flyouts, Brush resetBrush = null)
        {
            var allOpenFlyouts = flyouts.Where(x => x.IsOpen);

            var anyFlyoutOpen = allOpenFlyouts.Any(x => x.Position != ExtendedToolkit.Position.Bottom);

            if (!anyFlyoutOpen)
            {
                if (resetBrush == null)
                {
                    window.ResetAllWindowCommandsBrush();
                }
                else
                {
                    window.ChangeAllWindowCommandsBrush(resetBrush);
                }
            }
            var topFlyout = allOpenFlyouts
                            .Where(x => x.Position == ExtendedToolkit.Position.Top)
                            .OrderByDescending(x => x.ZIndex)
                            .FirstOrDefault();
            if (topFlyout != null)
            {
                window.UpdateWindowCommandsForFlyout(topFlyout);
            }
            else
            {
                var leftFlyout = allOpenFlyouts
                                 .Where(x => x.Position == ExtendedToolkit.Position.Left)
                                 .OrderByDescending(x => x.ZIndex)
                                 .FirstOrDefault();
                if (leftFlyout != null)
                {
                    window.UpdateWindowCommandsForFlyout(leftFlyout);
                }

                var rightFlyout = allOpenFlyouts
                                  .Where(x => x.Position == ExtendedToolkit.Position.Right)
                                  .OrderByDescending(x => x.ZIndex)
                                  .FirstOrDefault();
                if (rightFlyout != null)
                {
                    window.UpdateWindowCommandsForFlyout(rightFlyout);
                }
            }
        }

        private static bool NeedLightTheme(this IBrush brush)
        {
            if (brush == null)
            {
                return true;
            }

            // calculate brush color lightness
            var color = ((SolidColorBrush)brush).Color;

            var r = color.R / 255.0f;
            var g = color.G / 255.0f;
            var b = color.B / 255.0f;

            var max = r;
            var min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            var lightness = (max + min) / 2;

            return lightness > 0.1;
        }

        public static void ResetAllWindowCommandsBrush(this MetroWindow window)
        {
            window.ChangeAllWindowCommandsBrush(window.OverrideDefaultWindowCommandsBrush);
            window.ChangeAllWindowButtonCommandsBrush(window.OverrideDefaultWindowCommandsBrush);
        }

        public static void UpdateWindowCommandsForFlyout(this MetroWindow window, Flyout flyout)
        {
            window.ChangeAllWindowButtonCommandsBrush(flyout.Foreground, flyout.Position);
        }

        private static void ChangeAllWindowCommandsBrush(this MetroWindow window, Brush brush)
        {
            // set the theme based on color lightness
            var theme = brush.NeedLightTheme() ? WindowCommandTheme.Light : WindowCommandTheme.Dark;

            // set the theme to light by default
            window.LeftWindowCommands?.SetValue(WindowCommands.ThemeProperty, theme);
            window.RightWindowCommands?.SetValue(WindowCommands.ThemeProperty, theme);

            // clear or set the foreground property
            if (brush != null)
            {
                window.LeftWindowCommands?.SetValue(TemplatedControl.ForegroundProperty, brush);
                window.RightWindowCommands?.SetValue(TemplatedControl.ForegroundProperty, brush);
            }
            else
            {
                window.LeftWindowCommands?.ClearValue(TemplatedControl.ForegroundProperty);
                window.RightWindowCommands?.ClearValue(TemplatedControl.ForegroundProperty);
            }
        }

        private static void ChangeAllWindowButtonCommandsBrush(this MetroWindow window, IBrush brush, Position position = Position.Top)
        {
            // set the theme to light by default
            if (position == Position.Right || position == Position.Top)
            {
                // set the theme based on color lightness
                var theme = brush.NeedLightTheme() ? WindowCommandTheme.Light : WindowCommandTheme.Dark;

                window.WindowButtonCommands?.SetValue(WindowButtonCommands.ThemeProperty, theme);

                // clear or set the foreground property
                if (brush != null)
                {
                    window.WindowButtonCommands?.SetValue(TemplatedControl.ForegroundProperty, brush);
                }
                else
                {
                    window.WindowButtonCommands?.ClearValue(TemplatedControl.ForegroundProperty);
                }
            }
        }
    }
}