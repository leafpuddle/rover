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
            catch (DirectoryNotFoundException)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":warning: ERROR",
                    Description = "Contact the Rover admins. The list directory is not found, which could mean there are issues with me.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                    }
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }
            catch (FileNotFoundException)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":warning: ERROR",
                    Description = "Contact the Rover admins. The category list is not found, which could mean there are issues with me.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                    }
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }

            var msgembed = new EmbedBuilder
            {
                Title = ":bookmark_tabs: List - categories",
                Description = $"{listpage}",
                Color = 0x419BC4,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
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
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":bookmark_tabs: List - Category Not Found",
                    Description =
                        "This category is not found.\n" +
                        "Either the category name was typed incorrectly, or the category doesn't exist.\n" +
                        "If you think there should be a list for this category, contact the Rover admins.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}"
                    }
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }

            var msgembed = new EmbedBuilder
            {
                Title = $":bookmark_tabs: List - {category}",
                Description = $"{listpage}",
                Color = 0x419BC4,
                Footer = new EmbedFooterBuilder { Text = $"Response to {((IGuildUser)Context.User).Nickname ?? Context.User.Username}" }
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}
