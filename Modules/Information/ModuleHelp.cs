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
                await ReplyAsync("Contact the Rover admins. The manual directory is not found, which could mean there are issues with me.");
                return;
            }
            catch (FileNotFoundException)
            {
                await ReplyAsync("Contact the Rover admins. The help manual is not found, which could mean there are issues with me.");
                return;
            }

            await ReplyAsync(manpage);
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
                await ReplyAsync(
                    "This command is not found.\n" +
                    "Either the command name was typed incorrectly, or the command doesn't exist.\n" +
                    "If you think there should be a manual for this command, contact the Rover admins.");
                return;
            }

            await ReplyAsync(manpage);
        }
    }
}
