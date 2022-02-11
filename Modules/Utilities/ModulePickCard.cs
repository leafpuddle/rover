using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModulePickCard : ModuleBase<SocketCommandContext>
    {
        [Command("pickcard")]
        [Summary("Draws a random card from a deck of playing cards.")]
        public async Task PickCard()
        {
            string face;
            string suit;

            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));

            int randface = (rand.Next() % 13) + 1;
            int randsuit = (rand.Next() % 4) + 1;

            if (randface == 1) face = "Ace";
            else if (randface >= 2 && randface <= 10) face = randface.ToString();
            else if (randface == 11) face = "Jack";
            else if (randface == 12) face = "Queen";
            else face = "King";

            if (randsuit == 1) suit = "Clubs";
            else if (randsuit == 2) suit = "Hearts";
            else if (randsuit == 3) suit = "Spades";
            else suit = "Diamonds";

            Embed embed = new EmbedBuilder
            {
                Title = ":black_joker: Pick Card",
                Description = $"{face} of {suit}",
                Color = 0x9E845d,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}