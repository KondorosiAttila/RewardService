using Microsoft.Extensions.Hosting;
using RewardService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RewardService.Timers
{
	public class ScheduledActions : IHostedService, IDisposable
	{
		private Timer? _timer;
        private int minutesPlayed;
        public Player player = new Player();
        public CancellationToken token;

        public ScheduledActions(Player player)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            this.token = source.Token;
            this.player = player;       
            this._timer = null;
            this.minutesPlayed = 0;
        }

        public ScheduledActions()
        {}

        public Task StartAsync(CancellationToken token)
        {
            Console.WriteLine("Starting activity tracker");

            _timer = new Timer(TrackActiveTime, null, 0, 10000);

            return Task.CompletedTask;
        }

        private async void TrackActiveTime(object state)
        {
            if(this.minutesPlayed == 10) 
            {
                Console.WriteLine($"Player {this.player.PlayerId} played {this.minutesPlayed} seconds, and is allowed to claim reward");
                Dispose();
            }

            System.Console.WriteLine($"Player {this.player.PlayerId} played {this.minutesPlayed}");
            
            minutesPlayed = Interlocked.Increment(ref this.minutesPlayed);
        }

        public Task StopAsync(CancellationToken token)
        {
            this.player.LatestMinutesPlayed = this.minutesPlayed;
            
            Console.WriteLine($"Player activity stopped after {this.player.LatestMinutesPlayed} seconds");

            _timer?.Change(Timeout.Infinite, 0);
            Dispose();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }


    }
}