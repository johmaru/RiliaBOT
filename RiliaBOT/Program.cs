using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Tommy;

namespace RiliaBOT
{
    
    internal class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient _client;

        private async Task MainAsync()
        {
            AskExists_Toml();
            
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };
            
            _client = new DiscordSocketClient(config);
            var commandService = new CommandService();
            await InstallCommandAsync();
            _client.Log += Log;
            try
            {
                using(StreamReader reader = File.OpenText("Config.toml"))
                {
                    TomlTable table = TOML.Parse(reader);

                    string token = table["Token"];
                    
                    Console.WriteLine(token);
                    
                    await _client.LoginAsync(TokenType.Bot, token);
                    await _client.StartAsync();

                    await Task.Delay(-1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Err"+ e + "Starting failed for bot");
                throw;
            }
            async Task InstallCommandAsync()
            {
                _client.MessageReceived += HandleCommandAsync;

                await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            }
            
            async Task HandleCommandAsync(SocketMessage messageParam)
            {
                var message = messageParam as SocketUserMessage;

                if (message == null) return;

                if (message.Author.IsBot)
                {
                    return;
                }

                int argPos = 0;
                if (!(message.HasCharPrefix('!', ref argPos) ||
            
                      message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                
                    message.Author.IsBot)
                    
                    return;

                var context = new SocketCommandContext(_client, message);

                var result = await commandService.ExecuteAsync(context, argPos, null, MultiMatchHandling.Best);

                if (!result.IsSuccess)
            
                    await context.Channel.SendMessageAsync(result.ErrorReason);

            }
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        
      /* public class MyCommand: ModuleBase<SocketCommandContext>
        {
            [Command("ping")]
            public async Task Ping()
            {
                await ReplyAsync("pong");
            }
        }
        */
        private static void AskExists_Toml()
        {
            switch (File.Exists("./Config.toml"))
            {
                case false:
                    Console.WriteLine("Please input your Bot Token");
                    string input_token;
                    input_token = Console.ReadLine();
                    TomlTable toml = new TomlTable
                    {
                        ["Token"] = input_token,
                    };
                    using (StreamWriter writer = File.CreateText("Config.toml"))
                    {
                        toml.WriteTo(writer);
                        writer.Flush();
                    }
                    Console.WriteLine("Log : ConfigToml not Exists");
                    break;
                case true:
                    Console.WriteLine("Log : ConfigToml Exists");
                    break;
            }
        }
    }
}