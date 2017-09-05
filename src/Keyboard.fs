module Keyboard

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import
open Tools

let keyDown handler = Browser.window.addEventListener_keydown(fun e -> handler e; null)
let keyUp handler = Browser.window.addEventListener_keyup(fun e -> handler e; null)
let keyPress handler = Browser.window.addEventListener_keypress(fun e -> handler e; null)

let mutable private state = Set.empty
let mutable private counters = Map.empty

let private update press (event: Browser.KeyboardEvent) =
    let code = int event.keyCode
    if press && state |> Set.contains code |> not then 
        counters <- counters |> Map.updateOrAdd code (Option.def 0 >> (+) 1)
    state <- state |> (if press then Set.add else Set.remove) code

let isDown code = state |> Set.contains code

let initialize () =
    keyDown (update true)
    keyUp (update false)
