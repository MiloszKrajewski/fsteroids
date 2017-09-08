module Game

open Fable.Core
open Fable.Core.JsInterop
open Tools
open Js


let create init render update tick = 
    let canvas = Browser.canvasById "canvas"
    canvas.width <- Browser.width ()
    canvas.height <- Browser.height ()

    let now = Browser.timestamp
    let zero = now ()
    let mutable state = init canvas zero
    let push event = state <- update state event (now () - zero)

    let rec refresh _ = 
        push tick
        render canvas state
        Browser.requestFrame refresh

    Browser.requestFrame refresh

    push