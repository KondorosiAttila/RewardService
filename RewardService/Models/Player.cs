using System;

namespace RewardService.Models
{

    public class Player
    {
        public string PlayerId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public TimeSpan TimeSpentInGame { get; set; }
        public bool IsLoggedIn { get; set; }

        public Player()
        { }
    }
}