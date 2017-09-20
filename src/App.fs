namespace Fsteroids

open Fable.Core
open Fable.Core.JsInterop

module Main =
    open Fsteroids.Platform
    open Fsteroids.Domain
    open Fsteroids.Domain.Math

    let init (canvas: Canvas) timestamp =
        let w, h = canvas.width, canvas.height
        let c = w / 2.0, h / 2.0
        let ship = Ship.zero |> Ship.move c |> Ship.rotate 90.0
        { Size = (w, h); Timestamp = timestamp; Ship = Ship.zero |> Ship.move c; Keys = Set.empty }

    let render (canvas: Canvas) (model: State) =
        let context = canvas |> Browser.contextOf
        let w, h = model.Size
        Canvas.clear w h context
        context |> Ship.paint model.Ship

    let update model event timestamp = model

    let initialize () =
        let push = Game.start init render update Tick
        Browser.onKeyDown (fun e ->
            match int e.keyCode with
            | 38 -> Some Up | 40 -> Some Down | _ -> None
            |> Option.iter (KeyDown >> push)
        )

    Browser.onLoad (fun _ -> initialize ())
