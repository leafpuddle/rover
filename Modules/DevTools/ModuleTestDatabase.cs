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
            await Database.ValidateUser(Context.User, ((IGuildUser)Context.User).Nickname);

            ModelCard[] cards = await QueryModelTcg.GetCards(desc: "Tart");

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