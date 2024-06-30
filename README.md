   # Software Engineering II - Base Code #

## Requirements ## 

 * You need to install the Microsoft .NET SDK 8 available at https://dotnet.microsoft.com/en-us/download
 * This solution was tested with version 8.0.6

## Basic Setup ## 

 * Fill .env file (you can copy or adapt from .env.sample if you want to run things locally)
 * Run the database with Docker
 * Update the database by running the current migrations 

## Migrations ##

 * You need to install (once) the tools for running migrations `dotnet tool install --global dotnet-ef`
 * To create a new migration you can do `dotnet ef migrations add MigrationName`
 * To update the database you can do `dotnet ef database update`

## Default Data ##

 * Roles: Admin, Normal
 * User: root@example.com, Password: root (Admin)
 * User: normal@example.com, Password: normal (Normal)
 * Permissions:
   * Sample Feature 1 (Normal and Admin)
   * Sample Feature 2 (Normal and Admin)
   * Sample Admin Feature (Admin)

## Notes / Rules and Recommendations ##

 * Never push the .env file to git
 * `docker compose up --build database` will allow you to create a local database for you to work

## TODO ##

 * The docker compose containers for the frontend and api are not working yet!