using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleEcho : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        [Summary("Echoes a specified message.")]
        public async Task Echo(string message)
        {
            var msgembed = new EmbedBuilder
            {
                Title = ":speaking_head: Echo",
                Description = $"{message}",
                Color = 0x8A439C,
                Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}