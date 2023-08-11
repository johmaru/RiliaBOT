using System.Threading.Tasks;
using Discord.Commands;

namespace RiliaBOT.BotCore.Commands
{
    public class ping
    {
        public void Main()
        {
            var commandService = new CommandService();
            
            [Command("ping")]
            public async Task Ping()
            {
                await ReplyAsync("pong");
            }
        }
    }
}