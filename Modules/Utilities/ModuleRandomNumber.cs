using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleRandomNumber : ModuleBase<SocketCommandContext>
    {
        [Command("randomnumber")]
        [Summary("Returns a random number between 1 and 100.")]
        public async Task RandomNumber()
        {
            int result = GenerateResult(1, 100);

            await ReplyAsync(embed: GenerateEmbed(1, 100, result));
        }

        [Command("randomnumber")]
        [Summary("Returns a random number between 1 and a number.")]
        public async Task RandomNumber(int a)
        {
            if (a < Int32.MinValue || a > Int32.MaxValue)
            {
                await ReplyAsync(embed: GenerateError(
                    $"Number must be between {Int32.MinValue} and {Int32.MaxValue}."
                ));

                return;
            }

            int lower, upper, result;

            lower = Math.Min(1, a);
            upper = Math.Max(1, a);
            result = GenerateResult(lower, upper);

            await ReplyAsync(embed: GenerateEmbed(lower, upper, result));
        }

        [Command("randomnumber")]
        [Summary("Returns a random number between the lower value and upper value.")]
        public async Task RandomNumber(int a, int b)
        {
            if (a < Int32.MinValue || a > Int32.MaxValue || b < Int32.MinValue || b > Int32.MaxValue)
            {
                await ReplyAsync(embed: GenerateError(
                    $"Numbers must be between {Int32.MinValue} and {Int32.MaxValue}."
                ));

                return;
            }

            int lower, upper, result;

            lower = Math.Min(a, b);
            upper = Math.Max(a, b);
            result = GenerateResult(lower, upper);

            await ReplyAsync(embed: GenerateEmbed(lower, upper, result));
        }

        int GenerateResult(int lower, int upper)
        {
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            return (rand.Next() % ((upper + 1) - lower)) + lower;
        }

        Embed GenerateEmbed(int lower, int upper, int result)
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = $":grey_question: Random Number - {lower} to {upper}",
                Description = $"{result}",
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            };

            return embed.Build();
        }

        Embed GenerateError(string message)
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = ":grey_question: Random Number - Error",
                Description = message,
                Color = 0xC25955,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            };

            return embed.Build();
        }
    }
}