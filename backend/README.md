## This codebase requires .NET 8 to be installed

### Install .NET 8

https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Install Entity Framework Core tools reference - .NET Core CLI

https://learn.microsoft.com/en-us/ef/core/cli/dotnet

```
dotnet tool install --global dotnet-ef
```

### Install PostgreSQL

Direct install: https://www.postgresql.org/download/

or

Using Docker: https://hub.docker.com/_/postgres/

Setup PostgreSQL and update the connection string in appsettings.Development.json accordingly:

`appsettings.Development.json`

```json
    ...
    "ConnectionStrings": {
		"Default": "Server=127.0.0.1;Port=5432;Database=app-bootstrap;User Id=postgres;Password=admin123;"
	},
    ...
```

### Get codebase

```
git clone https://github.com/AnNguyenLe/DonateHope.git
```

### Database Design Migration

From root folder DonateHope (Terminal view):

Step 1:

```
DonateHope % cd src/DonateHope.Infrastructure
```

Step 2:

```
DonateHope.Infrastructure % dotnet ef database update --startup-project ../DonateHope.WebAPI
```

### Start this project as an backend server

From root folder DonateHope (Terminal view):

Step 1:

```
DonateHope % cd src/DonateHope.WebAPI
```

Step 2:

```
DonateHope.WebAPI % dotnet build
```

Step 3:

```
DonateHope.WebAPI % dotnet watch
```
