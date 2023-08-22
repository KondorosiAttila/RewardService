using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using RewardService.Models;
using MySqlConnector;


namespace RewardService.DataAccessManager
{
    public class DbMethods
    {
        private readonly string connectionstring = "Server=localhost;Database=rewardservice;User=root;Password=;";

        public DbMethods()
        {}

        public async Task<Player> RunQuery(string query, Player player)
        {
            System.Console.WriteLine(query);

            System.Console.WriteLine("Trying to establish connection to the DB server");
            var connection = new MySqlConnection(connectionstring);
            await connection.OpenAsync();

            var command = new MySqlCommand(query, connection);
            await command.ExecuteNonQueryAsync();
            System.Console.WriteLine($"Query {query} was successful!");

            await connection.CloseAsync();
            System.Console.WriteLine("Closing connection...");

            return player;
        }

        public async Task<Player> SavePlayerLogin(Player player)
        {
            string query = $"INSERT INTO playersonline (PlayerId, LoginDate) VALUES (\"{player.PlayerId}\", \"{player.LoginTime}\");";

            await RunQuery(query, player);

            return player;
        }

        public async Task<Player> DeletePlayerLoggedInEntry(Player player)
        {
            string query = $"DELETE FROM playersonline WHERE PlayerId=\"{player.PlayerId}\"";

            await RunQuery(query, player);

            return player;
        }
    }
}
