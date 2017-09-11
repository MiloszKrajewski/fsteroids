Push-Location "$PSScriptRoot\src"
try {
    & dotnet fable $args
}
finally {
    Pop-Location
}
