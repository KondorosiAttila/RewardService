using System;

namespace RewardService.LoginManager;

public class LoginManager : IMemoryManager
{
	public LoginManager()
	{
	}

    public void SavePlayerInfo(string playerId, Player player)
    {
        var memoryCache = MemoryCache.Default;
        if (memoryCache.Contains(playerId))
        {
            System.Console.WriteLine("There is a player with ID: " + playerId + ", no need to save player info again");
            return;
        }
        else
        {
            memoryCache.Add(playerId, player, DateTimeOffset.Now.AddHours(12.0));
            System.Console.WriteLine("Adding player with ID: " + player.PlayerId + " and login time " + player.LoginDate);
        }
    }

    public bool IsPlayerLoggedIn(string playerId)
	{
		return false;
	}
}
