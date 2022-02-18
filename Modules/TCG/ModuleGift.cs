using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleGift : ModuleBase<SocketCommandContext>
    {
        [Command("gift")]
        [Summary("Provides a gifted card to the requester.")]
        public async Task Gift()
        {
            await QueryModelRover.ValidateUser(Context.User, ((IGuildUser)Context.User).Nickname);

            DateTime timestamp = QueryModelTcg.GetTimestamp(Context.User.Id).Result;

            if(
                timestamp != null &&
                (DateTime.Now - timestamp).TotalHours < 1
            )
            {
                await ReplyAsync(embed: GenerateError(
                    "Too Early",
                    "You can only claim a gift once per hour!"
                ));

                return;
            }

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));

            int drawRarity = rand.Next(0, 100);
            short rarity = drawRarity switch
            {
                int n when n <= 46 => 1,
                int n when n > 46 && n <= 76 => 2,
                int n when n > 76 && n <= 91 => 3,
                int n when n > 91 && n <= 98 => 4,
                int n when n == 99 => 5,
                _ => 1
            };

            ModelCard[] drawpool = QueryModelTcg.GetCards(rarity: rarity).Result;
            ModelCard card = drawpool[rand.Next(0, drawpool.Length)];

            await QueryModelTcg.UpdateInventory(Context.User.Id, card.id, 1);
            await QueryModelTcg.UpdateTimestamp(Context.User.Id, DateTime.Now);

            Color embedColor = card.rarity switch
            {
                1 => Config.COLOR_EMBED_TCG_COMMON,
                2 => Config.COLOR_EMBED_TCG_UNCOMMON,
                3 => Config.COLOR_EMBED_TCG_RARE,
                4 => Config.COLOR_EMBED_TCG_LEGENDARY,
                5 => Config.COLOR_EMBED_TCG_MYTHIC,
                _ => Config.COLOR_EMBED_ERROR
            };

            await ReplyAsync(embed: GenerateEmbed(card.id, card.name, card.description, embedColor));
        }

        Embed GenerateEmbed(string id, string name, string desc, Color color)
        {
            Embed embed = new EmbedBuilder
            {
                Title = $":gift: You've Received A Gift - {name}",
                Description = desc,
                ImageUrl = @$"https://rover.us-east-1.linodeobjects.com/{id}.png",
                Color = color,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            return embed;
        }

        Embed GenerateError(string title, string message)
        {
            Embed embed = new EmbedBuilder
            {
                Title = $":gift: Error - {title}",
                Description = message,
                Color = Config.COLOR_EMBED_ERROR,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            return embed;
        }
    }
}