using Discord;
using Discord.Commands;
using System.Text;

namespace Rover.Modules
{
    public class ModuleTestDatabase : ModuleBase<SocketCommandContext>
    {
        [Command("testdb")]
        [Summary("Responds to a ping.")]
        public async Task TestDatabase()
        {
            await QueryModelRover.ValidateUser(Context.User, ((IGuildUser)Context.User).Nickname);

            ModelCard[] cards = await QueryModelTcg.GetCards(id: "apple", name: "Banana");

            if(cards.Length <= 0)
            {
                await ReplyAsync("Nothing returned.");
                return;
            }

            StringBuilder reply = new StringBuilder();

            foreach(ModelCard card in cards)
            {
                reply.Append
                (
                    $"{card.id}\n" +
                    $"Name: {card.name}\n" +
                    $"Description: {card.description}\n"
                );
            }

            await ReplyAsync(reply.ToString());
        }
    }
}