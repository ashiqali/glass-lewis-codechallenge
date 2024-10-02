# GLASS LEWIS - Code Challenge

## Overview - Company Portal 

This project provides a RESTful Web API for managing company records. The API allows users to create, retrieve, update, and list company records with validations, using a SQL Server database for persistence.


## Table of Contents

- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Authentication](#authentication)
- [Versioning](#versioning)
- [Logging](#logging)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)


## Technologies Used

### Backend
- .NET Core 8
- Entity Framework Core
- JWT Authentication
- AutoMapper
- Swagger
- xUnit

### Frontend
- React 18
- TypeScript
- Vite

## Setup and Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/)
- [Visual Studio or Visual Studio Code](https://visualstudio.microsoft.com/)

### Backend Setup

1. Clone the repository:
    ```sh
    https://github.com/ashiqali/glass-lewis-codechallenge.git
    ```

2. Navigate to the backend project directory:
    ```sh
    cd company-portal-api
    ```

3. Restore the .NET dependencies:
    ```sh
    dotnet restore
    ```

### Frontend Setup

1. Navigate to the frontend project directory:
    ```sh
    cd company-portal-app
    ```

2. Install the npm dependencies:
    ```sh
    npm install
    ```

### Database Setup

1. Update Connection String:

In the appsettings.json file, update the ConnectionStrings to point to your SQL Server instance.:
```sh
{
  "ConnectionStrings": {
    "DefaultConnection": "Database=CompanyPortalDB;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;"
  }
}

```

2. Run Database Migrations:

SQLServer has been used as database.
You can change connection string from appsettings.json within CompanyPortal.API and
apply database migrations to create the tables. From a command line :
 ```sh
    cd CompanyPortal.DAL
    update-database
```
Also, I have attached the script file inside the root folder. If restore db migration fails you can the use the this script to setup the database.
```sh 
 cd glass-lewis-codechallenge/DBScript.sql 
```

## Running the Application

### Backend

Start the .NET backend:
```sh
cd company-portal-api
dotnet run
```

### Frontend

Start the React frontend:
```sh
npm start
```

The backend will be running at `https://localhost:5000` and the frontend at `http://localhost:3000`.

## Testing

### Demo Login 

Credentials:
```sh
http://localhost:3000
Username: admin
Password: 123
```

### Backend

To run the tests for the backend:
```sh
cd CompanyPortal.Test
dotnet test
```
### Swagger
```sh
https://localhost:5000/swagger/index.html
```

## Authentication
- **JWT Authentication**: 
  - Authentication has been added. Endpoints require a JWT token; otherwise, a 401 response will be returned.
  - Users must log in or register to obtain a JWT token.
  - The token must be included in the Authorization header for requests:
    - Click the lock icon next to the particular endpoint and paste the token in the textbox on the Swagger page.
    - Alternatively, add the token to the Authorization header of the request.

To disable authentication for specific endpoints, remove the `Authorize` attribute from the respective controller.

## Versioning
- URL versioning has been implemented, currently supporting two versions.
- Versions can be changed via the Swagger page or by providing the version number in the URL.

## Logging
- **Structured Logging**: 
  - Serilog has been implemented for structured logging.
  - Logs are written to both file and console.
  - Configuration settings can be found in `appsettings.json`.
  - Examples of log message formatting and different logging levels are available in the `UserService`.
  - Logging will be improved over time.

## Project Structure

### Backend

```
Solution 'CompanyPortal' (5 of 5 projects)
├── CompanyPortal.API
│   ├── Connected Services
│   ├── Dependencies
│   ├── Properties
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── CompanyController.cs
│   │   ├── UserController.cs
│   ├── appsettings.json
│   ├── CompanyPortal.API.http
│   └── Program.cs
├── CompanyPortal.BLL
│   ├── Dependencies
│   ├── Services
│   │   ├── IServices
│   │   │   ├── IAuthService.cs
│   │   │   ├── ICompanyService.cs
│   │   │   └── IUserService.cs
│   │   ├── AuthService.cs
│   │   ├── CompanyService.cs
│   │   └── UserService.cs
│   ├── Utilities
│   │   ├── AutoMapperP
│   │   │   └── AutoMapperProfiles.cs
│   │   ├── CustomExceptions
│   │   │   ├── BadRequestException.cs
│   │   │   ├── CompanyNotFoundException.cs
│   │   │   ├── DuplicateIsinException.cs
│   │   │   ├── DuplicateUserException.cs
│   │   │   └── UserNotFoundException.cs
│   │   └── Settings
│   │       └── JwtSettings.cs
│   ├── Swagger
│   │   └── SwaggerConfigurationOptions.cs
│   ├── Validators
│   │   ├── UserToLoginDTOValidator.cs
│   │   └── UserToRegisterDTOValidator.cs
│   └── DependencyInjection.cs
├── CompanyPortal.DAL
│   ├── Dependencies
│   ├── DataContext
│   │   └── CompanyPortalDbContext.cs
│   ├── Entities
│   │   ├── Company.cs
│   │   └── User.cs
│   ├── Migrations
│   │   ├── 20240930222916_InitialMigration.cs
│   │   └── CompanyPortalDbContextModelSnapshot.cs
│   ├── Repositories
│   │   ├── IRepositories
│   │   │   ├── ICompanyRepository.cs
│   │   │   ├── IGenericRepository.cs
│   │   │   └── IUserRepository.cs
│   │   ├── CompanyRepository.cs
│   │   ├── GenericRepository.cs
│   │   └── UserRepository.cs
│   └── DependencyInjection.cs
├── CompanyPortal.DTO
│   ├── Dependencies
│   ├── DTOs
│   │   ├── Company
│   │   │   └── CompanyDTO.cs
│   │   ├── Jwt
│   │   │   ├── RefreshTokenDTO.cs
│   │   │   └── RefreshTokenToReturnDTO.cs
│   │   ├── User
│   │   │   ├── UserDTO.cs
│   │   │   ├── UserToAddDTO.cs
│   │   │   ├── UserToLoginDTO.cs
│   │   │   ├── UserToRegisterDTO.cs
│   │   │   ├── UserToReturnDTO.cs
│   │   │   └── UserToUpdateDTO.cs
├── CompanyPortal.Test
│   ├── Dependencies
│   ├── Helpers
│   │   └── CustomWebApplicationFactory.cs
│   ├── IntegrationTests
│   │   ├── API
│   │   │   ├── CompanyControllerTests.cs
│   │   │   └── UserControllerTests.cs
│   ├── UnitTests
│   │   ├── BLL
│   │   │   └── Services
│   │   │       ├── CompanyServiceTests.cs
│   │   │       └── UserServiceTests.cs
│   │   └── Validators
│   │       ├── UserToLoginDTOValidatorTests.cs
│   │       └── UserToRegisterDTOValidatorTests.cs

```

### Frontend

```
COMPANY-PORTAL-APP
├── node_modules
├── public
├── src
│   ├── api
│   │   ├── authApi.ts
│   │   └── companyApi.ts
│   ├── components
│   │   ├── CompanyForm.tsx
│   │   ├── CompanyList.tsx
│   │   ├── LoginForm.tsx
│   │   ├── MainLayout.tsx
│   │   └── RegisterForm.tsx
│   ├── config
│   │   └── config.ts
│   ├── pages
│   │   ├── CompanyPage.tsx
│   │   ├── FooterPage.tsx
│   │   ├── HeaderPage.tsx
│   │   ├── LoginPage.tsx
│   │   ├── PrivateRoute.tsx
│   │   └── RegisterPage.tsx
│   ├── types
│   │   ├── auth.ts
│   │   └── company.ts
│   ├── App.css
│   ├── App.test.tsx
│   ├── App.tsx
│   ├── declarations.d.ts
│   ├── index.css
│   ├── index.tsx
│   ├── logo.svg
│   ├── react-app-env.d.ts
│   ├── reportWebVitals.ts
│   ├── setupTests.ts
├── .gitignore
├── package-lock.json
├── package.json
├── README.md
└── tsconfig.json

```

## Screenshots
![Screenshot-1](https://github.com/user-attachments/assets/4235cb5a-f9cf-4744-8267-d58e93725aba)

![Screenshot-2](https://github.com/user-attachments/assets/6a250320-b156-4b8b-9891-17e4e42ec4d9)

![Screenshot-3](https://github.com/user-attachments/assets/59cfd07f-137b-4a7d-bc16-318aaeebae9c)

![Screenshot-4](https://github.com/user-attachments/assets/6d1df437-968c-4c71-ae12-9d2c8425834e)

![Screenshot-5](https://github.com/user-attachments/assets/e13ae6fb-d917-48e9-8d0e-01a181fcda9d)




---

