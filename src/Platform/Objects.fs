namespace Fsteroids.Platform

open Fsteroids.Domain
open Fsteroids.Domain.Math

module Ship = 
    let paint ship (context: Context) = 
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
