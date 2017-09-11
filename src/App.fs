module Main

open Fable.Core
open Fable.Core.JsInterop

open Js
open Math
open Tools

type Ship = 
    {
        Scale: float
        Position: Point
        Velocity: Vector
        Acceleration: float
        Rotation: float
        RotationSpeed: float
    }
    static member New x y = { 
        Scale = 10.0
        Position = (x, y); Velocity = (0.0, 0.0); Acceleration = 0.0
        Rotation = 0.0; RotationSpeed = 0.0
    }


type State = {
    Context: Browser.Context
    Size: float * float
    Timestamp: float
    Ship: Ship
}

let paintShip ship (context: Browser.Context) = 
    let { Scale = s; Position = o; Rotation = r } = ship
    let z = 0.0, 0.0
    let polygon = 
        [ 0.0; 120.0; 240.0; 0.0 ]
        |> List.map (fun a -> z |> move (polar 1.0 (rad a)) |> scale s |> rotate r |> move o)

    let paintPoly points = 
        let rec poly points first = 
            match points with
            | [] -> context.fill (); context.stroke ()
            | p :: tail -> 
                p |> if first then context.moveTo else context.lineTo
                poly tail false
        poly points true

    context.strokeStyle <- !^ "rgb(0,255,255)"
    context.fillStyle <- !^ "rgb(255,255,0)"
    paintPoly polygon

    ()

let init canvas timestamp = 
    let context = Browser.contextOf canvas
    let w, h = canvas.width, canvas.height
    { Context = context; Size = (w, h); Timestamp = timestamp; Ship = Ship.New (w / 2.) (h / 2.) }

let render _ (model: State) = 
    let context, (w, h) = model.Context, model.Size
    Canvas.clear w h context
    context |> paintShip model.Ship

let update model event timestamp = 
    match event with
    | "engine0" -> { model with Ship = Ship.engineOn model.Ship }
    | "engine1" -> { model with Ship = Ship.engineOff model.Ship }
    | "tick" -> { model with Ship = Ship.tick model.Ship }

let initialize () = 
    let push = Game.start init render update "tick"
    Browser.onKeyDown (fun e -> 
        match int e.keyCode with
        | 38 -> Some "engine1" // up
        | 40 -> Some "engine0" // dn
        | _ -> None
        |> Option.iter push
    )

Browser.onLoad (fun _ -> initialize ())
