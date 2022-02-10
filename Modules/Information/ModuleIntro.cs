using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleIntro : ModuleBase<SocketCommandContext>
    {
        [Command("intro")]
        [Summary("Provides an introduction to the functionality of the bot.")]
        public async Task Intro()
        {
            var msgembed = new EmbedBuilder
            {
                Title = ":robot: Introduction",
                Description = 
                    "Hi! I'm Rover.\n" +
                    "I'm a bot built specifically for this Discord server!\n" +
                    "I have a few different tricks I can do.\n\n" +
                    
                    "To get started, type !list to list out the different commands I respond to.\n" +
                    "If you ever have questions about how a command works, you can type !help [command] to see the manual.\n\n" +

                    "I'm very simple right now, but I encourage you to request new features.\n" +
                    "To make a request, simply let Finch or Vi know.\n" +
                    "If you want to get involved with my development, let us know!\n\n" +

                    "It's nice to meet you! Happy chatting!",
                Color = 0x419BC4,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}