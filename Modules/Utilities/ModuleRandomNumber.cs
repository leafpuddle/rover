using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleRandomNumber : ModuleBase<SocketCommandContext>
    {
        [Command("randomnumber")]
        [Summary("Returns a random number between 1 and 100.")]
        public async Task RandomNumber()
        {
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            await ReplyAsync(((rand.Next() % 100) + 1).ToString());
        }

        [Command("randomnumber")]
        [Summary("Returns a random number between 1 and a maximum number.")]
        public async Task RandomNumber(int a)
        {
            if (a < 1)
            {
                await ReplyAsync("The value must be greater than or equal to 1.");
                return;
            }

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            await ReplyAsync(((rand.Next() % a) + 1).ToString());
        }

        [Command("randomnumber")]
        [Summary("Returns a random number between the lower value and upper value.")]
        public async Task RandomNumber(int a, int b)
        {
            int lower, upper;

            if (a == b)
            {
                await ReplyAsync(a.ToString());
                return;
            }
            
            lower = Math.Min(a, b);
            upper = Math.Max(a, b);

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            await ReplyAsync(((rand.Next() % (upper - lower)) + lower).ToString());
        }
    }
}