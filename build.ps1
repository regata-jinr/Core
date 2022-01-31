<#
    .SYNOPSIS
    REGATA.CORE build script
    .DESCRIPTION
    Here is the tool that provides the simple way to build, test and pack projects recursively
    in order to gather all out assemblies and packages into the one place depends on configuration.
    .PARAMETER Release
     Release configuration for the build command.
    .PARAMETER Debug
     Debug configuration for the build command.(By default)
    .PARAMETER Test
     Build and Run test projects
     .PARAMETER $IgnoreTest
     Allows to ignore running test for release configuration
    .PARAMETER Pack
     Nuget pack command after building.
     Should be configured in csproj file separately.
    .PARAMETER projName
     Will be passed to fiter options of gci with *.csproj
    .EXAMPLE
    In order to build all projects with *win* pattern in name, run command:
    build -Name win
    #>
param 
(
    [switch]$Release,
    [switch]$Debug,
    [switch]$Test,
    [switch]$Pack,
    [switch]$IgnoreTest,
    [string]$Name
)

$od = "artifacts"
$pd = "packages"

function GetProjects {
    param 
    ([switch]$Test)
    
    if ($Test) {
        gci -Recurse -Path .\tests -Filter $Name*.csproj
    }
    else {
    }
    gci -Recurse -Path src -Filter $Name*.csproj -Exclude test
}


function RunTest {
    GetProjects -Test | ForEach-Object { dotnet test $_; }
}

function RunBuild {
    param ($config)

    GetProjects | ForEach-Object { dotnet build $_ -c $config -f net5.0-windows -o ([System.IO.Path]::Combine($od, $config, [System.IO.Path]::GetFileNameWithoutExtension($_))); }
}

function RunPack {
 GetProjects | ForEach-Object { nuget pack $_ -Build -Symbols -Verbosity Quiet -Properties Configuration=Release}
}
 
if ($Release) {
    RunBuild -config Release
    if (!$IgnoreTest) {RunTest}
}
elseif ($Debug) {
    RunBuild -config Debug
}
elseif ($Test) {
    RunTest
}
elseif ($Pack) {
    RunPack
}
else {
    RunBuild -config Debug
}

