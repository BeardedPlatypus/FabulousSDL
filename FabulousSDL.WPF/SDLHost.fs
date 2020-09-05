namespace FabulousSDL.WPF

open System
open System.Runtime.InteropServices
open System.Windows.Interop

module public SDLHost =
    [<DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)>]
    extern IntPtr CreateWindowEx(int dwExStyle,
                                 string lpszClassName,
                                 string lpszWindowName,
                                 int style,
                                 int x, int y,
                                 int width, int height,
                                 IntPtr hwndParent,
                                 IntPtr hMenu,
                                 IntPtr hInst,
                                 [<MarshalAs(UnmanagedType.AsAny)>] Object pvParam);


    [<DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)>]
    extern bool DestroyWindow(IntPtr hwnd)

    type public T (height: double, width: double) =
        inherit HwndHost()
        
        let wsChild = 0x40000000
        let wsVisible = 0x10000000
        let hostId = 0x00000002
        let wmErasebkgnd = 0x0014

        let hostHeight = (int) height
        let hostWidth = (int) width

        let mutable hwndHost = IntPtr.Zero
        let mutable viewport : FabulousSDL.Viewport.Shell.Interop.view option = None

        override this.BuildWindowCore (hwndParent: HandleRef) : HandleRef =
            hwndHost <- CreateWindowEx(0, 
                                      "static", 
                                      "", 
                                      wsChild ||| wsVisible, 
                                      0, 0, 
                                      hostWidth, hostHeight, 
                                      hwndParent.Handle, 
                                      IntPtr hostId, 
                                      IntPtr.Zero, 
                                      0)

            viewport <- Some <| new FabulousSDL.Viewport.Shell.Interop.view () 
            viewport.Value.initialise(hwndHost.ToPointer())
            viewport.Value.update ()

            HandleRef (this, hwndHost)

        override this.WndProc (hwnd: IntPtr, msg: int, wParam: IntPtr, lParam: IntPtr, handled: byref<bool>) : IntPtr =
            match (viewport, msg) with 
            | (Some vp, msgVal) when msgVal = wmErasebkgnd -> 
                vp.update ()
                handled <- true
            | _ -> 
                handled <- false
                
            IntPtr.Zero

        override SDLHost.DestroyWindowCore (hwnd: HandleRef) : unit =
            DestroyWindow(hwnd.Handle) |> ignore

