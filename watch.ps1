Push-Location "$PSScriptRoot\src"
try {
    & yarn
    & dotnet restore
    & dotnet fable yarn-run start
}
finally {
    Pop-Location
}
