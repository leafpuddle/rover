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
            var msgembed = new EmbedBuilder
            {
                Title = ":satellite: Ping",
                Description = "Ping Received",
                Color = 0x7CA37D,
                Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}