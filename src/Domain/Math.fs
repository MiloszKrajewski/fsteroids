namespace Fsteroids.Domain

open System

type Point = float * float
type Vector = float * float

module Math = 
    let inline sin a = Math.Sin(a)
    let inline cos a = Math.Cos(a)
    let pi = Math.PI
    let rad a = a * pi / 180.0
    let rotate (a: float) (x, y) = let sina, cosa = sin a, cos a in (x * cosa - y * sina, x * sina + y * cosa)
    let move ((dx, dy): Vector) (x, y) = (x + dx, y + dy)
    let scale (s: float) (x, y) = (x * s, y * s)
    let polar d a = rotate a (d, 0.0)

