module Math

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open System

let inline sin a = Math.Sin(a)
let inline cos a = Math.Cos(a)
let rotate a (x, y) = let sina, cosa = sin a, cos a in (x * cosa - y * sina, x * sina + y * cosa)
let translate (dx: float) (dy: float) (x, y) = (x + dx, y + dy)
let scale (s: float) (x, y) = (x * s, y * s)