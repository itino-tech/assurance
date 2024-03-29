$threshold = 100
$reportFilePath = "$PSScriptRoot\..\CodeCoverage\SummaryGithub.md"
$errorExitcode = 1

if (-not (Test-Path $reportFilePath)) 
{
    Write-Host "File not found at $reportFilePath"
    $host.SetShouldExit($errorExitcode)
}

$reportText = Get-Content -Path $reportFilePath -Raw
$linePattern = '\| \*\*Line coverage:\*\* \| (\d+(\.\d+)?)% \(\d+ of \d+\) \|'
$branchPattern = '\| \*\*Branch coverage:\*\* \| (\d+(\.\d+)?)% \(\d+ of \d+\) \|'

function Process-Percentage($name, $pattern)
{
    $match = $reportText | Select-String -Pattern $pattern -AllMatches | Select-Object -First 1

    if (-not $match) 
    {
        Write-Host "No '$name' percentages found in the report text."
        $host.SetShouldExit($errorExitcode)
    } 
    
    $percentage = [double]$match.Matches.Groups[1].Value
    Write-Host "Extracted '$name' test coverage percentage: $percentage"
    
    if ($percentage -lt $threshold) 
    {
        Write-Host "The '$name' coverage $percentage is less than $threshold."
        $host.SetShouldExit($errorExitcode)    
    }
}

Process-Percentage 'line' $linePattern
Process-Percentage 'branch' $branchPattern
