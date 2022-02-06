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
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            var msgembed = new EmbedBuilder
            {
                Title = ":game_die: Dice Roll - 20-Sided Die",
                Description = ((rand.Next() % 20) + 1).ToString(),
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }

        [Command("rolldice")]
        [Summary("Returns the result of an N-sided dice roll.")]
        public async Task RollDice(int sides)
        {
            if (sides <= 0)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":game_die: Dice Roll - Error",
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
                Title = $":game_die: Dice Roll - {sides}-Sided Die",
                Description = ((rand.Next() % sides) + 1).ToString(),
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}