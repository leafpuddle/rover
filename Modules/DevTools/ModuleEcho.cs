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
            Embed embed = new EmbedBuilder
            {
                Title = ":speaking_head: Echo",
                Description = $"{message}",
                Color = 0x8A439C,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}