using Discord.Commands;

namespace Rover.Modules
{
    public class ModuleHelp : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Provides instructions for using the help command.")]
        public async Task Help()
        {
            await ReplyAsync(HelpMan());
        }

        [Command("help")]
        [Summary("Provides instructions for using a command.")]
        public async Task Help(string command)
        {
            switch(command)
            {
                // DevTools Commands
                case "echo":
                    await ReplyAsync(ModuleEcho.HelpMan());
                    break;
                case "ping":
                    await ReplyAsync(ModulePing.HelpMan());
                    break;

                // Information Commands
                case "help":
                    await ReplyAsync(HelpMan());
                    break;
                case "intro":
                    await ReplyAsync("TODO: intro HelpMan");
                    break;
                case "list":
                    await ReplyAsync("TODO: list HelpMan");
                    break;

                // Utilities Commands
                case "flipcoin":
                    await ReplyAsync("TODO: flipcoin HelpMan");
                    break;
                case "randomnumber":
                    await ReplyAsync("TODO: randomnumber HelpMan");
                    break;
                case "rolldice":
                    await ReplyAsync("TODO: rolldice HelpMan");
                    break;

                // Catchall
                default:
                    await ReplyAsync("That command could not be found.");
                    break;
            }
        }

        public static string HelpMan()
        {
            return @"```
            help: Displays instructions for using a specific command.

            Usage: !help [command]

            Example: !help list
            ```";
        }
    }
}