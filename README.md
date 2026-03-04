# EffiMetricAPI
A performance-based earnings and tracking engine designed to calculate and manage earnings based on performance metrics, inspired by professional billiard hall management systems.

## Status
This project is under active development and is being continuously improved.

## Tech Stack
- Framework: ASP.NET Core (.NET 8.0)

- Database: SQLite

- ORM: Entity Framework Core

- Documentation: Swagger UI

## Features
- Legendary Performance Tracking: Advanced logic for calculating earnings based on player/staff performance.

- SQLite & EF Core: Lightweight local database with robust ORM integration for seamless data management.

- Dynamic Calculations: Real-time earnings engine based on billiard-specific custom metrics.

- Data Isolation: Organized ownership logic for entities.

- Smart Update: Partial data preservation on updates.

- FluentValidation Integration: Centralized and clean validation layer.

- DTO Architecture: Prevents direct exposure of entities by introducing controlled data transfer objects.

- Global Exception Handling: Consistent and professional JSON error responses.

- Automatic Rank Calculation: Dynamic rank assignment based on efficiencyScore.

## Planned Features
- Dashboard Integration: Visualizing performance trends.

- Multi-Hall Support: Managing multiple locations under one API.

## Getting Started
1. Clone the repository:
   git clone https://github.com/xMrFrozen/EffiMetricAPI.git

2. Navigate to the project folder:
   cd EffiMetricAPI

3. Run the application:
   dotnet run
   
The SQLite database is created automatically on the first run.

## API Documentation
Once the application is running, explore the API via Swagger:
https://localhost:xxxx/swagger
