using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleIntro : ModuleBase<SocketCommandContext>
    {
        [Command("intro")]
        [Summary("Provides an introduction to the functionality of the bot.")]
        public async Task Intro()
        {
            await ReplyAsync(
                "Hi! I'm Rover.\n" +
                "I'm a bot built specifically for this Discord server!\n" +
                "I have quite a few different tricks I can do.\n\n" +
                
                "To get started, type !list to list out the different commands I respond to.\n" +
                "If you ever have questions about how a command works, you can type !help [command] to see the manual.\n" +
                "It's nice to meet you! Happy chatting!"
            );
        }
    }
}