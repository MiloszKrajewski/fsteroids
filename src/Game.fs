module Game

open Fable.Core
open Fable.Core.JsInterop
open Tools
open Js

let sampling = 1000.0 / 60.0 // 60 fps

let start init render update tick = 
    let canvas = Browser.canvasById "game"
    canvas.width <- Browser.width ()
    canvas.height <- Browser.height ()

    let now = Browser.timestamp
    let zero = now ()

    let mutable state = init canvas zero
    let mutable stamp = zero

    let push now event = state <- update state event (now - zero)

    let rec sample until = 
        let next = stamp + sampling
        if next <= until then 
            stamp <- next
            push next tick
            sample until

    let rec refresh _ = 
        sample (now ())
        render canvas state
        Browser.requestFrame refresh

    Browser.requestFrame refresh

    push (now ())
