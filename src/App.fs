module Main

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

let init w h t = ()
let render canvas model = ()
let update model event timestamp = model

let initialize () = 
    let push = Game.create init render update "tick"
    push "hello"

Browser.window.addEventListener_load(fun e -> initialize () |> ignore |> box)
