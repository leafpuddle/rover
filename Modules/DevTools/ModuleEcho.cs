using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleEcho : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        [Summary("Echoes a specified message.")]
        public async Task Echo(string message)
        {
            await ReplyAsync(message);
        }

        public static string HelpMan()
        {
            return @"```
            echo: Echoes a string of text.

            Usage: !echo ""[Message]""

            Example: !echo ""This is a message Rover will repeat!""
            ```";
        }
    }
}