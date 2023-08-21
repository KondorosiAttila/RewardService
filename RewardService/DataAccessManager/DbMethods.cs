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

        public async Task<Player> SavePlayerLogin(Player player)
        {
            string query = $"INSERT INTO playersonline (PlayerId, LoginDate) VALUES (\"{player.PlayerId}\", \"{player.LoginTime}\");";
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
    }
}
