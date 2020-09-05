namespace FabulousSDL.WPF

open System
open System.Runtime.InteropServices
open System.Windows.Interop

open Xamarin.Forms.Platform.WPF
open FabulousSDL

type ViewportRenderer() =
    inherit ViewRenderer<Viewport, System.Windows.Controls.Border>()

    let mutable sdlHost : SDLHost.T option = None

    override this.OnElementChanged(e: ElementChangedEventArgs<Viewport>) = 
        base.OnElementChanged(e)

        match (this.Control, sdlHost) with
        | (null, None) -> 
            this.SetNativeControl(System.Windows.Controls.Border())
            this.Control.Background <- System.Windows.Media.Brushes.Black

            sdlHost <- Some <| new SDLHost.T(this.Control.Width, this.Control.Height)

            this.Control.Child <- sdlHost.Value

            sdlHost.Value.add_MessageHook (new HwndSourceHook(fun (hwnd: IntPtr) (msg: int) (wParam: IntPtr) (lParam: IntPtr) (handled: byref<bool>) -> 
                handled <- false 
                IntPtr.Zero))
        | _ ->
            do ()


module Dummy_ViewPortRenderer = 
    [<assembly: ExportRenderer(typeof<FabulousSDL.Viewport>, typeof<ViewportRenderer>)>]
    do ()

