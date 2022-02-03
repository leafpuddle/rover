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
                manpage = System.IO.File.ReadAllText(@"\Manuals\help.txt");
            }
            catch (FileNotFoundException)
            {
                await ReplyAsync("Contact the Rover Admin. The Help manual is not found, which could mean there are issues with me.");
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
                manpage = System.IO.File.ReadAllText(@$"\Manuals\{command}.txt");
            }
            catch (FileNotFoundException)
            {
                await ReplyAsync("This command is not found.");
                return;
            }

            await ReplyAsync(manpage);
        }
    }
}