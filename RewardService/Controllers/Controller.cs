﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RewardService.Models;
using RewardService.DataAccessManager;

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
            
            System.Console.WriteLine($"Player with ID {player.PlayerId} logged in at {player.LoginTime}");

            System.Console.WriteLine($"Sending {player.PlayerId} info to DataAccessManager");
            await dbCommands.SavePlayerLogin(player);

            return player;
        }

        
    }
}