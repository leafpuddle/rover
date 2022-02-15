using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleGift : ModuleBase<SocketCommandContext>
    {
        [Command("gift")]
        [Summary("Provides a gifted card to the requester.")]
        public async Task Gift()
        {
            await QueryModelRover.ValidateUser(Context.User, ((IGuildUser)Context.User).Nickname);

            ModelCard[] drawpool = QueryModelTcg.GetCards().Result;
            Random rand = new Random(int.Parse(DateTime.Now.ToString("MMddHHmmss")));
            ModelCard card = drawpool[rand.Next() % drawpool.Length];

            await QueryModelTcg.UpdateInventory(Context.User.Id, card.id, 1);

            Embed embed = new EmbedBuilder
            {
                Title = $":gift: You've Received A Gift - {card.name}",
                Description =
                    card.description + "\n\n" +
                    "[pretend theres an image here]",
                Color = 0x7CA37D,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}