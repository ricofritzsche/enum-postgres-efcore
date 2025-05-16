# Enum Persistence Console App

This .NET 9 console application demonstrates how to persist C# enums directly into a PostgreSQL database as native enum types, ensuring full type safety both in code and at the database level.

## Overview

Instead of storing enums as plain strings or integers, this project uses PostgreSQL’s built-in `ENUM` type. With Npgsql’s EF Core provider, we map a C# `enum` to the database enum without manual serialization or parsing.

Key benefits:

* **Database-side enforcement**: Invalid values are rejected by Postgres.
* **Zero-boilerplate mapping**: Npgsql handles parameter binding for you.
* **Human-readable & compact**: Stored as efficient integers internally, but shown as labels in SQL clients.
* **Schema evolution**: EF Core migrations can generate and alter enum types.

## Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* [PostgreSQL (>= 9.1)](https://www.postgresql.org/download/)
* Connection string with sufficient privileges to create types and tables.

## Project Structure

```
EnumPersistenceConsoleApp/
├── EnumPersistenceConsoleApp.csproj   
├── AppDbContext.cs   
├── Order.cs                          
├── Program.cs                     
└── README.md                          
```

### Program.cs Highlights

1. **`OrderStatus` enum**

   ```csharp
   public enum OrderStatus { Pending, Processing, Completed, Cancelled }
   ```
2. **`Order` entity** with `Status` property
3. **DbContext configuration**:

    * In `OnConfiguring()`, Npgsql’s `MapEnum<OrderStatus>("order_status")` registers the native enum type at the ADO.NET layer.
    * In `OnModelCreating()`, `HasPostgresEnum<OrderStatus>("order_status")` prepares EF Core migrations, and `.HasColumnType("order_status")` maps the property directly.

   This ensures the database enum type and tables exist before use.

## Getting Started

1. **Clone the repo**:

   ```bash
   git clone https://github.com/ricofritzsche/enum-postgres-efcore
   cd enum-postgres-efcore
   ```

2. **Configure connection**:
   In `Program.cs`, update the `connectionString` in `OnConfiguring()` to match your PostgreSQL server credentials.

3. **Build & run**:

   ```bash
   dotnet build
   dotnet run
   ```

   You should see output like:

   ```console
   Saved Order Id=1 with Status=Processing
   All orders in database:
   - Order Id=1, Status=Processing
   ```

