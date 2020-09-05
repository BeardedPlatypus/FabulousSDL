namespace FabulousSDL.WPF

open Xamarin.Forms.Platform.WPF

open FabulousSDL
open FabulousSDL.WPF.Components

type ViewportRenderer() =
    inherit ViewRenderer<Viewport, ViewportControl>()

    override this.OnElementChanged(e: ElementChangedEventArgs<Viewport>) = 
        base.OnElementChanged(e)

        match this.Control with
        | null -> 
            this.SetNativeControl(ViewportControl())
        | _ ->
            do ()


module Dummy_ViewPortRenderer = 
    [<assembly: ExportRenderer(typeof<FabulousSDL.Viewport>, typeof<ViewportRenderer>)>]
    do ()

