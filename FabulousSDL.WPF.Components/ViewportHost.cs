using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace FabulousSDL.WPF.Components
{
    public class ViewportHost : HwndHost
    {
        #region Constant Interop Values
        internal const int WsChild = 0x40000000;
        internal const int WsVisible = 0x10000000;
        internal const int HostId = 0x00000002;
        internal const int WmErasebkgnd = 0x0014;
        #endregion

        private IntPtr _hwndHost;
        private Viewport.Shell.Interop.view _view;

        private readonly int _hostHeight;
        private readonly int _hostWidth;

        public ViewportHost(double width, double height)
        {
            _hostWidth = (int) width;
            _hostHeight = (int) height;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            _hwndHost = CreateWindowEx(0, "static", "",
                WsChild | WsVisible,
                0, 0,
                _hostWidth,
                _hostHeight,
                hwndParent.Handle,
                (IntPtr) HostId,
                IntPtr.Zero,
                0);

            _view = new Viewport.Shell.Interop.view();

            unsafe
            {
                _view.initialise(_hwndHost.ToPointer());
            }

            _view.update();
            return new HandleRef(this, _hwndHost);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            DestroyWindow(hwnd.Handle);
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WmErasebkgnd:
                    _view.update();
                    handled = true;
                    break;
                default:
                    handled = false;
                    break;
            }

            return IntPtr.Zero;
        }

        #region P/Invoke Declarations

        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(int dwExStyle,
            string lpszClassName,
            string lpszWindowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInst,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        #endregion
    }
}