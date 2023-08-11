using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace RiliaBOT.BotCore.Commands
{
    public class PingPong : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Ping!")
                .WithDescription($"PONG back to you {Context.User.Mention} (This will be a never ending game of Ping/Pong)")
                .WithColor(Color.DarkRed);

            await ReplyAsync("", false, builder.Build());
        }
    }
}