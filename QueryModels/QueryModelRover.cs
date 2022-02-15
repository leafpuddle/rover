using Discord;
using Npgsql;

namespace Rover
{
    public static class QueryModelRover
    {
        public static async Task ValidateUser(IUser user, string? nickname = null)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Database.connString);
            await conn.OpenAsync();

            string query =
                "INSERT INTO r_users (id, username, discriminator, nickname) " +
                "VALUES ($1, $2, $3, $4) " +
                "ON CONFLICT (id) DO UPDATE SET " +
                "username = $2," +
                "discriminator = $3," +
                "nickname = $4;";

            await using NpgsqlCommand cmdCheckUser = new NpgsqlCommand(query, conn)
            {
                Parameters =
                {
                    new() { Value = (long)user.Id },
                    new() { Value = user.Username },
                    new() { Value = short.Parse(user.Discriminator) },
                    new() { Value = nickname ?? user.Username }
                }
            };
            await cmdCheckUser.PrepareAsync();
            await cmdCheckUser.ExecuteNonQueryAsync();

            await conn.CloseAsync();
        }
    }
}