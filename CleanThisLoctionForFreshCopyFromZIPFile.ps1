cls;

[string] $rootFolderToCleanPath = "C:\DEV\Experiments\BFF_NET10";
[string []] $subFoldersToSpare = @(
    "$($rootFolderToCleanPath)\src\Shell\client-app\node_modules",
    "$($rootFolderToCleanPath)\src\Microservices\Products\BFF.Web\client-app\node_modules",
    "$($rootFolderToCleanPath)\src\Microservices\Orders\BFF.Web\src\node_modules",
    "$($rootFolderToCleanPath)\src\Microservices\Orders\BFF.Web\client-app\node_modules"
);

Write-Host "===| Scanning and Categorizing Folders under '$rootFolderToCleanPath' |===`n" -ForegroundColor Cyan

$successCount = 0;
$markedForDeletionCount = 0;
$i = 1;

while ($true) {
    Write-Host "`nIteration # $i ...`n";

    [string []] $allFoldersInside = Get-ChildItem -Path $rootFolderToCleanPath -Directory -Recurse | Select-Object -ExpandProperty FullName

    [string []] $subFoldersExcluded = @();
    [string []] $subFoldersMarkedForDeletion = @();

    foreach ($folderPath in $allFoldersInside) {
        $isSpared = $false;
   
        foreach ($sparePath in $subFoldersToSpare) {
            if ($sparePath -eq $folderPath -or $sparePath -like "$folderPath\*" -or $folderPath -like "$sparePath\*" ) {
                $isSpared = $true;
                break;
            }
        }

        if ($isSpared) {
            $subFoldersExcluded += $folderPath;
        }
        else {
            $subFoldersMarkedForDeletion += $folderPath;
        }
    }

    if ($subFoldersMarkedForDeletion.Count -eq 0) {
        Write-Host "`nNo more iterations needed; all folders to be deleted have been located."
        break;
    }

    Write-Host "===| Summary |===`n" -ForegroundColor Cyan
    Write-Host "`t`tIteration # $i - Folders excluded: $($subFoldersExcluded.Count)" -ForegroundColor Green
    Write-Host "`t`tIteration # $i - Folders marked for deletion: $($subFoldersMarkedForDeletion.Count)" -ForegroundColor Red
    Write-Host "`n===| Summary End |===`n" -ForegroundColor Cyan

    $markedForDeletionCount += $subFoldersMarkedForDeletion.Count;

    Write-Host "`n===| Iteration # $i - Starting Deletion of $($subFoldersMarkedForDeletion.Count) Folders |===" -ForegroundColor Red
   
    foreach ($folderToDelete in $subFoldersMarkedForDeletion) {
        if (Test-Path -Path $folderToDelete -PathType Container) {
            try {
                Remove-Item -Path $folderToDelete -Recurse -Force -ErrorAction Stop
                Write-Host "   -> DELETED: '$folderToDelete\'." -ForegroundColor Green
                $successCount ++;
            }
            catch {
                Write-Host "   -> FAILED TO DELETE: Failed to delete $folderToDelete. $($_.Exception.Message)" -ForegroundColor Red
            }
        }
        else {
            $successCount ++;
        }
    }

    $i++;
}

Write-Host "`n===| Starting Sibling File Cleanup in Spared Directories |===`n" -ForegroundColor DarkYellow

[string []] $allFilesInside = Get-ChildItem -Path $rootFolderToCleanPath -File -Recurse -ErrorAction SilentlyContinue | Select-Object -ExpandProperty FullName
[string []] $filesMarkedForDeletion = @();

foreach ($filePath in $allFilesInside) {
    $isFileSpared = $false;
   
    foreach ($sparePath in $subFoldersToSpare) {
        if ($filePath -like "$sparePath\*") {
            $isFileSpared = $true;
            break;
        }
    }
   
    if (-not $isFileSpared) {
        $filesMarkedForDeletion += $filePath;
    }
}

$fileDeletionCount = $filesMarkedForDeletion.Count;
Write-Host "`n===| Starting Deletion of $($fileDeletionCount) Files |===" -ForegroundColor Yellow

foreach ($fileToDelete in $filesMarkedForDeletion) {
    if (Test-Path -Path $fileToDelete -PathType Leaf) {
        try {
            Remove-Item -Path $fileToDelete -Force -ErrorAction Stop
            $totalFilesDeleted++
        }
        catch {
            Write-Host "    -> FAILED TO DELETE FILE: '$fileToDelete'. $($_.Exception.Message)" -ForegroundColor Red
        }
    }
}

Write-Host "`n===| Clean-up complete. $($successCount) out of $markedForDeletionCount Folders, and $totalFilesDeleted sibling files successfully cleaned. |===" -ForegroundColor Red