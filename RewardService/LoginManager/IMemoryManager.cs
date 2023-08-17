using System;

namespace RewardService.LoginManager;

public interface IMemoryManager
{
    void SavePlayerInfo(string playerId, Player player);

    bool IsPlayerLoggedIn(string playerId);
}
