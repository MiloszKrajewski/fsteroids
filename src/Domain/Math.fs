namespace Fsteroids.Domain

open System

type Point = float * float
type Vector = float * float

module Math =
    let inline sin a = Math.Sin(a)
    let inline cos a = Math.Cos(a)
    let pi = Math.PI
    let rad a = a * pi / 180.0
    let rec wrap max v =
        if v < 0.0 then wrap max (v + max)
        elif v > max then wrap max (v - max)
        else v
    let wrap360 = wrap 360.0
    let rec cap max v =
        if v < 0.0 then 0.0
        elif v > max then max
        else v


module Point =
    let rotate (a: float) (x, y) = let sina, cosa = sin a, cos a in (x * cosa - y * sina, x * sina + y * cosa)
    let move ((dx, dy): Vector) (x, y) = (x + dx, y + dy)
    let scale (s: float) (x, y) = (x * s, y * s)
    let polar d a = rotate a (d, 0.0)
