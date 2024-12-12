Write-Host "🚀 Starting Task Challenge Backend..." -ForegroundColor Green

Set-Location backend
dotnet restore
dotnet build
Set-Location TaskChallenge.Api
dotnet run --urls="https://localhost:5000"
