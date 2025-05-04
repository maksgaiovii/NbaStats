
namespace TeamStatETL
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=nbaStats";
            string csvPath = @"C:\Users\Legion\Downloads\TeamStatistics.csv";
            int inserted = 0;

            using var conn = new Npgsql.NpgsqlConnection(connectionString);
            conn.Open();

            // Build team name to id dictionary
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

            using var fileReader = new StreamReader(csvPath);
            using var csv = new CsvHelper.CsvReader(fileReader, System.Globalization.CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                string teamName = csv.GetField("teamName");
                int? teamId = FindTeamId(teamName, teamNameToId);
                if (teamId == null)
                {
                    Console.WriteLine($"Team not found: {teamName}");
                    continue;
                }

                if (!int.TryParse(csv.GetField("gameId"), out int matchId) || !MatchIdExists(matchId, conn))
                    continue;
                float.TryParse(csv.GetField("assists"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float assistsFloat);
                int assists = (int)Math.Round(assistsFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("blocks"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float blocksFloat);
                int blocks = (int)Math.Round(blocksFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("steals"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float stealsFloat);
                int steals = (int)Math.Round(stealsFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("fieldGoalsAttempted"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float fgaFloat);
                int fga = (int)Math.Round(fgaFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("fieldGoalsMade"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float fgmFloat);
                int fgm = (int)Math.Round(fgmFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("fieldGoalsPercentage"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float fgPct);
                float.TryParse(csv.GetField("threePointersAttempted"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float tpaFloat);
                int tpa = (int)Math.Round(tpaFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("threePointersMade"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float tpmFloat);
                int tpm = (int)Math.Round(tpmFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("threePointersPercentage"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float tpPct);
                float.TryParse(csv.GetField("freeThrowsAttempted"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float ftaFloat);
                int fta = (int)Math.Round(ftaFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("freeThrowsMade"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float ftmFloat);
                int ftm = (int)Math.Round(ftmFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("freeThrowsPercentage"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float ftPct);
                float.TryParse(csv.GetField("reboundsDefensive"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float rebDefFloat);
                int rebDef = (int)Math.Round(rebDefFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("reboundsOffensive"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float rebOffFloat);
                int rebOff = (int)Math.Round(rebOffFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("reboundsTotal"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float rebTotFloat);
                int rebTot = (int)Math.Round(rebTotFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("foulsPersonal"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float foulsFloat);
                int fouls = (int)Math.Round(foulsFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("turnovers"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float turnoversFloat);
                int turnovers = (int)Math.Round(turnoversFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("teamScore"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float teamScoreFloat);
                int teamScore = (int)Math.Round(teamScoreFloat, MidpointRounding.AwayFromZero);
                float.TryParse(csv.GetField("opponentScore"), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float opponentScoreFloat);
                int opponentScore = (int)Math.Round(opponentScoreFloat, MidpointRounding.AwayFromZero);

                using var cmd = new Npgsql.NpgsqlCommand(@"
                INSERT INTO teamstats
                (matchid, teamid, fgmade, fgattempted, freethrowsmade, freethrowsattempted, threepointersmade, threepointersattempted, points, assists, rebounds, steals, blocks, turnovers)
                VALUES 
                (@matchid, @teamid, @fgm, @fga, @ftm, @fta, @tpm, @tpa, @teamscore, @assists, @rebounds, @steals, @blocks, @turnovers)
                ON CONFLICT DO NOTHING;", conn);

                cmd.Parameters.AddWithValue("teamid", teamId.Value);
                cmd.Parameters.AddWithValue("matchid", matchId);
                cmd.Parameters.AddWithValue("assists", assists);
                cmd.Parameters.AddWithValue("blocks", blocks);
                cmd.Parameters.AddWithValue("steals", steals);
                cmd.Parameters.AddWithValue("fgm", fgm);
                cmd.Parameters.AddWithValue("fga", fga);
                cmd.Parameters.AddWithValue("fgpct", fgPct);
                cmd.Parameters.AddWithValue("tpm", tpm);
                cmd.Parameters.AddWithValue("tpa", tpa);
                cmd.Parameters.AddWithValue("tppct", tpPct);
                cmd.Parameters.AddWithValue("ftm", ftm);
                cmd.Parameters.AddWithValue("fta", fta);
                cmd.Parameters.AddWithValue("ftpct", ftPct);
                cmd.Parameters.AddWithValue("rebdef", rebDef);
                cmd.Parameters.AddWithValue("reboff", rebOff);
                cmd.Parameters.AddWithValue("rebounds", rebTot);
                cmd.Parameters.AddWithValue("fouls", fouls);
                cmd.Parameters.AddWithValue("turnovers", turnovers);
                cmd.Parameters.AddWithValue("teamscore", teamScore);
                cmd.Parameters.AddWithValue("opponentscore", opponentScore);

                inserted += cmd.ExecuteNonQuery();
            }

            Console.WriteLine($"\u2705 Team stats inserted: {inserted}");
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
        static bool MatchIdExists(int matchId, Npgsql.NpgsqlConnection conn)
        {
            using var cmd = new Npgsql.NpgsqlCommand("SELECT 1 FROM match WHERE matchid = @matchid LIMIT 1", conn);
            cmd.Parameters.AddWithValue("matchid", matchId);
            using var reader = cmd.ExecuteReader();
            return reader.Read();
        }
    }
}