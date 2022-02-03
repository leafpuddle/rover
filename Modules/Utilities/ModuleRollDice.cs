using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleRollDice : ModuleBase<SocketCommandContext>
    {
        [Command("rolldice")]
        [Summary("Returns the result of a 20-sided dice roll.")]
        public async Task RollDice()
        {
            Random rand = new Random(int.Parse(DateTime.Now.ToString("yyMMddHHmmss")));
            await ReplyAsync(((rand.Next() % 20) + 1).ToString());
        }

        [Command("rolldice")]
        [Summary("Returns the result of an N-sided dice roll.")]
        public async Task RollDice(int sides)
        {
            if (sides <= 0)
            {
                await ReplyAsync("The number specified must be 1 or greater.");
                return;
            }

            Random rand = new Random(int.Parse(DateTime.Now.ToString("yyMMddHHmmss")));
            await ReplyAsync(((rand.Next() % sides) + 1).ToString());
        }
    }
}