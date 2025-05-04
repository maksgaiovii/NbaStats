using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Npgsql;
using CsvHelper;

class Program
{
    static void Main()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=nbaStats";
        string csvPath = @"C:\Users\Legion\Downloads\Games.csv";

        // Map partial team names to IDs
        var teamNameToId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Hawks", 1610612737 }, { "Celtics", 1610612738 }, { "Cavaliers", 1610612739 },
            { "Pelicans", 1610612740 }, { "Bulls", 1610612741 }, { "Mavericks", 1610612742 },
            { "Nuggets", 1610612743 }, { "Warriors", 1610612744 }, { "Rockets", 1610612745 },
            { "Clippers", 1610612746 }, { "Lakers", 1610612747 }, { "Heat", 1610612748 },
            { "Bucks", 1610612749 }, { "Timberwolves", 1610612750 }, { "Nets", 1610612751 },
            { "Knicks", 1610612752 }, { "Magic", 1610612753 }, { "Pacers", 1610612754 },
            { "76ers", 1610612755 }, { "Suns", 1610612756 }, { "Trail Blazers", 1610612757 },
            { "Kings", 1610612758 }, { "Spurs", 1610612759 }, { "Thunder", 1610612760 },
            { "Raptors", 1610612761 }, { "Jazz", 1610612762 }, { "Grizzlies", 1610612763 },
            { "Wizards", 1610612764 }, { "Pistons", 1610612765 }, { "Hornets", 1610612766 }
        };

        int inserted = 0;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            int gameId = int.Parse(csv.GetField("gameId"));
            string gameDateStr = csv.GetField("gameDate");
            string homeTeamName = csv.GetField("hometeamName");
            string awayTeamName = csv.GetField("awayteamName");
            string homeScoreStr = csv.GetField("homeScore");
            string awayScoreStr = csv.GetField("awayScore");

            // Parse date
            // Extract only the date part before any space (e.g., "5/3/2025" from "5/3/2025  7:30:00 PM")
            var dateOnlyStr = gameDateStr.Trim().Split(' ')[0];
            string[] formats = { "MM/dd/yyyy", "M/d/yyyy", "yyyy-MM-dd" };
            if (!DateTime.TryParseExact(dateOnlyStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var gameDate))
                continue;
            if (gameDate.Year < 2020)
                continue;

            // Fuzzy match team names
            int? homeTeamId = FindTeamId(homeTeamName, teamNameToId);
            int? awayTeamId = FindTeamId(awayTeamName, teamNameToId);
            if (homeTeamId == null || awayTeamId == null)
                continue;

            int.TryParse(homeScoreStr, out int homeScore);
            int.TryParse(awayScoreStr, out int awayScore);
            int seasonId = 6 - (2025 - gameDate.Year); // Assuming the season ID is fixed for this example

            using var cmd = new NpgsqlCommand(@"
                INSERT INTO match (matchid, date, hometeamid, awayteamid, homescore, awayscore, seasonid)
                VALUES (@gameid, @gamedate, @hometeamid, @awayteamid, @homescore, @awayscore, @seasonid)
                ON CONFLICT (matchid) DO NOTHING;", conn);

            cmd.Parameters.AddWithValue("gameid", gameId);
            cmd.Parameters.AddWithValue("gamedate", gameDate);
            cmd.Parameters.AddWithValue("hometeamid", homeTeamId.Value);
            cmd.Parameters.AddWithValue("awayteamid", awayTeamId.Value);
            cmd.Parameters.AddWithValue("homescore", homeScore);
            cmd.Parameters.AddWithValue("awayscore", awayScore);
            cmd.Parameters.AddWithValue("seasonid", seasonId);

            inserted += cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"\u2705 Matches inserted: {inserted}");
    }

    static int? FindTeamId(string teamName, Dictionary<string, int> teamNameToId)
    {
        if (string.IsNullOrWhiteSpace(teamName))
            return null;
        foreach (var kvp in teamNameToId)
        {
            if (teamName.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                return kvp.Value;
        }

        return null;
    }
}