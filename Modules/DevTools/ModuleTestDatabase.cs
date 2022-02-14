using Discord;
using Discord.Commands;
using Rover;

namespace Rover.Modules
{
    public class ModuleTestDatabase : ModuleBase<SocketCommandContext>
    {
        [Command("testdb")]
        [Summary("Responds to a ping.")]
        public async Task TestDatabase()
        {
            await Database.ValidateUser(Context.User, ((IGuildUser)Context.User).Nickname);

            await ReplyAsync("If this says 'Giga Chad' the DB is working: " + Database.TestDatabase().Result);
        }
    }
}