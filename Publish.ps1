<#
.SYNOPSIS
    Publishes the PanoramicData.WhoIs NuGet package to nuget.org

.PARAMETER SkipTests
    Skip running unit tests before publishing

.EXAMPLE
    .\Publish.ps1
    .\Publish.ps1 -SkipTests
#>

param(
    [switch]$SkipTests
)

$ErrorActionPreference = 'Stop'

# Step 1: Check for git porcelain (uncommitted changes)
Write-Host "Checking for uncommitted changes..." -ForegroundColor Cyan
$gitStatus = git status --porcelain
if ($gitStatus) {
    Write-Error "Working directory is not clean. Please commit or stash your changes before publishing."
    exit 1
}
Write-Host "Working directory is clean." -ForegroundColor Green

# Step 2: Determine the Nerdbank git version
Write-Host "Determining version using Nerdbank.GitVersioning..." -ForegroundColor Cyan
$nbgvInstalled = Get-Command nbgv -ErrorAction SilentlyContinue
if (-not $nbgvInstalled) {
    Write-Error "nbgv tool is not installed. Install it with: dotnet tool install -g nbgv"
    exit 1
}

$versionJson = nbgv get-version --format json | ConvertFrom-Json
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to get version from Nerdbank.GitVersioning"
    exit 1
}

$version = $versionJson.NuGetPackageVersion
Write-Host "Version: $version" -ForegroundColor Green

# Step 3: Check that nuget-key.txt exists, has content and is gitignored
Write-Host "Checking nuget-key.txt..." -ForegroundColor Cyan
$nugetKeyPath = Join-Path $PSScriptRoot "nuget-key.txt"

if (-not (Test-Path $nugetKeyPath)) {
    Write-Error "nuget-key.txt does not exist in the solution root."
    exit 1
}

$nugetKey = Get-Content $nugetKeyPath -Raw
if ([string]::IsNullOrWhiteSpace($nugetKey)) {
    Write-Error "nuget-key.txt exists but is empty."
    exit 1
}
$nugetKey = $nugetKey.Trim()

$gitIgnoreCheck = git check-ignore nuget-key.txt
if ($LASTEXITCODE -ne 0) {
    Write-Error "nuget-key.txt is not gitignored. Add it to .gitignore before publishing."
    exit 1
}
Write-Host "nuget-key.txt is valid and gitignored." -ForegroundColor Green

# Step 4: Run unit tests (unless -SkipTests is specified)
if (-not $SkipTests) {
    Write-Host "Running unit tests..." -ForegroundColor Cyan
    dotnet test "$PSScriptRoot\PanoramicData.WhoIs.Test\PanoramicData.WhoIs.Test.csproj" --configuration Release --no-build
    if ($LASTEXITCODE -ne 0) {
        # Try building first then testing
        dotnet test "$PSScriptRoot\PanoramicData.WhoIs.Test\PanoramicData.WhoIs.Test.csproj" --configuration Release
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Unit tests failed."
            exit 1
        }
    }
    Write-Host "Unit tests passed." -ForegroundColor Green
} else {
    Write-Host "Skipping unit tests." -ForegroundColor Yellow
}

# Step 5: Build and pack the project
Write-Host "Building and packing the project..." -ForegroundColor Cyan
dotnet pack "$PSScriptRoot\PanoramicData.WhoIs\PanoramicData.WhoIs.csproj" --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to pack the project."
    exit 1
}
Write-Host "Package created successfully." -ForegroundColor Green

# Step 6: Publish to nuget.org
Write-Host "Publishing to nuget.org..." -ForegroundColor Cyan
$nupkgPath = "$PSScriptRoot\PanoramicData.WhoIs\bin\Release\PanoramicData.WhoIs.$version.nupkg"

if (-not (Test-Path $nupkgPath)) {
    Write-Error "NuGet package not found at: $nupkgPath"
    exit 1
}

dotnet nuget push $nupkgPath --api-key $nugetKey --source https://api.nuget.org/v3/index.json --skip-duplicate
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to publish to nuget.org"
    exit 1
}

Write-Host "Successfully published PanoramicData.WhoIs version $version to nuget.org" -ForegroundColor Green
exit 0
