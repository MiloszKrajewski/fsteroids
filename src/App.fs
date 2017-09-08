module Main

open Fable.Core
open Fable.Core.JsInterop
open Js
open Tools

type Value = float
type Point = float * float
type Vector = float * float

type Ship = 
    {
        Position: Point
        Velocity: Vector
        Acceleration: Value
        Rotation: Value
        RotationSpeed: Value
    }
    static member New x y = 
        { Position = (x, y); Velocity = (0., 0.); Acceleration = 0.; Rotation = 0.; RotationSpeed = 0. }


type State = {
    Context: Browser.Context
    Size: float * float
    Timestamp: float
    Ship: Ship
}

let paintShip (x, y) (context: Canvas.Context) = 
    let z = 0.0, 0.0
    let p = [0.0; 120.0; 240.0] |> List.map (fun a -> z |> movedd (rad a) 1.0 |> scale 50.0)


let init canvas timestamp = 
    let context = Browser.contextOf canvas
    let w, h = canvas.width, canvas.height
    { Context = context; Size = (w, h); Timestamp = timestamp; Ship = Ship.New (w / 2.) (h / 2.) }

let render _ (model: State) = 
    let context, (w, h) = model.Context, model.Size
    Canvas.clear w h context
    paintShip model.Ship.Position

let update model event timestamp =  model

let initialize () = 
    let push = Game.create init render update "tick"
    push "hello"

Browser.onLoad (fun _ -> initialize ())