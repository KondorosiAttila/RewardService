using Microsoft.AspNetCore.Mvc;
using System;
using RewardService.LoginManager;

namespace RewardService.Controllers;

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
    public string GetPlayerInfo(string playerId)
    {
        Player player = new Player()
        {
            PlayerId = playerId,
            LoginDate = DateTime.Now,
            IsLoggedIn = true
        };

        //SavePlayerInfo(player.PlayerId, player);

        return "Player with ID: " + player.PlayerId + " logged in at " + player.LoginDate;
    }
}
