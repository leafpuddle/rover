# Rover - A Discord Bot

A custom bot for the private server "This Place Is Not A Place Of Honor."

https://rover.leafpuddle.io

## Requires

- .NET 6.0
- PostgreSQL v13.0+

## Dependencies

- Discord.NET v3.3.2
- MSBuild v17.0.0
- Npgsql v6.0.3

In order to update your dependencies, use `dotnet restore`.

## Variables

You will need to set the following environment variables when running this bot in order for it to correctly operate:

- `rover_botToken` - The bot token provided by Discord
- `rover_dbHost` - The hostname of the database
- `rover_dbName` - The name of the database
- `rover_dbUser` - The username to log into the database
- `rover_dbPass` - The password for the user
- `rover_dbPort` - The port to connect to the database on

## Building

Right after cloning the repo, use `dotnet restore` in the project root to update the project dependencies. It's a good idea to do this after updating the repo during early dev, as the packages may change more frequently. To build, use `dotnet build`. You can build and then immediately run with `dotnet run`

## Authors

- Vi Roesler - vroesler@leafpuddle.com
- Joshua Finch - jfinch@faultybranches.net