using Microsoft.AspNetCore.Mvc;
using System;
using RewardService.Models;
using System.Threading.Tasks;
using RewardService.DataAccessManager;

namespace RewardService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/getplayerinfo/{playerId}")]
        public async Task<PlayersOnline> GetPlayerInfo(string playerId)
        {
            PlayersOnline player = new PlayersOnline(playerId);
           
            await SaveLoginInfo(player);

            return player;
        }
    }
}