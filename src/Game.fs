module Game

open Fable.Core
open Fable.Core.JsInterop
open Tools


let create init render update tick = 
    let canvas = Browser.canvasById "canvas"
    canvas.width <- Browser.width ()
    canvas.height <- Browser.height ()

    let zero = Browser.timestamp ()
    let mutable state = init canvas zero
    let push event = state <- update state event (Browser.timestamp () - zero)

    let rec refresh _ = 
        push tick
        render canvas state
        Browser.requestFrame refresh

    Browser.requestFrame refresh

    push