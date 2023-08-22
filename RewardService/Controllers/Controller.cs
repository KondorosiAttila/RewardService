using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RewardService.Models;
using RewardService.DataAccessManager;
using RewardService.Timers;

namespace RewardService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {

        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        DbMethods dbCommands = new DbMethods();

        [HttpPost("/login/{id}")]
        public async Task<Player> PlayerLogin(string id)
        {
            Player player = new Player()
            {
                PlayerId = id,
                LoginTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture)
            };

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            System.Console.WriteLine($"Player with ID {player.PlayerId} logged in at {player.LoginTime}");

            System.Console.WriteLine($"Sending {player.PlayerId} info to DataAccessManager");
            await dbCommands.SavePlayerLogin(player);

            ScheduledActions trigger = new ScheduledActions(player);
            
            await trigger.StartAsync(token);
            await trigger.StopAsync(token);

            return player;
        }

        [HttpDelete("/logout/{id}")]
        public async Task<Player> PlayerLogout(string id)
        {
            Player player = new Player()
            {
                PlayerId = id,
                LogoutTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture)
            };

            System.Console.WriteLine($"Player with ID {player.PlayerId} logged out at {player.LogoutTime}");

            await dbCommands.DeletePlayerLoggedInEntry(player);

            return player;
        }
    }
}
