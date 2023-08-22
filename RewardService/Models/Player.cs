using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RewardService.Models
{
    public class Player
    {
        public string PlayerId { get; set; }

        public string? LoginTime { get; set; }

        public string? LogoutTime { get; set; }

        public Player()
        { }
    }
}
