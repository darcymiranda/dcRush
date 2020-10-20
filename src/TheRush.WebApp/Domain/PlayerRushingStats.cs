using System;

namespace TheRush.WebApp.Domain
{
    public class PlayerRushingStats
    {
        public Guid Id { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public string Pos { get; set; }
        public int Att { get; set; }
        public double AttG { get; set; }
        public int Yds { get; set; }
        public double Avg { get; set; }
        public double YdsG { get; set; }
        public int Td { get; set; }
        public int Lng { get; set; }
        public int First { get; set; }
        public double FirstPercentage { get; set; }
        public int TwentyPlus { get; set; }
        public int FortyPlus { get; set; }
        public int Fum { get; set; }
    }
}