# Commands

## Run below commands to run backend project
````
dotnet run --project=src/ERP.WebApi/ERP.WebApi.csproj

dotnet build --project=src/ERP.WebApi/ERP.WebApi.csproj

dotnet clean

dotnet restore
````

## Run below commands to run frontend project
````
cd src/ERP.Web
ng serve
ng build
````

## Run below commands in sequence to create architecture for backend
````
mkdir ERP
cd ERP
dotnet new sln -n=ERP
mkdir src

dotnet new classlib -o src/ERP.Domain
dotnet new classlib -o src/ERP.Infrastructure
dotnet new classlib -o src/ERP.Application
dotnet new webapi -o src/ERP.WebApi
dotnet new classlib -o src/ERP.DbMigrations

dotnet sln add src/ERP.Domain/ERP.Domain.csproj
dotnet sln add src/ERP.Infrastructure/ERP.Infrastructure.csproj
dotnet sln add src/ERP.Application/ERP.Application.csproj
dotnet sln add src/ERP.WebApi/ERP.WebApi.csproj
dotnet sln add src/ERP.DbMigrations/ERP.DbMigrations.csproj

dotnet add src/ERP.Infrastructure/ERP.Infrastructure.csproj reference src/ERP.Domain/ERP.Domain.csproj

dotnet add src/ERP.Application/ERP.Application.csproj reference src/ERP.Domain/ERP.Domain.csproj
dotnet add src/ERP.Application/ERP.Application.csproj reference src/ERP.Infrastructure/ERP.Infrastructure.csproj

dotnet add src/ERP.WebApi/ERP.WebApi.csproj reference src/ERP.Domain/ERP.Domain.csproj
dotnet add src/ERP.WebApi/ERP.WebApi.csproj reference src/ERP.Application/ERP.Application.csproj
dotnet add src/ERP.WebApi/ERP.WebApi.csproj reference src/ERP.Infrastructure/ERP.Infrastructure.csproj
dotnet add src/ERP.WebApi/ERP.WebApi.csproj reference src/ERP.DbMigrations/ERP.DbMigrations.csproj

dotnet add src/ERP.DbMigrations/ERP.DbMigrations.csproj reference src/ERP.Infrastructure/ERP.Infrastructure.csproj
````

## Run below commands to create frontend app
````
cd src
ng new ERP.Web
````

## Commands to Update Db Migrations
In ERP.DbMigrations Project run below commands
````
dotnet ef migrations add InitialCreate --startup-project ../ERP.WebApi/ERP.WebApi.csproj // Use this command to create new migration
````