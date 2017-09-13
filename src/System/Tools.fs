namespace System

module Option =
    let inline def v o = defaultArg o v
    let inline alt f o = match o with | Some r -> r | _ -> f ()

module Map =
    let updateOrAdd key update map = map |> Map.add key (map |> Map.tryFind key |> update)