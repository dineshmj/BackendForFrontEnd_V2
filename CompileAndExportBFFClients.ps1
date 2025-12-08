cls;

function Install-NPM-Dependencies {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Directory
    )

    cd $Directory

    Write-Host "`n`n"    
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan
    Write-Host "`t1. Installing dependencies in '$Directory' (npm install)..." -ForegroundColor Cyan
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan

    npm install

    if ($LASTEXITCODE -ne 0) {
        Write-Error "npm install failed in '$Directory'. Stopping script."
        exit 1
    }
   
    Write-Host "Dependency installation complete for '$Directory'." -ForegroundColor Green
}

function Export-NextJS-SPA-For-BFF {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Directory
    )

    cd $Directory
    
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan
    Write-Host "`t1. Exporting SPA as static files for BFF in '$Directory' (npm install)..." -ForegroundColor Cyan
    Write-Host "`t######################################################################################################################################################" -ForegroundColor Cyan

    npm run export

    if ($LASTEXITCODE -ne 0) {
        Write-Error "'npm run $NpmCommand' failed in '$Directory'. Stopping script."
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

Install-NPM-Dependencies -Directory $shellSpaAppFolder
Export-NextJS-SPA-For-BFF -Directory $shellSpaAppFolder

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==      Step # 2: Installing NPM Dependencies and exporting SPA for BFF for Products Microservice Frontend      ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-NPM-Dependencies -Directory $productsMicroserviceSpaAppFolder
Export-NextJS-SPA-For-BFF -Directory $productsMicroserviceSpaAppFolder

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==            Step # 3: Installing NPM Dependencies for BFF for Orders Microservice Frontend                    ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-NPM-Dependencies -Directory $ordersMicroserviceBffAppFolder

Write-Host "==================================================================================================================" -ForegroundColor Yellow;
Write-Host "==            Step # 4: Installing NPM Dependencies for SPA for Orders Microservice Frontend                    ==" -ForegroundColor Yellow;
Write-Host "==================================================================================================================" -ForegroundColor Yellow;

Install-NPM-Dependencies -Directory $ordersMicroserviceSpaAppFolder


## $visualStudioCodePath = "C:\Users\Dinesh\AppData\Local\Programs\Microsoft VS Code\Code.exe";
$visualStudioCodePath = "P:\Users\dines_y5ddmdz\AppData\Local\Programs\Microsoft VS Code\Code.exe"

# Start-Process -FilePath $visualStudioCodePath -ArgumentList $ordersMicroserviceBffAppFolder