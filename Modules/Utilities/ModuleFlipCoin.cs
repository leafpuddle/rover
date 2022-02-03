using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleFlipCoin : ModuleBase<SocketCommandContext>
    {
        [Command("flipcoin")]
        [Summary("Returns the result of a coin flip.")]
        public async Task FlipCoin()
        {
            Random rand = new Random(int.Parse(DateTime.Now.ToString("yyMMddHHmmss")));
            if ((rand.Next() % 2) == 1) await ReplyAsync("Heads");
            else await ReplyAsync("Tails");
        }
    }
}