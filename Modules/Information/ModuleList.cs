using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleList : ModuleBase<SocketCommandContext>
    {
        [Command("list")]
        [Summary("Provides a list of available command categories.")]
        public async Task List()
        {
            string listpage;

            try
            {
                listpage = System.IO.File.ReadAllText(@"./Lists/list.txt");
            }
            catch (DirectoryNotFoundException e)
            {
                await ReplyAsync(embed: GenerateError(
                    "System Error",
                    "Contact the Rover admins. The list directory is not found, which could mean there are issues with me.\n\n" + e
                ));

                return;
            }
            catch (FileNotFoundException e)
            {
                await ReplyAsync(embed: GenerateError(
                    "System Error",
                    "Contact the Rover admins. The category list is not found, which could mean there are issues with me.\n\n" + e
                ));

                return;
            }

            await ReplyAsync(embed: GenerateEmbed("Categories", listpage));
        }

        [Command("list")]
        [Summary("Provides a list of commands within a certain category")]
        public async Task List(string category)
        {
            string listpage;

            try
            {
                listpage = System.IO.File.ReadAllText(@$"./Lists/{category}.txt");
            }
            catch (DirectoryNotFoundException e)
            {
                await ReplyAsync(embed: GenerateError(
                    "System Error",
                    "Contact the Rover admins. The list directory is not found, which could mean there are issues with me.\n\n" + e
                ));

                return;
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                await ReplyAsync(embed: GenerateError(
                    "Category Not Found",
                    "This category is not found.\n" +
                    "Either the category name was typed incorrectly, or the category doesn't exist.\n" +
                    "If you think there should be a list for this category, contact the Rover admins."
                ));

                return;
            }

            await ReplyAsync(embed: GenerateEmbed(category, listpage));
        }

        Embed GenerateEmbed(string category, string listpage)
        {
            Embed embed = new EmbedBuilder
            {
                Title = $":bookmark_tabs: List - {category}",
                Description = listpage,
                Color = Config.COLOR_EMBED_TOOLTIP,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            return embed;
        }

        Embed GenerateError(string header, string message)
        {
            Embed embed = new EmbedBuilder
            {
                Title = $":bookmark_tabs: List - Error - {header}",
                Description = message,
                Color = Config.COLOR_EMBED_ERROR,
                Footer = new EmbedFooterBuilder
                {
                    Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                }
            }.Build();

            return embed;
        }
    }
}
