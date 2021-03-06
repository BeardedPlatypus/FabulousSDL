﻿namespace FabulousSDL.WPF

open Xamarin.Forms.Platform.WPF

open FabulousSDL
open FabulousSDL.WPF.Components

/// <summary>
/// <see cref="ViewportRenderer"/> provides the custom renderer implementation
/// to render the <see cref="Viewport"/> as a <see cref="ViewportControl"/>.
/// </summary>
type ViewportRenderer() =
    inherit ViewRenderer<Viewport, ViewportControl>()

    override this.OnElementChanged(e: ElementChangedEventArgs<Viewport>) = 
        base.OnElementChanged(e)

        match this.Control with
        | null -> 
            this.SetNativeControl(ViewportControl())
        | _ ->
            do ()

// Dummy module to ensure this renderer is exported and picked up by Xamarin.Forms
module Dummy_ViewPortRenderer = 
    [<assembly: ExportRenderer(typeof<FabulousSDL.Viewport>, typeof<ViewportRenderer>)>]
    do ()

