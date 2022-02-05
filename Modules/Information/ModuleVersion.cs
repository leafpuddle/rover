using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleVersion : ModuleBase<SocketCommandContext>
    {
        [Command("version")]
        [Summary("Provides version information for debugging purposes.")]
        public async Task Version()
        {
            await ReplyAsync("Rover: v0.1");
        }
    }
}
