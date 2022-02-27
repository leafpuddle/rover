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
            Embed embed = new EmbedBuilder
            {
                Title = ":gear: Version",
                Description = "Rover: " + Config.VERSION,
                Color = Config.COLOR_EMBED_MESSAGE,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}
