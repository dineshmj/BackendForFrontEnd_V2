cls;

function Install-Dependencies {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Directory,
        [switch]$UsePnpm
    )

    cd $Directory
    $packageManager = if ($UsePnpm) { "pnpm" } else { "npm" }

    Write-Host "`n`n"    
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan
    Write-Host "`t1. Installing dependencies in '$Directory' ($packageManager install)..." -ForegroundColor Cyan
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan

    & $packageManager install

    if ($LASTEXITCODE -ne 0) {
        Write-Error "$packageManager install failed in '$Directory'. Stopping script."
        exit 1
    }
   
    Write-Host "Dependency installation complete for '$Directory'." -ForegroundColor Green
}

function Export-NextJS-SPA-For-BFF {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Directory,
        [switch]$UsePnpm
    )

    cd $Directory
    $packageManager = if ($UsePnpm) { "pnpm" } else { "npm" }
    
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan
    Write-Host "`t1. Exporting SPA as static files for BFF in '$Directory' ($packageManager run export)..." -ForegroundColor Cyan
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan

    & $packageManager run export

    if ($LASTEXITCODE -ne 0) {
        Write-Error "'$packageManager run export' failed in '$Directory'. Stopping script."
        exit 1
    }
   
    Write-Host "Exporting of SPA for BFF complete for '$Directory'." -ForegroundColor Green
}

cls

$codeRootFolder = $PSScriptRoot;

$shellSpaAppFolder = "$($codeRootFolder)\src\Shell\client-app";
$productsMicroserviceSpaAppFolder = "$($codeRootFolder)\src\Microservices\Products\BFF.Web\client-app";
$ordersMicroserviceBffAppFolder = "$($codeRootFolder)\src\Microservices\Orders\BFF.Web";
$ordersMicroserviceSpaAppFolder = "$($ordersMicroserviceBffAppFolder)\client-app";

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==           Step # 1: Installing NPM Dependencies and exporting SPA for BFF for PAS Shell                      ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-Dependencies -Directory $shellSpaAppFolder -UsePnpm
Export-NextJS-SPA-For-BFF -Directory $shellSpaAppFolder -UsePnpm

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==      Step # 2: Installing NPM Dependencies and exporting SPA for BFF for Products Microservice Frontend      ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-Dependencies -Directory $productsMicroserviceSpaAppFolder -UsePnpm
Export-NextJS-SPA-For-BFF -Directory $productsMicroserviceSpaAppFolder -UsePnpm

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==            Step # 3: Installing NPM Dependencies for BFF for Orders Microservice Frontend                    ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-Dependencies -Directory $ordersMicroserviceBffAppFolder -UsePnpm

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==            Step # 4: Installing NPM Dependencies for SPA for Orders Microservice Frontend                    ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-Dependencies -Directory $ordersMicroserviceSpaAppFolder -UsePnpm


## $visualStudioCodePath = "C:\Users\Dinesh\AppData\Local\Programs\Microsoft VS Code\Code.exe";
$visualStudioCodePath = "P:\Users\dines_y5ddmdz\AppData\Local\Programs\Microsoft VS Code\Code.exe"

# Start-Process -FilePath $visualStudioCodePath -ArgumentList $ordersMicroserviceBffAppFolder