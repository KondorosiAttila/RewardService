using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Tools;
using Pomelo.EntityFrameworkCore.MySql;
using RewardService.Models;
using MySqlConnector;
using MySql.Data;

namespace RewardService.DataAccessManager
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{
		}

		DbSet<PlayersOnline> PlayerLoginInfo { get; set; }
	}

	public class DatabaseMethods : IDatabaseMethods
	{
		private readonly string connectionString = "Server=localhost;Database=rewardservice;User=root;Password=;";


       /* public string GetConnectionString()
		{
			ConnectionStringSettingsCollection connectionString = ConfigurationManager.ConnectionStrings;
			return connectionString.ConnectionString;
		}*/

		public async void SaveLoginInfo(PlayersOnline playersOnline)
		{
			var query= $"INSERT INTO playersonline {playersOnline.PlayerId} {playersOnline.LoginTime};";

			MySqlConnection connection	= new MySqlConnection(connectionString);
			MySqlCommand command		= new MySqlCommand(query, connection);

			command.CommandType			= CommandType.Text;

			var result					= await command.ExecuteNonQuery();
            await Console.Out.WriteLineAsync(result);

            /*var connectionString = GetConnectionString(); 

			using var connection = new MySqlConnection(connectionString);
			await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);
			using var execute = await command.ExecuteAsync();

            await Console.Out.WriteLineAsync(execute); */
        }
	}
}