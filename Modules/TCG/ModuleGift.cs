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
            Embed embed = new EmbedBuilder
            {
                Title = ":gift: You've Received A Gift",
                Description = "Ping Received",
                Color = 0x7CA37D,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}