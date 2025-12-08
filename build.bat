@echo off
echo Building BFF Solution...

echo Building NextJS client...
cd src\BFF.Web\client-app
call npm install
call npm run export
cd ..\..\..

echo Building .NET projects...
dotnet restore
dotnet build --configuration Release

echo Build complete!
