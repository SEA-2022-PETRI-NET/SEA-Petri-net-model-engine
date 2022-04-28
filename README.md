# SEA-Petri-net-model-engine

[![CI](https://github.com/SEA-2022-PETRI-NET/SEA-Petri-net-model-engine/actions/workflows/ci.yml/badge.svg)](https://github.com/SEA-2022-PETRI-NET/SEA-Petri-net-model-engine/actions/workflows/ci.yml)

This project uses ASP.NET Core 6 to implement a web API for petri net modelling and simulation.

# Setup
Install .NET 6
Install postgresql

In appsettings.json is the db connection string used. It's all default when installing postgresql except the password which you should change for user postgres to "Pass2020!". If you would like to use a GUI to inspect the tables of the underlying database you can use pgadmin for that.

Open the solution in the IDE of your choice
There already is a database migration, so after setting up postgresql then you can use "dotnet ef database update" in the csproj directory to apply the migration, i.e. create the database and schema

Build and run the project
