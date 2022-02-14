using Discord;
using Npgsql;

namespace Rover
{
    public static class Database
    {
        private static string? databaseName;
        private static string? databaseHost;
        private static string? databaseUser;
        private static string? databasePass;
        private static string? databasePort;
        private static string connString;

        static Database()
        {
            databaseName = Environment.GetEnvironmentVariable("rover_dbName");
            databaseHost = Environment.GetEnvironmentVariable("rover_dbHost");
            databasePass = Environment.GetEnvironmentVariable("rover_dbPass");
            databaseUser = Environment.GetEnvironmentVariable("rover_dbUser");
            databasePort = Environment.GetEnvironmentVariable("rover_dbPort");

            connString =
                $"Host={databaseHost};" +
                $"Username={databaseUser};" +
                $"Password={databasePass};" +
                $"Database={databaseName};" +
                $"Port={databasePort};";
        }

        public static async Task<string> TestDatabase()
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT name FROM tcg_cards WHERE id='giga_chad'", conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            string returnValue = "didn't work";

            while (await reader.ReadAsync())
            {
                returnValue = reader.GetString(0);
            }

            conn.Close();

            return returnValue;
        }

        public static async Task ValidateUser(IUser user, string? nickname = null)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using NpgsqlCommand cmdCheckUser = new NpgsqlCommand(
                "INSERT INTO r_users (id, username, discriminator, nickname) " +
                $"VALUES ({user.Id}, '{user.Username}', {user.Discriminator}, '{nickname ?? user.Username}') " +
                "ON CONFLICT (id) DO UPDATE SET " +
                $"username = '{user.Username}'," +
                $"discriminator = {user.Discriminator}," +
                $"nickname = '{nickname ?? user.Username}';",
                conn
            );
            await cmdCheckUser.ExecuteNonQueryAsync();

            await conn.CloseAsync();
        }
    }
}