using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleEcho : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        [Summary("Echoes a specified message.")]
        public async Task Echo([Remainder]string message)
        {
            Embed embed = new EmbedBuilder
            {
                Title = ":speaking_head: Echo",
                Description = $"{message}",
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