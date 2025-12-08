@echo off
echo Starting all services...

start "IdentityServer" cmd /k "cd src\IdentityServer && dotnet run --urls=https://localhost:5000"
timeout /t 3

start "Orders API" cmd /k "cd src\Microservices\Orders.API && dotnet run --urls=https://localhost:5001"
start "Products API" cmd /k "cd src\Microservices\Products.API && dotnet run --urls=https://localhost:5002"
start "Payments API" cmd /k "cd src\Microservices\Payments.API && dotnet run --urls=https://localhost:5003"
timeout /t 3

start "BFF Application" cmd /k "cd src\BFF.Web && dotnet run --urls=https://localhost:5005"

echo All services started!
echo IdentityServer: https://localhost:5000
echo Orders API: https://localhost:5001
echo Products API: https://localhost:5002
echo Payments API: https://localhost:5003
echo BFF Application: https://localhost:5005
