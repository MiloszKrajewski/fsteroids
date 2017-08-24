Push-Location "$PSScriptRoot\src"
try {
    & dotnet fable yarn-run start
}
finally {
    Pop-Location
}
