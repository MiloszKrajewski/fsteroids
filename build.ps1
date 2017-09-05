Push-Location "$PSScriptRoot\src"
try {
    & yarn
    & dotnet restore
    & dotnet fable yarn-run build
}
finally {
    Pop-Location
}
