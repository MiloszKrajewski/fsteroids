Push-Location "$PSScriptRoot\src"
try {
    & dotnet fable yarn-run build
}
finally {
    Pop-Location
}
