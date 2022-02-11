using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModulePing : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Responds to a ping.")]
        public async Task Ping()
        {
            Embed embed = new EmbedBuilder
            {
                Title = ":satellite: Ping",
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