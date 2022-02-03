using Discord.Commands;

namespace Rover.Modules
{
    public class ModulePing : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Responds to a ping.")]
        public async Task Ping()
        {
            await ReplyAsync("Ping received.");
        }

        public static string HelpMan()
        {
            return @"```
            ping: Responds if the ping command is received.

            Usage: !ping
            ```";
        }
    }
}