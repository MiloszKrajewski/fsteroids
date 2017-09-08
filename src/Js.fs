namespace Js

open Fable.Core
open Fable.Core.JsInterop

module Browser = 
    open Fable.Import

    type Canvas = Browser.HTMLCanvasElement
    type Context = Browser.CanvasRenderingContext2D

    let inline elementById<'a when 'a :> Browser.HTMLElement> id = 
        Browser.document.getElementById(id) :?> 'a
    let inline canvasById id = elementById<Canvas> id
    let inline contextOf (canvas: Canvas): Context = canvas.getContext_2d ()
    let inline width () = Browser.window.innerWidth
    let inline height () = Browser.window.innerHeight
    let inline timestamp () = Browser.performance.now ()
    let inline onLoad handler = Browser.window.addEventListener_load(fun e -> handler e; null)
    let inline requestFrame handler = Browser.window.requestAnimationFrame(fun e -> handler e) |> ignore
    let inline onKeyDown handler = Browser.window.addEventListener_keydown(fun e -> handler e; null)
    let inline onKeyUp handler = Browser.window.addEventListener_keyup(fun e -> handler e; null)
    // let onKeyPress handler = Browser.window.addEventListener_keypress(fun e -> handler e; null)

module Canvas = 
    let black = "rgb(0,0,0)"
    let white = "rgb(255,255,255)"

    let rect x y w h c (context: Browser.Context) = 
        context.fillStyle <- !^ c
        context.fillRect (x, y, w, h)

    let clear w h (context: Browser.Context) = 
        context |> rect 0.0 0.0 w h black
