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
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            if ((rand.Next() % 2) == 1)
            {
                var msgembed = new EmbedBuilder
                {
                    Title = ":coin: Coin Flip",
                    Description = "Heads!",
                    Color = 0x9E845d,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                    }
                };
                await ReplyAsync(embed: msgembed.Build());
            }
            else
            {
                var msgembed = new EmbedBuilder
                {
                    Title = ":coin: Coin Flip",
                    Description = "Tails!",
                    Color = 0x9E845d,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                    }
                };
                await ReplyAsync(embed: msgembed.Build());
            }
        }
    }
}