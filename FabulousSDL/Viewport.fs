namespace FabulousSDL

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

type Viewport() =
    inherit View()

[<AutoOpen>]
module FabulousViewport = 
    type Fabulous.XamarinForms.View with
        static member inline Viewport() =
            let attribs = ViewBuilders.BuildView(0)

            let update registry (prevOpt: ViewElement voption) (source: ViewElement) (target: Viewport) =
                         ViewBuilders.UpdateView(registry, prevOpt, source, target)

            ViewElement.Create(Viewport, update, attribs)
