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
            catch (DirectoryNotFoundException e)
            {
                await ReplyAsync(embed: GenerateError(
                    "System Error",
                    "Contact the Rover admins. The manual directory is not found, which could mean there are issues with me.\n\n" + e
                ));

                return;
            }
            catch (FileNotFoundException e)
            {
                await ReplyAsync(embed: GenerateError(
                    "System Error",
                    "Contact the Rover admins. The help manual is not found, which could mean there are issues with me.\n\n" + e
                ));

                return;
            }

            await ReplyAsync(embed: GenerateEmbed("help", manpage));
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
            catch (DirectoryNotFoundException e)
            {
                await ReplyAsync(embed: GenerateError(
                    "System Error",
                    "Contact the Rover admins. The manual directory is not found, which could mean there are issues with me.\n\n" + e
                ));

                return;
            }
            catch (FileNotFoundException)
            {
                await ReplyAsync(embed: GenerateError(
                    "Command Not Found",
                    "This command is not found.\n" +
                    "Either the command name was typed incorrectly, or the command doesn't exist.\n" +
                    "If you think there should be a manual for this command, contact the Rover admins."
                ));

                return;
            }

            await ReplyAsync(embed: GenerateEmbed(command, manpage));
        }

        Embed GenerateEmbed(string command, string manpage)
        {
            Embed embed = new EmbedBuilder
            {
                Title = $":notebook_with_decorative_cover: Manual - {command}",
                Description = $"{manpage}",
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
                Title = $":notebook_with_decorative_cover: Manual - Error - {header}",
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
