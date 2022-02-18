using Npgsql;

namespace Rover
{
    public static class QueryModelTcg
    {
        public static async Task<ModelCard[]> GetCards
        (
            string? id = null,
            string? name = null,
            string? desc = null,
            short? rarity = null,
            short? series = null,
            int? value = null,
            bool search = false
        )
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Database.connString);
            await conn.OpenAsync();

            string query = "SELECT id, name, description, rarity, series, value FROM tcg_cards";

            if(id != null || name != null || desc != null || rarity != null || series != null || value != null)
            {
                query += " WHERE ";
                List<string> arguments = new List<string>();

                if(id != null) arguments.Add("id = $1");
                if(name != null) arguments.Add("name = $2");
                if(desc != null) arguments.Add("description LIKE $3");
                if(rarity != null) arguments.Add("rarity = $4");
                if(series != null) arguments.Add("series = $5");
                if(value != null) arguments.Add("value = $6");

                query += string.Join(search ? " OR " : " AND ", arguments.ToArray());
            }

            await using var cmdGetCards = new NpgsqlCommand(query, conn)
            {
                Parameters =
                {
                    new() { Value = id ?? "" },
                    new() { Value = name ?? "" },
                    new() { Value = "%" + desc + "%" },
                    new() { Value = rarity ?? 0 },
                    new() { Value = series ?? 0 },
                    new() { Value = value ?? 0 }
                }
            };
            await cmdGetCards.PrepareAsync();
            await using var reader = await cmdGetCards.ExecuteReaderAsync();

            List<ModelCard> cards = new List<ModelCard>();

            while (await reader.ReadAsync())
            {
                cards.Add(new ModelCard
                {
                    id = reader.GetString(0),
                    name = reader.GetString(1),
                    description = reader.GetString(2),
                    rarity = (short)reader.GetValue(3),
                    series = (short)reader.GetValue(4),
                    value = (int)reader.GetValue(5)
                });
            }

            await conn.CloseAsync();

            return cards.ToArray();
        }

        public static async Task UpdateInventory(ulong user, string card, int value, bool overwrite = false)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Database.connString);
            await conn.OpenAsync();

            string query = 
                "INSERT INTO tcg_inventory (\"user\", card, quantity) " +
                "VALUES ($1, $2, GREATEST(0, $3)) " +
                "ON CONFLICT ON CONSTRAINT \"pkey-tcg_inventory\" DO UPDATE SET " +
                "quantity = GREATEST(0, ";
            if(!overwrite)
                query += "tcg_inventory.quantity + ";
            query += "$3);";

            await using var cmdUpdateInv = new NpgsqlCommand(query, conn)
            {
                Parameters =
                {
                    new() { Value = (long)user },
                    new() { Value = card },
                    new() { Value = value }
                }
            };
            await cmdUpdateInv.PrepareAsync();
            await cmdUpdateInv.ExecuteNonQueryAsync();

            await conn.CloseAsync();
        }

        public static async Task<DateTime> GetTimestamp(ulong user)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Database.connString);
            await conn.OpenAsync();

            DateTime returnValue = new DateTime();

            string query =
                "SELECT time FROM tcg_timestamps" +
                $" WHERE \"user\" = $1";

            await using var cmdGetTimestamp = new NpgsqlCommand(query, conn)
            {
                Parameters = { new() { Value = (long)user } }
            };
            await cmdGetTimestamp.PrepareAsync();
            await using var reader = await cmdGetTimestamp.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                returnValue = reader.GetDateTime(0);
            }

            await conn.CloseAsync();

            return returnValue;
        }

        public static async Task UpdateTimestamp(ulong user, DateTime time)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Database.connString);
            await conn.OpenAsync();

            string query =
                "INSERT INTO tcg_timestamps (\"user\", time) " +
                "VALUES ($1, $2) " +
                "ON CONFLICT ON CONSTRAINT \"pkey-tcg_timestamps\" DO UPDATE SET " +
                "time = $2";
            
            await using var cmdUpdateTimestamp = new NpgsqlCommand(query, conn)
            {
                Parameters =
                {
                    new() { Value = (long)user },
                    new() { Value = time }
                }
            };
            await cmdUpdateTimestamp.PrepareAsync();
            await cmdUpdateTimestamp.ExecuteNonQueryAsync();

            await conn.CloseAsync();
        }
    }
}