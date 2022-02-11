using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleRollDice : ModuleBase<SocketCommandContext>
    {
        [Command("rolldice")]
        [Summary("Returns the result of a 20-sided dice roll.")]
        public async Task RollDice()
        {
            int result = GenerateResult(20);
            await ReplyAsync(embed: GenerateEmbed(20, result));
        }

        [Command("rolldice")]
        [Summary("Returns the result of an N-sided dice roll.")]
        public async Task RollDice(int sides)
        {
            if (sides <= 0 || sides > Int32.MaxValue)
            {
                await ReplyAsync(embed: GenerateError(
                    $"The number of sides must be greater than or equal to 1 and less than or equal to {Int32.MaxValue}."
                ));
                return;
            }

            int result = GenerateResult(sides);
            await ReplyAsync(embed: GenerateEmbed(sides, result));
        }

        int GenerateResult(int sides)
        {
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            return (rand.Next() % sides) + 1;
        }

        Embed GenerateEmbed(int sides, int result)
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = $":game_die: Roll Dice - {sides}-Sided Die",
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
                Title = ":game_die: Roll Dice - Error",
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