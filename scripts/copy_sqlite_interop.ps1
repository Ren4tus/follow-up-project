# Copies SQLite.Interop.dll x86/x64 into project output folders (bin) as x86/ and x64/ subfolders
# Usage: run from repository root: powershell -ExecutionPolicy Bypass -File .\scripts\copy_sqlite_interop.ps1

$repo = Split-Path -Path $PSScriptRoot -Parent
$packagesDir = Join-Path $repo 'packages'
$stub = Get-ChildItem -Path $packagesDir -Filter 'Stub.System.Data.SQLite.Core.NetFramework*' | Sort-Object Name -Descending | Select-Object -First 1
if (-not $stub) { Write-Error "Stub package not found under $packagesDir"; exit 1 }
$stubPath = $stub.FullName
$x86 = Join-Path $stubPath 'build\net46\x86\SQLite.Interop.dll'
$x64 = Join-Path $stubPath 'build\net46\x64\SQLite.Interop.dll'
if (-not (Test-Path $x86) -or -not (Test-Path $x64)) { Write-Error "Expected interop DLLs not found: $x86 or $x64"; exit 1 }

# Find all project output folders (bin\Debug, bin\Release, including net48/net46 folders)
$outputs = Get-ChildItem -Path $repo -Recurse -Directory -ErrorAction SilentlyContinue | Where-Object { $_.FullName -match '\\bin\\(Debug|Release)(\\|$)' }
if ($outputs.Count -eq 0) { Write-Warning 'No bin/Debug or bin/Release folders found; ensure projects have been built at least once.' }

foreach ($out in $outputs) {
    $outPath = $out.FullName
    $destX86 = Join-Path $outPath 'x86'
    $destX64 = Join-Path $outPath 'x64'
    New-Item -ItemType Directory -Path $destX86 -Force | Out-Null
    New-Item -ItemType Directory -Path $destX64 -Force | Out-Null
    Copy-Item -Path $x86 -Destination (Join-Path $destX86 'SQLite.Interop.dll') -Force
    Copy-Item -Path $x64 -Destination (Join-Path $destX64 'SQLite.Interop.dll') -Force
    Write-Output "Copied interop DLLs to $outPath (x86/x64)"
}

Write-Output 'Done. If your app still fails, ensure the Visual C++ Redistributable is installed and that build platform matches (x86/x64).'
