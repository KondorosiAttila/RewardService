using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Tools;
using Pomelo.EntityFrameworkCore.MySql;
using RewardService.Models;

namespace RewardService.DataAccessManager
{
    interface IDatabaseMethods
    {
        void SaveLoginInfo(PlayersOnline playersOnline);

    }
}