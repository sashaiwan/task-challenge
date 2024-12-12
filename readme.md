# Task Management Application

A full-stack task management application built with ASP.NET Core and Angular.

## Prerequisites

- .NET 8.0 SDK
- Node.js 22
- Angular CLI
- SQLite
- Git

## Project Structure

```
TaskChallenge/
├── backend/               # Backend solution
│   ├── TaskChallenge.Api/
│   ├── TaskChallenge.Application/
│   └── TaskChallenge.Infrastructure/
└── frontend/             # Angular application
```

## Getting Started

### Using Scripts

#### Windows (PowerShell)

```powershell
./start-back.ps1
./start-front.ps1
```

#### Unix/Linux/MacOS (bash)

```bash
./start-back.sh
./start-front.sh
```

### Manual Setup

1. Clone the repository:

```bash
git clone <repository-url>
cd TaskChallenge
```

1. Backend Setup:

```bash
cd backend
dotnet restore
dotnet build
cd TaskChallenge.Api
dotnet run
```

1. Frontend Setup:

```bash
cd frontend
npm install
ng serve
```

## Features

- Create, read, update, and delete tasks
- Mark tasks as completed
- Filter tasks by completion status
- Required field validation
- Due date validation (must be in the future)
- Error handling and success messages
- Responsive design with Tailwind CSS

## API Endpoints

- GET /api/tasks - Get all tasks (optional filter: ?completed=true/false)
- POST /api/tasks - Create a new task
- PUT /api/tasks/{id} - Update a task
- DELETE /api/tasks/{id} - Delete a task

## Development

### Backend

- ASP.NET Core 8.0
- Entity Framework Core with SQLite
- Clean Architecture pattern

### Frontend

- Angular 19
- Tailwind CSS
- Reactive Forms
- Standalone Components

## Testing

(WIP)

## License

MIT
