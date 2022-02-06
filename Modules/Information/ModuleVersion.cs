using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleVersion : ModuleBase<SocketCommandContext>
    {
        [Command("version")]
        [Summary("Provides version information for debugging purposes.")]
        public async Task Version()
        {
            var msgembed = new EmbedBuilder
            {
                Title = ":gear: Version",
                Description = "Rover: v0.1",
                Color = 0x7CA37D,
                Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}
