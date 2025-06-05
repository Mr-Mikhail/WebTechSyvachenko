# Sivach Kebab

Sivach Kebab is a demonstration restaurant platform built with ASP.NET Core. The solution contains a web application and several Azure Functions showcasing modern cloud integration.

## Overview

- **Framework**: .NET 6 MVC & Web API
- **Database**: MS SQL via Entity Framework Core
- **Cloud Services**: Azure Web Apps, Azure SQL, Azure Search, Azure Blob Storage, Azure Functions and Queues, Azure Key Vault
- **Authentication**: ASP.NET Core Identity with Google login
- **CI/CD**: GitHub Actions for automated builds, deployment and API testing

## Repository Structure

```
OnlineShop/          ASP.NET Core MVC & API project
Sivach.Function/     Azure Functions project
Tests/               Insomnia API test suite
WebTechSyvachenko.sln Solution file
```

## Core Features

### Web Application

- CRUD APIs for dishes, categories, reviews and photos
- In-memory caching and paginated API responses
- Google Maps integration to display restaurant locations
- Azure Search powered dish search endpoint
- File uploads and downloads via Azure Blob Storage
- Exchange rate page powered by Privat24 API
- Sample "Fortune Teller" feature

### Azure Functions

- `SimpleHttpFunction` – basic HTTP trigger
- `QueueRequest` – HTTP endpoint that enqueues messages
- `AsynchronousProcess` – queue trigger that posts fortunes back to the site

## Development

```bash
# build the solution
$ dotnet build

# run the web application
$ dotnet run --project OnlineShop/OnlineShop.csproj
```

The web app uses `appsettings.Development.json` for the connection string. The provided Dockerfile can be used to containerize the application.

## Testing

API tests are defined in `Tests/insomnia.json`. They can be executed with Inso:

```bash
$ inso run test "sivach.yaml" --ci --env "Production" --src ./Tests/insomnia.json
```

## Continuous Deployment

Two GitHub workflows are included:

- **main_sivach.yml** – builds the solution and deploys the web app to Azure Web App
- **api_test.yml** – runs the Insomnia test suite after deployment

These workflows enable automated delivery whenever code is pushed to the `main` branch.


