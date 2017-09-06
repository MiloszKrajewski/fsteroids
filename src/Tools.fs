namespace Tools

module Browser = 
    open Fable.Import

    type Canvas = Browser.HTMLCanvasElement
    type Context = Browser.CanvasRenderingContext2D

    let inline elementById<'a when 'a :> Browser.HTMLElement> id = Browser.document.getElementById(id) :?> 'a
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


module Option =
    let inline def v o = defaultArg o v

module Map =
    let updateOrAdd key update map = map |> Map.add key (map |> Map.tryFind key |> update)