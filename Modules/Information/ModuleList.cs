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
                listpage = System.IO.File.ReadAllText(@".\Lists\list.txt");
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                await ReplyAsync("Contact the Rover Admin. The list page is not found, which could mean there are issues with me.");
                return;
            }

            await ReplyAsync(listpage);
        }

        [Command("list")]
        [Summary("Provides a list of commands within a certain category")]
        public async Task List(string category)
        {
            string listpage;

            try
            {
                listpage = System.IO.File.ReadAllText(@$".\Lists\{category}.txt");
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                await ReplyAsync("This category is not found.");
                return;
            }

            await ReplyAsync(listpage);
        }
    }
}