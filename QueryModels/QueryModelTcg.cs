using Npgsql;
using System.Text;

namespace Rover
{
    public static class QueryModelTcg
    {
        public static async Task<ModelCard[]> GetCards
        (
            string? id = null, string? name = null, string? desc = null, short? rarity = null, short? series = null, int? value = null
        )
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(Database.connString);
            await conn.OpenAsync();

            StringBuilder query = new StringBuilder();

            query.Append("SELECT id, name, description, rarity, series, value FROM tcg_cards");

            if(id != null || name != null || desc != null || rarity != null || series != null || value != null)
            {
                query.Append(" WHERE ");
                bool includeAnd = false;

                if(id != null)
                {
                    query.Append($"id = '{id}'");
                    includeAnd = true;
                }
                if(name != null)
                {
                    if(includeAnd) query.Append(" AND ");
                    query.Append($"name = '{name}'");
                    includeAnd = true;
                }
                if(desc != null)
                {
                    if(includeAnd) query.Append(" AND ");
                    query.Append($"description LIKE '%{desc}%'");
                    includeAnd = true;
                }
                if(rarity != null)
                {
                    if(includeAnd) query.Append(" AND ");
                    query.Append($"rarity = {rarity}");
                    includeAnd = true;
                }
                if(series != null)
                {
                    if(includeAnd) query.Append(" AND ");
                    query.Append($"series = {series}");
                    includeAnd = true;
                }
                if(value != null)
                {
                    if(includeAnd) query.Append(" AND ");
                    query.Append($"value = {value}");
                }
            }

            await using var cmdGetCards = new NpgsqlCommand(query.ToString(), conn);
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
    }
}