Push-Location "$PSScriptRoot"
try {
    & ./.paket/paket.exe $args
}
finally {
    Pop-Location
}
