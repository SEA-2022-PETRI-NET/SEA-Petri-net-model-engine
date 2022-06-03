# SEA-Petri-net-model-engine

[![CI](https://github.com/SEA-2022-PETRI-NET/SEA-Petri-net-model-engine/actions/workflows/ci.yml/badge.svg)](https://github.com/SEA-2022-PETRI-NET/SEA-Petri-net-model-engine/actions/workflows/ci.yml)

This project uses ASP.NET Core 6 to implement a web API for petri net modelling and simulation.

# Setup
Install .NET 6 and postgresql

In appsettings.json is the db connection string used. It's all default when installing postgresql except the password which you should change for user postgres to "Pass2020!". If you would like to use a GUI to inspect the tables of the underlying database you can use pgadmin/dbeaver for that.

Open the solution in the IDE of your choice (JetBrains Rider, VSCode etc.).

# Database 
For the database, we use the .Net entity framework called [EF Core](https://docs.microsoft.com/en-us/ef/core/). 
There already is a database migration, so after setting up postgresql then you can use "dotnet ef database update" in the csproj directory to apply the migration, i.e. create the database and schema.
Checkout [migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) for more info about how to change the db schema.
