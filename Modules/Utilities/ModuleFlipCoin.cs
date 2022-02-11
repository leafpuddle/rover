using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleFlipCoin : ModuleBase<SocketCommandContext>
    {
        [Command("flipcoin")]
        [Summary("Returns the result of a coin flip.")]
        public async Task FlipCoin()
        {
            string result;

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));

            if ((rand.Next() % 2) == 1) result = "Heads!";
            else result = "Tails!";

            Embed embed = new EmbedBuilder
            {
                Title = ":coin: Flip Coin",
                Description = result,
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}