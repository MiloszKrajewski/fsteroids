namespace Fsteroids

open Fable.Core
open Fable.Core.JsInterop

module Main = 
    open Fsteroids.Platform
    open Fsteroids.Domain
    open Fsteroids.Domain.Math

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

    let init (canvas: Browser.Canvas) timestamp = 
        let w, h = canvas.width, canvas.height
        let c = w / 2.0, h / 2.0
        { Size = (w, h); Timestamp = timestamp; Ship = Ship.zero |> Ship.move c }

    let render (canvas: Browser.Canvas) (model: State) = 
        let context = canvas |> Browser.contextOf
        let w, h = model.Size
        Canvas.clear w h context
        context |> paintShip model.Ship

    let update model event timestamp = model
        

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
