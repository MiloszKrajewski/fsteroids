module Main

open Fable.Core
open Fable.Core.JsInterop
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

let black = !^ "rgb(0,0,0)"
let white = !^ "rgb(255,255,255)"
let init canvas timestamp = 
    let context = Browser.contextOf canvas
    let w, h = canvas.width, canvas.height
    { Context = context; Size = (w, h); Timestamp = timestamp; Ship = Ship.New (w / 2.) (h / 2.) }

let render _ (model: State) = 
    let context, (w, h) = model.Context, model.Size
    context.fillStyle <- black
    context.fillRect (0., 0., w, h)
    context.fillStyle <- white
    let (x, y) = model.Ship.Position
    context.fillRect (x - 5., y - 5., 10., 10.)

let update model event timestamp =  model

let initialize () = 
    let push = Game.create init render update "tick"
    push "hello"

Browser.onLoad (fun _ -> initialize ())