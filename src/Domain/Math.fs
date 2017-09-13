namespace Fsteroids.Domain

open System

module Math = 
    type Point = float * float
    type Vector = float * float

    let inline sin a = Math.Sin(a)
    let inline cos a = Math.Cos(a)
    let pi = Math.PI
    let rotate a (x, y) = let sina, cosa = sin a, cos a in (x * cosa - y * sina, x * sina + y * cosa)
    let move (dx: float, dy: float) (x, y) = (x + dx, y + dy)
    let scale (s: float) (x, y) = (x * s, y * s)
    let polar d a = rotate a (d, 0.0)
    let rad a = a * pi / 180.0