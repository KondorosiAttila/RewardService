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

        public ScheduledActions(Player player)
		{
            this.player.PlayerId = player.PlayerId;
            this.player.LoginTime = player.LoginTime;
            this._timer = null;
            this.minutesPlayed = 0;
        }

        public Task StartAsync(CancellationToken token)
        {
            Console.WriteLine("Starting activity tracker");

            _timer = new Timer(TrackActiveTime, null, 0, 2);

            return Task.CompletedTask;
        }

        private void TrackActiveTime(object state)
        {
            if (minutesPlayed == 2)
            {
                Console.WriteLine($"Player {this.player.PlayerId} played {minutesPlayed} minutes");
            }
            minutesPlayed = Interlocked.Increment(ref minutesPlayed);
        }

        public Task StopAsync(CancellationToken token)
        {
            Console.WriteLine($"Player activity stopped");

            _timer?.Change(0, 2);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }


    }
}