using Discord;
using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleHelp : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Provides instructions for using the help command.")]
        public async Task Help()
        {
            string manpage;

            try
            {
                manpage = System.IO.File.ReadAllText(@"./Manuals/help.txt");
            }
            catch (DirectoryNotFoundException)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":warning: ERROR",
                    Description = "Contact the Rover admins. The manual directory is not found, which could mean there are issues with me.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }
            catch (FileNotFoundException)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":warning: ERROR",
                    Description = "Contact the Rover admins. The help manual is not found, which could mean there are issues with me.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }

            var msgembed = new EmbedBuilder
            {
                Title = ":notebook_with_decorative_cover: Manual - help",
                Description = $"{manpage}",
                Color = 0x419BC4,
                Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
            };
            await ReplyAsync(embed: msgembed.Build());
        }

        [Command("help")]
        [Summary("Provides instructions for using a command.")]
        public async Task Help(string command)
        {
            string manpage;

            try
            {
                manpage = System.IO.File.ReadAllText(@$"./Manuals/{command}.txt");
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                var errembed = new EmbedBuilder
                {
                    Title = ":notebook_with_decorative_cover: Manual - Command Not Found",
                    Description =
                        "This command is not found.\n" +
                        "Either the command name was typed incorrectly, or the command doesn't exist.\n" +
                        "If you think there should be a manual for this command, contact the Rover admins.",
                    Color = 0xC25955,
                    Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
                };
                await ReplyAsync(embed: errembed.Build());
                return;
            }

            var msgembed = new EmbedBuilder
            {
                Title = $":notebook_with_decorative_cover: Manual - {command}",
                Description = $"{manpage}",
                Color = 0x419BC4,
                Footer = new EmbedFooterBuilder { Text = $"Response to {Context.User.Username}" },
            };
            await ReplyAsync(embed: msgembed.Build());
        }
    }
}
