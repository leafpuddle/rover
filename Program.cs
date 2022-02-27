using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Rover.Modules;
using System.Reflection;

namespace Rover
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly InteractionService _interactions;

        private readonly string? _botToken;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,

                CaseSensitiveCommands = false,
            });

            _interactions = new InteractionService(_client, new InteractionServiceConfig
            {
                LogLevel = LogSeverity.Info
            });

            _client.Log += Log;
            _commands.Log += Log;
            _interactions.Log += Log;

            try { _botToken = Environment.GetEnvironmentVariable("rover_botToken"); }
            catch (ArgumentNullException e) when (_botToken == null)
            {
                Log(new LogMessage(
                    LogSeverity.Critical,
                    "Startup",
                    "No bot token found at environment variable 'rover_botToken'. The variable is either empty or missing.",
                    e)
                );
            }
        }

        private static Task Log(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private async Task MainAsync()
        {
            await InitCommands();

            await _client.LoginAsync(TokenType.Bot, _botToken);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private async Task InitCommands()
        {
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            await _interactions.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);

            _client.MessageReceived += HandleCommandAsync;
            _client.InteractionCreated += HandleInteractionAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

            int pos = 0;
            if (msg.HasCharPrefix('!', ref pos))
            {
                var context = new SocketCommandContext(_client, msg);

                var result = await _commands.ExecuteAsync(context, pos, services: null);
            }
        }

        private async Task HandleInteractionAsync(SocketInteraction arg)
        {
            if (arg == null) return;

            if (arg.User.Id == _client.CurrentUser.Id || arg.User.IsBot) return;

            var context = new SocketInteractionContext(_client, arg);

            var result = await _interactions.ExecuteCommandAsync(context, services: null);
        }
    }
}
