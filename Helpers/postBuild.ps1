# Post build script to update package nuspec file and deploy nuget
# Place in project root folder
# nuget.exe must be accessible in your PATH system variable
# Custom nuget repo must be setup
# Place in project post build property: 
# powershell -NoProfile -ExecutionPolicy RemoteSigned -file $(ProjectDir)postBuild.ps1 -projPath $(ProjectDir) -config "$(ConfigurationName)" -projFileName "$(ProjectFileName)" -projName "$(ProjectName)"

Param(
    [string]$projPath,
    [string]$config,
	[string]$projFileName,
	[string]$projName
)

# $projPath = "C:\Projects\Helpers\Helpers\"
# $config = "Release"
# $projFileName = "Helpers.csproj"
# $projName = "Helpers"

$assemblyPath = $projPath + "Properties\AssemblyInfo.cs"
Write-Output "Post Build Started"
Write-Output "Project Path: $projPath"
Write-Output "Configuration: $config"
Write-Output "Project File Name: $projFileName"
Write-Output "Project Name: $projName"
Write-Output "Assembly Path: $assemblyPath"

#Exits if the build configuration is not release
if ($config -ne "Release")
{
	Write-Output "Not building for release configuration. Exiting."
    exit 0
}

#changes the directory to the project path
Set-Location $projPath
Write-Output "Current Directory: $pwd"

#Gets the assembly version in the assembly file
$pattern = '\[assembly: AssemblyVersion\("(.*)"\)\]'
(Get-Content $assemblyPath) | ForEach-Object{
     if($_ -match $pattern){
         $version = $matches[1]
		 Write-Output "Version:" $version
     }
 }
 
 # Checks if file version is an empty string
 If([string]::IsNullOrEmpty($version)){
	throw [System.Exception] "No version found in AssemblyInfo file."
 }

 #Creates a nupkg file
 nuget pack $projFileName -Properties Configuration=Release;

 #Pushes the nupkg file to the custom nuget source
 $nugetPackFilename = $projName + "." + $version + ".nupkg"
 if ([System.IO.File]::Exists("$pwd\$nugetPackFilename") -ne $true)  {
     Write-Output "Could not find file $nugetPackFilename ! Nuget build likely failed, check output for details."
 }
 else {     
     nuget push $nugetPackFilename -Source "Frazier"
 }
 
 Write-Output "Post Build Finished"
