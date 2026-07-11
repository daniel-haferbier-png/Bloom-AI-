# BloomAI - Quick Start Guide

## Prerequisites
- .NET 8 SDK
- PostgreSQL 14+
- Docker & Docker Compose (optional)

## Local Development Setup

### Step 1: Clone Repository
```bash
git clone https://github.com/daniel-haferbier-png/Bloom-AI-.git
cd Bloom-AI-
```

### Step 2: Start Database
```bash
# Using Docker Compose
docker-compose up postgres

# Or create manually
creatdb bloomai_dev
```

### Step 3: Apply Migrations
```bash
cd src/Backend/BloomAI.API
dotnet ef database update
```

### Step 4: Run API
```bash
dotnet run
```

### Step 5: Test
Visit: `https://localhost:7001/swagger`

## Test Credentials

**Admin Account:**
- Email: admin@bloomai.com
- Password: Admin@123456

**Employee Account:**
- Email: john@bloomai.com
- Password: Worker@123456

## Next Steps

1. Build mobile app (Phase 2)
2. Implement AI features (Phase 2-3)
3. Deploy to production

See docs/ folder for detailed documentation.
