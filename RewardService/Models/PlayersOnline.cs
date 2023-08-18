using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Tools;
using Pomelo.EntityFrameworkCore.MySql;
using System.ComponentModel.DataAnnotations;

namespace RewardService.Models
{
    public class PlayersOnline
    {
        [Key]
        public string PlayerId { get; set; }
        
        public DateTime LoginTime { get; set; }

        public PlayersOnline(string PlayerId)
        {
            this.PlayerId = PlayerId;
            this.LoginTime = DateTime.Now;
        }
    }
}