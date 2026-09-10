namespace DungeonLeaderboardAPI
{
    public class LeaderboardEntry
    {
        public string PlayerName { get; set; }

        public int Score { get; set; }

        public int Level { get; set; }

        public int Gold { get; set; }

        public int MonstersDefeated { get; set; }
    }
}