using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheRush.WebApp.Domain;
using TheRush.WebApp.Infrastructure.Database;

namespace TheRush.WebApp.Infrastructure
{
    public class SeedTheRushDatabase
    {
        private readonly TheRushDatabaseContext _dbContext;

        public SeedTheRushDatabase(TheRushDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateAndSeedFromFile()
        {
            _dbContext.Database.Migrate();

            // Lazily decide if we've seeded already
            if (_dbContext.PlayerRushingStats.Any()) return;
            
            var rushingData = JsonConvert.DeserializeObject<JArray>(File.ReadAllText("rushing.json"));

            _dbContext.PlayerRushingStats.AddRange(rushingData.Select(x => new PlayerRushingStats
            {
                Player = x.Value<string>("Player"),
                Team = x.Value<string>("Team"),
                Pos = x.Value<string>("Pos"),
                Att = x.Value<int>("Att"),
                AttG = x.Value<double>("Att/G"),
                Yds = Convert.ToInt32(Regex.Replace(x.Value<string>("Yds"), @"[^\w\s\-]*", string.Empty)),
                Avg = x.Value<double>("Avg"),
                YdsG = x.Value<double>("Yds/G"),
                Td = x.Value<int>("TD"),
                Lng = Convert.ToInt32(Regex.Replace(x.Value<string>("Lng"), @"[^\-0-9]", string.Empty)),
                First = x.Value<int>("1st"),
                FirstPercentage = x.Value<double>("1st%"),
                TwentyPlus = x.Value<int>("20+"),
                FortyPlus = x.Value<int>("40+"),
                Fum = x.Value<int>("FUM"),
            }));
            _dbContext.SaveChanges();
        }
    }
}