echo "🚀 Starting Task Challenge Backend..."

cd backend
dotnet restore
dotnet build
cd TaskChallenge.Api
dotnet run --urls="https://localhost:5000"
