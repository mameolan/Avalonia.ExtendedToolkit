using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class SliderEx : RangeBase
    {

        public SliderEx()
        {
            InitializeCommands();

            Minimum = 0.0d;
            Maximum = 10.0d;
            Value = 0;

            Thumb.DragStartedEvent.AddClassHandler<SliderEx>((o, e) => OnThumbDragStarted(o, e));
            Thumb.DragDeltaEvent.AddClassHandler<SliderEx>((o, e) => OnThumbDragDelta(o, e));
            Thumb.DragCompletedEvent.AddClassHandler<SliderEx>((o, e) => OnThumbDragCompleted(o, e));




        }

        #region Commands
        private static ICommand IncreaseLargeCommand { get; set; }
        private static ICommand IncreaseSmallCommand { get; set;}
        private static ICommand DecreaseLargeCommand { get; set;}
        private static ICommand DecreaseSmallCommand { get; set;}
        private static ICommand MinimizeValueCommand { get; set;}
        private static ICommand MaximizeValueCommand { get; set; }

        static void InitializeCommands()
        {
            IncreaseLargeCommand = ReactiveCommand.Create<object>(x => OnIncreaseLargeCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            IncreaseSmallCommand = ReactiveCommand.Create<object>(x => OnIncreaseSmallCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            
            DecreaseLargeCommand = ReactiveCommand.Create<object>(x => OnDecreaseLargeCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            DecreaseSmallCommand = ReactiveCommand.Create<object>(x => OnDecreaseSmallCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            MinimizeValueCommand = ReactiveCommand.Create<object>(x => OnMinimizeValueCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            MaximizeValueCommand = ReactiveCommand.Create<object>(x => OnMaximizeValueCommand(x), outputScheduler: RxApp.MainThreadScheduler);
        }

        private static object OnMinimizeValueCommand(object x)
        {
            throw new NotImplementedException();
        }

        private static object OnMaximizeValueCommand(object x)
        {
            throw new NotImplementedException();
        }


        private static object OnDecreaseSmallCommand(object x)
        {
            throw new NotImplementedException();
        }

        private static object OnIncreaseSmallCommand(object x)
        {
            throw new NotImplementedException();
        }

        private static object OnDecreaseLargeCommand(object x)
        {
            throw new NotImplementedException();
        }

        private static object OnIncreaseLargeCommand(object x)
        {
            throw new NotImplementedException();
        }

        #endregion







        private void OnThumbDragCompleted(SliderEx sliderEx, VectorEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void OnThumbDragDelta(SliderEx sliderEx, VectorEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void OnThumbDragStarted(SliderEx sliderEx, VectorEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
