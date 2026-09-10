using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DungeonLeaderboardAPI
{
    public class LeaderboardStore
    {
        private readonly string filePath = "leaderboard.json";

        public List<LeaderboardEntry> GetLeaderboard()
        {
            if (!File.Exists(filePath))
            {
                return new List<LeaderboardEntry>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<LeaderboardEntry>();
            }

            List<LeaderboardEntry> scores =
                JsonSerializer.Deserialize<List<LeaderboardEntry>>(json);

            if (scores == null)
            {
                return new List<LeaderboardEntry>();
            }

            return scores
                .OrderByDescending(x => x.Score)
                .Take(100)
                .ToList();
        }

        public void AddScore(LeaderboardEntry entry)
        {
            List<LeaderboardEntry> scores =
                GetLeaderboard();

            scores.Add(entry);

            scores = scores
                .OrderByDescending(x => x.Score)
                .Take(100)
                .ToList();

            string json =
                JsonSerializer.Serialize(
                    scores,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(filePath, json);
        }
    }
}