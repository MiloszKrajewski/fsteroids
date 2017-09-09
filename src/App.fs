module Main

open Fable.Core
open Fable.Core.JsInterop

open Js
open Math
open Tools

type Ship = 
    {
        Position: Point
        Velocity: Vector
        Acceleration: float
        Rotation: float
        RotationSpeed: float
    }
    static member New x y = 
        { Position = (x, y); Velocity = (0., 0.); Acceleration = 0.; Rotation = 0.; RotationSpeed = 0. }


type State = {
    Context: Browser.Context
    Size: float * float
    Timestamp: float
    Ship: Ship
}

let paintShip ship (context: Browser.Context) = 
    let { Position = o; Rotation = r } = ship
    let z = 0.0, 0.0
    let polygon = 
        [| 0.0; 120.0; 240.0 |]
        |> Array.map (fun a -> z |> move (polar 1.0 (rad a)) |> scale 20.0 |> rotate r |> move o)
        |> List.ofArray

    let poly points = 
        let rec poly points first = 
            match points with
            | [] -> context.fill ()
            | p :: tail -> 
                p |> if first then context.moveTo else context.lineTo
                poly tail false
        poly points true

    context.fillStyle <- !^ "rgb(255,255,0)"
    poly polygon

    ()


let init canvas timestamp = 
    let context = Browser.contextOf canvas
    let w, h = canvas.width, canvas.height
    { Context = context; Size = (w, h); Timestamp = timestamp; Ship = Ship.New (w / 2.) (h / 2.) }

let render _ (model: State) = 
    let context, (w, h) = model.Context, model.Size
    Canvas.clear w h context
    context |> paintShip model.Ship

let update model event timestamp =  model

let initialize () = 
    let push = Game.create init render update "tick"
    push "hello"

// Browser.onLoad (fun _ -> initialize ())

let mutable c = 0
let i () = c <- c + 1; c
let a = [| i (); i (); i () |]
a |> Array.map string |> ignore
printfn "c: %d" c
