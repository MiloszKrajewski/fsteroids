module Game

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

let inline now () = Browser.performance.now () / 1000.0
let inline schedule handler = Browser.window.requestAnimationFrame(fun _ -> handler ()) |> ignore

let create init render update tick = 
    let canvas = Browser.document.getElementById("canvas") :?> Browser.HTMLCanvasElement
    canvas.width <- Browser.window.innerWidth
    canvas.height <- Browser.window.innerHeight

    let zero = now ()
    let mutable state = init canvas.width canvas.height zero
    let push event = state <- update state event (now () - zero)

    let rec frame () = 
        push tick
        render canvas state
        schedule frame

    schedule frame

    push
