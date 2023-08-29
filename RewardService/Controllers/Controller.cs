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
        ScheduledActions trigger;

        [HttpPost("/login/{id}")]
        public async Task<Player> PlayerLogin(string id)
        {
            Player LoginPlayer = new Player()
            {
                PlayerId = id,
                LoginTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture)
            };

            Player.PlayersOnline.Add(LoginPlayer);

            System.Console.WriteLine($"Player with ID {LoginPlayer.PlayerId} logged in at {LoginPlayer.LoginTime}");

            System.Console.WriteLine($"Sending {LoginPlayer.PlayerId} info to DataAccessManager");
            await dbCommands.SavePlayerLogin(LoginPlayer);

            trigger = new ScheduledActions(LoginPlayer);
            await trigger.StartAsync(trigger.token);

            return LoginPlayer;
        }

        [HttpDelete("/logout/{id}")]
        public async Task<Player> PlayerLogout(string id)
        {
            //Branch example comment
            //Player LogoutPlayer = Player.PlayersOnline.FirstOrDefault(Player => Player.PlayerId.Equals(id));
            //LogoutPlayer.LogoutTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture);

            Player LogoutPlayer = new Player()
            {
                PlayerId = id,
                LogoutTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture)
            };

            System.Console.WriteLine($"Player with ID {LogoutPlayer.PlayerId} logged out at {LogoutPlayer.LogoutTime}");

            await dbCommands.DeletePlayerLoggedInEntry(LogoutPlayer);

            //await trigger.StopAsync(trigger.token);

            return null;
        }
    }
}
