namespace Tools

module Option =
    let inline def v o = defaultArg o v

module Map =
    let updateOrAdd key update map = map |> Map.add key (map |> Map.tryFind key |> update)
