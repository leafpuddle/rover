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
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            var msgembed = new EmbedBuilder
            {
                Title = ":grey_question: Random Number - 1 to 100",
                Description = ((rand.Next() % 100) + 1).ToString(),
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }

        [Command("randomnumber")]
        [Summary("Returns a random number between 1 and a maximum number.")]
        public async Task RandomNumber(int a)
        {
            if (a < 1)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":grey_question: Random Number - Error",
                    Description = "The value must be greater than or equal to 1.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                    }
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            var msgembed = new EmbedBuilder
            {
                Title = $":grey_question: Random Number - 1 to {a}",
                Description = ((rand.Next() % a) + 1).ToString(),
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }

        [Command("randomnumber")]
        [Summary("Returns a random number between the lower value and upper value.")]
        public async Task RandomNumber(int a, int b)
        {
            int lower, upper;

            if (a == b)
            {
                var msgembed1 = new EmbedBuilder
                {
                    Title = $":grey_question: Random Number - {a} to {b}",
                    Description = a.ToString(),
                    Color = 0x9E845d,
                    Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
                };
                await ReplyAsync(embed: msgembed1.Build());
                await ReplyAsync(a.ToString());
                return;
            }
            
            lower = Math.Min(a, b);
            upper = Math.Max(a, b);

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            var msgembed = new EmbedBuilder
            {
                Title = $":grey_question: Random Number - {lower} to {upper}",
                Description = ((rand.Next() % ((upper + 1) - lower)) + lower).ToString(),
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}