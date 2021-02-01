    <#
    .SYNOPSIS
    REGATA.CORE build system
    .DESCRIPTION
    Here is the tool that provide the simple way to build and test plenty project in order to gather all out assemblies into the one place depends on configuration.
    .PARAMETER Release
     Release configuratoin for the build command.
    .PARAMETER Debug
     Debug configuratoin for the build command.(By default)
    .PARAMETER Test
     Build and Run test projects
    .PARAMETER projName
     Will be passed to fiter options of gci with *.csproj
    .EXAMPLE
     
    #>
    param 
    (
         [switch]$Release,
         [switch]$Debug,
         [switch]$Test,
         [string]$Name
    )


$od = "artifacts"

function GetProjects
{
    param 
    ([switch]$Test)
    
    if ($Test)
    {
        gci -Recurse -Path .\tests -Filter $Name*.csproj
    }
    else 
    {
    }
        gci -Recurse -Path src -Filter $Name*.csproj -Exclude test
}


function RunTest
{
   GetProjects -Test | ForEach-Object {dotnet test $_;}
}

function RunBuild
{
    param ($config)

    GetProjects | ForEach-Object {dotnet build $_ -c $config -o ([System.IO.Path]::Combine($od, $config, [System.IO.Path]::GetFileNameWithoutExtension($_)));}

}
 

    


    if ($Release)
    {
         RunBuild -config Release
         RunTest
    }
    elseif ($Debug)
    {
        RunBuild -config Debug
    }
    elseif ($Test)
    {
        RunTest
    }
    else 
    {
        RunBuild -config Debug
    }




