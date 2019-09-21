using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public partial class MetroWindow : Window
    {
        private const string PART_Icon = "PART_Icon";
        private const string PART_TitleBar = "PART_TitleBar";
        private const string PART_WindowTitleBackground = "PART_WindowTitleBackground";
        private const string PART_WindowTitleThumb = "PART_WindowTitleThumb";
        private const string PART_FlyoutModalDragMoveThumb = "PART_FlyoutModalDragMoveThumb";
        private const string PART_LeftWindowCommands = "PART_LeftWindowCommands";
        private const string PART_RightWindowCommands = "PART_RightWindowCommands";
        private const string PART_WindowButtonCommands = "PART_WindowButtonCommands";
        private const string PART_OverlayBox = "PART_OverlayBox";
        private const string PART_MetroActiveDialogContainer = "PART_MetroActiveDialogContainer";
        private const string PART_MetroInactiveDialogsContainer = "PART_MetroInactiveDialogsContainer";
        private const string PART_FlyoutModal = "PART_FlyoutModal";
        private const string PART_Content = "PART_Content";

        Image icon;
        Control titleBar;
        Control titleBarBackground;
        Thumb windowTitleThumb;
        Thumb flyoutModalDragMoveThumb;
        private IInputElement restoreFocus;
        internal ContentPresenter LeftWindowCommandsPresenter;
        internal ContentPresenter RightWindowCommandsPresenter;
        internal ContentPresenter WindowButtonCommandsPresenter;

        internal Grid overlayBox;
        internal Grid metroActiveDialogContainer;
        internal Grid metroInactiveDialogContainer;
        //private Storyboard overlayStoryboard;
        Rectangle flyoutModal;


        private Grid _titleBar;

        private Grid _bottomHorizontalGrip;
        private Grid _bottomLeftGrip;
        private Grid _bottomRightGrip;
        private Grid _leftVerticalGrip;
        private Grid _rightVerticalGrip;
        private Grid _topHorizontalGrip;
        private Grid _topLeftGrip;
        private Grid _topRightGrip;

        private Button _closeButton;
        private Button _minimiseButton;
        private Button _restoreButton;



        private Image _icon;

        private bool _mouseDown;
        private Point _mouseDownPosition;
    }
}
