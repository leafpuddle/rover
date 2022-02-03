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
    }
}