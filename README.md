# VLK-API
# VLK Stock Management System

This project is a full-stack stock management system built with .NET 8, featuring a RESTful API, a web-based UI, and a SQL Server database. The solution is containerized using Docker for easy deployment and development.

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Testing](#testing)
## Features

- Manage clients, stocks, and trading operations.
- RESTful API for backend operations.
- Angular-based UI for user interaction.
- SQL Server database for persistent storage.
- Unit tests for reliability.
- Dockerized services for simplified setup.

## Architecture

- **API**: .NET 8 Web API (`cumakilinc/vlk-api:latest`)
- **UI**: Angular frontend (`cumakilinc/vlk-ui:latest`)
- **Database**: Azure SQL Edge (`mcr.microsoft.com/azure-sql-edge`)
- **Testing**: xUnit-based unit tests

## Getting Started

### Prerequisites

- [Docker](https://www.docker.com/get-started)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for local development)
- [Node.js & Angular CLI](https://angular.io/guide/setup-local) (for UI development)

### Running with Docker

1. Clone the repository:
https://github.com/cimey/VLK.git
Go to solution folder

3. Start all services:
   docker-compose up -d
   
4. Access the UI at [http://localhost:4200](http://localhost:4200).
5. API is available at [http://localhost:5119](http://localhost:5119).

### Environment Variables

- **SQL Server**:  
  - `SA_PASSWORD`: Set the SQL Server admin password.
  - `ACCEPT_EULA`: Must be `"Y"`.

- **API**:  
  - `ASPNETCORE_ENVIRONMENT`: Set to `"Development"` or `"Production"`.
  - `ConnectionStrings__Default`: Connection string for SQL Server.

- **UI**:  
  - `API_BASE_URL`: Base URL for API requests.

## Project Structure
- **API/**                  
  - .NET 8 Web API project 
- **Application/**          
  - Business logic and services 
- **Domain/**               
  - Entity models and interfaces 
- **Infrastructure/**       
  - Data access, configurations, migrations 
- **Model/**                
  - DTOs and request/response models 
- **UnitTest/**             
  - xUnit test projects 
- **docker-compose.yml**    
  - Docker Compose configuration 
- **API/Dockerfile**        
  - API Docker build file

## Configuration

- Update connection strings and environment variables in `docker-compose.yml` and `API/appsettings.json` as needed.
- Database migrations are managed via Entity Framework Core.

## Testing

- Unit tests are located in the `UnitTest/` directory.
- Run tests with:
dotnet test
