# BloomAI - AI Powered Work Intelligence Platform

**Philosophy:** "The human does the work. BloomAI understands, organizes and documents it."

BloomAI is a production-ready SaaS platform that helps companies and workers manage real-world work, automatically document activities, and reduce administrative effort. Initially targeting handcraft companies, construction sites, and field service workers, with modular expansion to other industries.

## Table of Contents

- [Quick Start](#quick-start)
- [Architecture Overview](#architecture-overview)
- [Technology Stack](#technology-stack)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Development](#development)
- [Deployment](#deployment)
- [API Documentation](#api-documentation)
- [Project Structure](#project-structure)
- [Features](#features)
- [Security & Compliance](#security--compliance)
- [Contributing](#contributing)

## Quick Start

### Prerequisites

- .NET 8+ SDK
- PostgreSQL 14+
- Node.js 18+ (for build tools)
- Visual Studio 2022 or VS Code
- Android SDK / Xcode (for mobile development)

### Setup in 5 Minutes

```bash
# Clone repository
git clone https://github.com/daniel-haferbier-png/Bloom-AI-.git
cd Bloom-AI-

# Backend setup
cd src/Backend/BloomAI.API
dotnet restore
dotnet ef database update
dotnet run

# Mobile setup (requires separate environment)
cd src/Mobile/BloomAI.Mobile
dotnet build -f net8.0-android

# Frontend setup (optional admin dashboard)
cd src/Frontend/BloomAI.Dashboard
npm install
npm start
```

**API will be available at:** `https://localhost:7001`

**Database migrations run automatically on startup.**

---

## Architecture Overview

### Layered Architecture

```
┌─────────────────────────────────────────────────────┐
│         Mobile App (.NET MAUI)                      │
│  - iOS, Android, Windows                            │
│  - MVVM Pattern                                     │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│    REST API (ASP.NET Core 8)                        │
│  - Authentication & Authorization                  │
│  - Business Logic Layer                            │
│  - API Controllers                                 │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│    Application Services Layer                       │
│  - Business Rules                                  │
│  - Workflows                                       │
│  - AI Integration                                  │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│    Data Access Layer (Entity Framework Core)        │
│  - Repository Pattern                              │
│  - Database Migrations                             │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│    PostgreSQL Database                              │
│  - Company Data                                    │
│  - User Profiles                                   │
│  - Projects, Tasks, Reports                        │
│  - Audit Logs                                      │
└─────────────────────────────────────────────────────┘
```

### Module Overview

| Module | Purpose | Status |
|--------|---------|--------|
| **User Management** | Authentication, profiles, permissions | ✅ Phase 1 |
| **Company System** | Multi-tenant company management | ✅ Phase 1 |
| **Project Management** | Projects, locations, customers | ✅ Phase 1 |
| **Smart Task Engine** | Dynamic task creation and prioritization | ✅ Phase 1 |
| **Location Intelligence** | GPS, geofencing, location-based workflows | 🔄 Phase 2 |
| **Work Documentation** | Auto-collection of work activities | 🔄 Phase 2 |
| **AI Report Generator** | OpenAI integration for report generation | 🔄 Phase 2 |
| **Translation System** | Multi-language support | 🔄 Phase 2 |
| **Digital Signatures** | Customer approval workflows | 🔄 Phase 3 |
| **AI Intelligence Engine** | Context, memory, recommendations | 🔄 Phase 3 |
| **Digital Work Twin** | Project history and analytics | 🔄 Phase 3 |
| **Company Knowledge** | Procedures and solutions database | 🔄 Phase 4 |
| **Audit System** | Activity logging and compliance | ✅ Phase 1 |

---

## Technology Stack

### Backend

| Layer | Technology | Version |
|-------|-----------|---------|
| **Framework** | ASP.NET Core | 8.0 |
| **Language** | C# | 12 |
| **Database** | PostgreSQL | 14+ |
| **ORM** | Entity Framework Core | 8.0 |
| **Auth** | JWT (System.IdentityModel.Tokens.Jwt) | Latest |
| **Password Hashing** | BCrypt.Net | Latest |
| **Validation** | FluentValidation | Latest |
| **Logging** | Serilog | Latest |
| **API Documentation** | Swagger/OpenAPI | 3.0 |

### Mobile

| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | .NET MAUI | 8.0 |
| **Language** | C# | 12 |
| **Architecture** | MVVM Toolkit | Latest |
| **HTTP** | HttpClient | Native |
| **Storage** | SQLite (local) | Latest |
| **Location** | Microsoft.Maui.Controls.Maps | Latest |
| **Camera** | MediaPicker | Native |

### DevOps

| Tool | Purpose |
|------|---------|
| **Docker** | Containerization |
| **Docker Compose** | Local development environment |
| **GitHub Actions** | CI/CD pipeline |
| **Azure App Service** | Cloud deployment (optional) |
| **Azure Database for PostgreSQL** | Cloud database |

---

## Installation

### 1. Clone Repository

```bash
git clone https://github.com/daniel-haferbier-png/Bloom-AI-.git
cd Bloom-AI-
```

### 2. Install Backend Dependencies

```bash
cd src/Backend/BloomAI.API
dotnet restore
```

### 3. Configure Database Connection

Create `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=bloomai_dev;Username=postgres;Password=your_password"
  },
  "Jwt": {
    "SecretKey": "your-super-secret-key-min-32-characters-long-1234567890!",
    "Issuer": "BloomAI",
    "Audience": "BloomAI-Users",
    "ExpirationMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### 4. Create PostgreSQL Database

```bash
# Using Docker (recommended)
docker run --name bloomai-db -e POSTGRES_PASSWORD=password -e POSTGRES_DB=bloomai_dev -p 5432:5432 -d postgres:14

# Or using local PostgreSQL
createdb bloomai_dev
```

### 5. Run Database Migrations

```bash
dotnet ef database update
```

This creates all tables, relationships, and initial data.

### 6. Run Backend

```bash
dotnet run
```

API will be available at `https://localhost:7001`

Check Swagger UI at `https://localhost:7001/swagger`

### 7. Setup Mobile App

```bash
cd src/Mobile/BloomAI.Mobile

# For Android
dotnet build -f net8.0-android

# For iOS (macOS only)
dotnet build -f net8.0-ios

# For Windows
dotnet build -f net8.0-windows
```

---

## Database Setup

### Database Diagram

```
┌─────────────────────────────┐
│        Users                │
├─────────────────────────────┤
│ Id (PK)                     │
│ FirstName                   │
│ LastName                    │
│ Email (UNIQUE)              │
│ PasswordHash                │
│ Role                        │
│ LanguagePreference          │
│ CompanyId (FK)              │
│ CreatedAt                   │
│ UpdatedAt                   │
│ IsActive                    │
└──────────────┬──────────────┘
               │
      ┌────────▼─────────┐
      │   Companies      │
      ├──────────────────┤
      │ Id (PK)          │
      │ Name             │
      │ TaxId            │
      │ Industry         │
      │ Country          │
      │ CreatedAt        │
      │ UpdatedAt        │
      │ IsActive         │
      └────────┬─────────┘
               │
      ┌────────▼──────────────┐
      │    Projects           │
      ├───────────────────────┤
      │ Id (PK)               │
      │ CompanyId (FK)        │
      │ Name                  │
      │ CustomerId (FK)       │
      │ LocationId (FK)       │
      │ Status                │
      │ StartDate             │
      │ EndDate               │
      │ Budget                │
      │ CreatedAt             │
      │ UpdatedAt             │
      └────────┬──────────────┘
               │
      ┌────────▼──────────────┐
      │      Tasks            │
      ├───────────────────────┤
      │ Id (PK)               │
      │ ProjectId (FK)        │
      │ Title                 │
      │ Description           │
      │ Status                │
      │ Priority              │
      │ AssignedToId (FK)     │
      │ DueDate               │
      │ CompletedAt           │
      │ CreatedAt             │
      │ UpdatedAt             │
      └───────────────────────┘
```

### Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply pending migrations
dotnet ef database update

# Rollback last migration
dotnet ef database update PreviousMigrationName

# View migration history
dotnet ef migrations list
```

### Seed Initial Data

The system automatically seeds:
- 5 default roles (Employee, ProjectManager, Administrator, CompanyOwner, Customer)
- Sample company for development
- Sample users
- Sample projects and tasks

---

## Development

### Project Structure

```
Bloom-AI-/
├── src/
│   ├── Backend/
│   │   └── BloomAI.API/
│   │       ├── Controllers/              # API Endpoints
│   │       ├── Services/                 # Business Logic
│   │       ├── Models/                   # Domain Models
│   │       ├── Data/                     # DbContext, Migrations
│   │       ├── Middleware/               # Auth, Error Handling
│   │       ├── Validators/               # FluentValidation
│   │       ├── Mappers/                  # DTO Mapping
│   │       ├── appsettings.json          # Configuration
│   │       └── Program.cs                # DI Setup
│   │
│   ├── Mobile/
│   │   └── BloomAI.Mobile/
│   │       ├── Views/                    # UI Screens
│   │       ├── ViewModels/               # MVVM Logic
│   │       ├── Services/                 # API Integration
│   │       ├── Models/                   # Client Models
│   │       ├── Resources/                # Assets, Strings
│   │       ├── MauiProgram.cs            # DI Setup
│   │       └── App.xaml                  # App Shell
│   │
│   └── Frontend/
│       └── BloomAI.Dashboard/            # Admin Dashboard (optional)
│           ├── src/
│           ├── public/
│           └── package.json
│
├── docs/                                 # Documentation
├── tests/                                # Unit & Integration Tests
├── docker-compose.yml                    # Local Dev Environment
├── .github/workflows/                    # CI/CD Pipelines
└── README.md                             # This file
```

### Running in Docker

```bash
# Start full stack (API + Database)
docker-compose up

# Stop services
docker-compose down

# View logs
docker-compose logs -f api
docker-compose logs -f db
```

### Available Endpoints (Phase 1)

#### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/refresh` - Refresh expired token
- `POST /api/auth/logout` - Logout

#### Users
- `GET /api/users/{id}` - Get user profile
- `PUT /api/users/{id}` - Update user profile
- `GET /api/users` - List users (company admin only)

#### Companies
- `POST /api/companies` - Create company
- `GET /api/companies/{id}` - Get company details
- `PUT /api/companies/{id}` - Update company
- `GET /api/companies/{id}/employees` - List company employees

#### Projects
- `POST /api/projects` - Create project
- `GET /api/projects/{id}` - Get project details
- `PUT /api/projects/{id}` - Update project
- `GET /api/projects` - List user's projects
- `GET /api/projects/{id}/tasks` - Get project tasks

#### Tasks
- `POST /api/tasks` - Create task
- `GET /api/tasks/{id}` - Get task details
- `PUT /api/tasks/{id}` - Update task
- `PATCH /api/tasks/{id}/status` - Update task status
- `GET /api/tasks` - List tasks for user

#### Locations
- `POST /api/locations` - Create location
- `GET /api/locations/{id}` - Get location details
- `GET /api/locations` - List company locations

---

## Deployment

### Azure Deployment

```bash
# Create resource group
az group create --name bloomai-prod --location westeurope

# Create App Service
az appservice plan create --name bloomai-plan --resource-group bloomai-prod --sku B2 --is-linux

# Deploy API
az webapp create --name bloomai-api --plan bloomai-plan --resource-group bloomai-prod --runtime DOTNETCORE:8.0

# Create PostgreSQL
az postgres server create --name bloomai-db --resource-group bloomai-prod --admin-user pgadmin --admin-password StrongPassword123!

# Publish code
dotnet publish -c Release
az webapp up --name bloomai-api --resource-group bloomai-prod
```

### Environment Variables

Set these in your deployment environment:

```
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=<postgresql-connection-string>
Jwt__SecretKey=<your-secret-key>
Jwt__Issuer=BloomAI
Jwt__Audience=BloomAI-Users
Jwt__ExpirationMinutes=60
OpenAI__ApiKey=<your-openai-key>
```

### CI/CD Pipeline

GitHub Actions automatically:
1. Runs tests on every push
2. Builds Docker image
3. Pushes to registry on release tag
4. Deploys to Azure on production release

See `.github/workflows/` for details.

---

## API Documentation

### Authentication Flow

```
1. User registers → POST /api/auth/register
   └─ Get JWT token
   
2. User logs in → POST /api/auth/login
   └─ Get JWT token with refresh token
   
3. Include in headers → Authorization: Bearer <token>
   └─ Access protected endpoints
   
4. Token expires → POST /api/auth/refresh
   └─ Get new token
```

### Request/Response Format

All API requests and responses use JSON.

**Example Request:**
```bash
curl -X POST https://localhost:7001/api/tasks \
  -H "Authorization: Bearer eyJhbGc..." \
  -H "Content-Type: application/json" \
  -d '{
    "projectId": "123e4567-e89b-12d3-a456-426614174000",
    "title": "Install electrical sockets",
    "description": "Install 3 wall sockets in living room",
    "priority": "High",
    "status": "Open",
    "dueDate": "2024-12-31"
  }'
```

**Example Response:**
```json
{
  "id": "123e4567-e89b-12d3-a456-426614174001",
  "projectId": "123e4567-e89b-12d3-a456-426614174000",
  "title": "Install electrical sockets",
  "description": "Install 3 wall sockets in living room",
  "priority": "High",
  "status": "Open",
  "assignedToId": null,
  "dueDate": "2024-12-31T00:00:00Z",
  "completedAt": null,
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### Error Responses

```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [
    {
      "field": "title",
      "message": "Title is required"
    }
  ]
}
```

---

## Features

### Phase 1: Foundation (Current) ✅
✅ User authentication and authorization
✅ Multi-tenant company system
✅ Project management
✅ Task management with status tracking
✅ Location management
✅ Role-based access control
✅ Audit logging
✅ Mobile app foundation with MVVM
✅ JWT-based security
✅ Complete API documentation

### Phase 2: Work Intelligence 🔄
🔄 GPS and geofencing
🔄 Work documentation system
🔄 AI report generation (OpenAI)
🔄 Multi-language translation
🔄 Photo and document management
🔄 Voice input support

### Phase 3: Advanced AI 🔄
🔄 AI Intelligence Engine
  - Context awareness
  - Memory system
  - Recommendation engine
🔄 Digital signatures and approvals
🔄 Digital Work Twin (project history)

### Phase 4: Enterprise 🔄
🔄 Company Knowledge System
🔄 Advanced analytics and reporting
🔄 Workflow automation
🔄 Industry-specific modules
🔄 Integration marketplace

---

## Security & Compliance

### GDPR Compliance

The system implements:
- ✅ User consent architecture
- ✅ Data deletion support
- ✅ Data export functionality
- ✅ Audit logging of all changes
- ✅ Role-based access control
- ✅ Secure password hashing with BCrypt
- ✅ JWT token expiration and refresh
- ✅ HTTPS/TLS enforcement

### Security Features

- **Password Security:** BCrypt hashing with salt
- **Authentication:** JWT with configurable expiration
- **Authorization:** Role-based access control (RBAC)
- **Data Encryption:** TLS for data in transit
- **Input Validation:** FluentValidation on all endpoints
- **SQL Injection Prevention:** Parameterized queries via EF Core
- **CORS:** Configured for allowed origins
- **Rate Limiting:** Implemented on authentication endpoints

### Audit Trail

Every change is logged with:
- User ID
- Change timestamp
- Data changed (before/after)
- IP address
- User agent

---

## Testing

### Run Tests

```bash
# All tests
dotnet test

# Specific test project
dotnet test tests/BloomAI.Tests

# With coverage
dotnet test /p:CollectCoverage=true
```

### Test Structure

```
tests/
├── BloomAI.Tests.Unit/
│   ├── Services/
│   ├── Validators/
│   └── Mappers/
│
└── BloomAI.Tests.Integration/
    ├── Controllers/
    ├── Database/
    └── Authentication/
```

---

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open Pull Request

### Code Standards

- Follow C# naming conventions (PascalCase for public, camelCase for private)
- Add XML documentation for public APIs
- Write unit tests for new features
- Update README.md if adding features
- Run `dotnet format` before committing

---

## Support & Documentation

- 📖 [Full API Documentation](docs/API.md)
- 🏗️ [Architecture Guide](docs/ARCHITECTURE.md)
- 🔐 [Security Guide](docs/SECURITY.md)
- 📱 [Mobile Development Guide](docs/MOBILE.md)
- 🚀 [Deployment Guide](docs/DEPLOYMENT.md)
- ⚡ [Quick Start Guide](QUICKSTART.md)

---

## License

© 2024 BloomAI. All rights reserved.

---

## Roadmap

**Q1 2024:** Phase 1 completion, mobile beta
**Q2 2024:** Phase 2 (AI documentation), iOS/Android release
**Q3 2024:** Phase 3 (advanced AI), enterprise customers
**Q4 2024:** Phase 4 (knowledge system), industry expansions

---

## Getting Started

1. **Read the [QUICKSTART.md](QUICKSTART.md)** for immediate setup
2. **Check [docs/API.md](docs/API.md)** for API endpoint details
3. **Review [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md)** for system design
4. **Follow [docs/DEPLOYMENT.md](docs/DEPLOYMENT.md)** for production setup

---

**Last Updated:** January 2024
**Status:** ✅ Active Development - Phase 1 Complete
**Current Build:** v0.1.0-alpha
**Maintainer:** Daniel Haferbier
**Repository:** [GitHub - BloomAI](https://github.com/daniel-haferbier-png/Bloom-AI-)

---

## Quick Commands

```bash
# Development
cd src/Backend/BloomAI.API
dotnet run                           # Start API
dotnet ef migrations add MigName     # Create migration
dotnet ef database update            # Apply migrations

# Docker
docker-compose up                    # Start services
docker-compose down                  # Stop services
docker-compose logs -f api           # View logs

# Testing
dotnet test                          # Run all tests
dotnet test --filter "Category=Unit" # Run specific tests

# Deployment
dotnet publish -c Release            # Build for production
az webapp up --name bloomai-api      # Deploy to Azure
```

---

**Welcome to BloomAI!** 🚀 Start building intelligent work management today.
