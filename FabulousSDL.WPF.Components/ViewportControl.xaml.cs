using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace FabulousSDL.WPF.Components
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ViewportControl: UserControl
    {
        private bool _hasLoaded = false;
        private bool _hasInitialized = false;

        private ViewportHost _viewportHost;

        public ViewportControl()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!_hasLoaded)
                _hasLoaded = true;

            if (!_hasInitialized && IsValidSize(ViewportCanvas.ActualWidth, 
                                               ViewportCanvas.ActualHeight))
                InitializeViewport();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_hasLoaded || _hasInitialized || !IsValidSize(e.NewSize))
                return;

            InitializeViewport();
            _hasInitialized = true;
            SizeChanged -= OnSizeChanged;
        }

        private static bool IsValidSize(Size size) => IsValidSize(size.Width, size.Height);
        private static bool IsValidSize(double width, double height) => width > 0.0 && height > 0.0;

        private void InitializeViewport()
        {
            _viewportHost = new ViewportHost(ViewportCanvas.ActualWidth, ViewportCanvas.ActualHeight);
            ViewportCanvas.Child = _viewportHost;

            _viewportHost.MessageHook += new HwndSourceHook(ControlMsgFilter);
        }

        private IntPtr ControlMsgFilter(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = false;
            return IntPtr.Zero;
        }
    }
}