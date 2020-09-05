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
        private ViewportHost _viewportHost;

        public ViewportControl()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewportHost = new ViewportHost(ViewportCanvas.ActualHeight, 
                                             ViewportCanvas.ActualWidth);
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