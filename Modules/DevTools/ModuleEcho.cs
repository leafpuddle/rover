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
    }
}