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
        string csvPath = @"C:\Users\Legion\Downloads\PlayerStatistics.csv";

        int inserted = 0;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            int playerId = GetPlayerIdByNameSurname(csv.GetField("firstName"), csv.GetField("lastName"), conn);
            if (playerId == 0)
            {
                Console.WriteLine($"Player not found: {csv.GetField("firstName")} {csv.GetField("lastName")}");
                continue;
            }
            
         float.TryParse(csv.GetField("gameId"), NumberStyles.Float, CultureInfo.InvariantCulture, out float matchIdFloat);
          int matchId = (int)Math.Round(matchIdFloat, MidpointRounding.AwayFromZero);
          if (!MatchIdExists(matchId, conn))
              continue;
          float.TryParse(csv.GetField("numMinutes"), NumberStyles.Float, CultureInfo.InvariantCulture, out float minutesFloat);
          int minutes = (int)Math.Round(minutesFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("points"), NumberStyles.Float, CultureInfo.InvariantCulture, out float pointsFloat);
          int points = (int)Math.Round(pointsFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("assists"), NumberStyles.Float, CultureInfo.InvariantCulture, out float assistsFloat);
          int assists = (int)Math.Round(assistsFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("blocks"), NumberStyles.Float, CultureInfo.InvariantCulture, out float blocksFloat);
          int blocks = (int)Math.Round(blocksFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("steals"), NumberStyles.Float, CultureInfo.InvariantCulture, out float stealsFloat);
          int steals = (int)Math.Round(stealsFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("fieldGoalsAttempted"), NumberStyles.Float, CultureInfo.InvariantCulture, out float fgaFloat);
          int fga = (int)Math.Round(fgaFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("fieldGoalsMade"), NumberStyles.Float, CultureInfo.InvariantCulture, out float fgmFloat);
          int fgm = (int)Math.Round(fgmFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("fieldGoalsPercentage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float fgPct);
          float.TryParse(csv.GetField("threePointersAttempted"), NumberStyles.Float, CultureInfo.InvariantCulture, out float tpaFloat);
          int tpa = (int)Math.Round(tpaFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("threePointersMade"), NumberStyles.Float, CultureInfo.InvariantCulture, out float tpmFloat);
          int tpm = (int)Math.Round(tpmFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("threePointersPercentage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float tpPct);
          float.TryParse(csv.GetField("freeThrowsAttempted"), NumberStyles.Float, CultureInfo.InvariantCulture, out float ftaFloat);
          int fta = (int)Math.Round(ftaFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("freeThrowsMade"), NumberStyles.Float, CultureInfo.InvariantCulture, out float ftmFloat);
          int ftm = (int)Math.Round(ftmFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("freeThrowsPercentage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float ftPct);
          float.TryParse(csv.GetField("reboundsDefensive"), NumberStyles.Float, CultureInfo.InvariantCulture, out float rebDefFloat);
          int rebDef = (int)Math.Round(rebDefFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("reboundsOffensive"), NumberStyles.Float, CultureInfo.InvariantCulture, out float rebOffFloat);
          int rebOff = (int)Math.Round(rebOffFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("reboundsTotal"), NumberStyles.Float, CultureInfo.InvariantCulture, out float rebTotFloat);
          int rebTot = (int)Math.Round(rebTotFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("foulsPersonal"), NumberStyles.Float, CultureInfo.InvariantCulture, out float foulsFloat);
          int fouls = (int)Math.Round(foulsFloat, MidpointRounding.AwayFromZero);
          float.TryParse(csv.GetField("turnovers"), NumberStyles.Float, CultureInfo.InvariantCulture, out float turnoversFloat);
          int turnovers = (int)Math.Round(turnoversFloat, MidpointRounding.AwayFromZero);

            using var cmd = new NpgsqlCommand(@"
    INSERT INTO playerstats 
    (playerid, matchid, fgmade, fgattempted, freethrowsmade, freethrowsattempted, threepointersmade, threepointersattempted, points, assists, rebounds, steals, minutesplayed, blocks, turnovers)
    VALUES 
    (@playerid, @matchid, @fgm, @fgattempted, @freethrowsmade, @freethrowsattempted, @threepointersmade, @threepointersattempted, @points, @assists, @rebounds, @steals, @minutesplayed, @blocks, @turnovers)
    ON CONFLICT DO NOTHING;", conn);

            cmd.Parameters.AddWithValue("playerid", playerId);
            cmd.Parameters.AddWithValue("matchid", matchId);
            cmd.Parameters.AddWithValue("minutesplayed", minutes);
            cmd.Parameters.AddWithValue("points", points);
            cmd.Parameters.AddWithValue("assists", assists);
            cmd.Parameters.AddWithValue("blocks", blocks);
            cmd.Parameters.AddWithValue("steals", steals);
            cmd.Parameters.AddWithValue("fgattempted", fga);
            cmd.Parameters.AddWithValue("fgm", fgm);
            cmd.Parameters.AddWithValue("fgpct", fgPct);
            cmd.Parameters.AddWithValue("threepointersattempted", tpa);
            cmd.Parameters.AddWithValue("threepointersmade", tpm);
            cmd.Parameters.AddWithValue("tppct", tpPct);
            cmd.Parameters.AddWithValue("freethrowsattempted", fta);
            cmd.Parameters.AddWithValue("freethrowsmade", ftm);
            cmd.Parameters.AddWithValue("ftpct", ftPct);
            cmd.Parameters.AddWithValue("rebdef", rebDef);
            cmd.Parameters.AddWithValue("reboff", rebOff);
            cmd.Parameters.AddWithValue("rebounds", rebTot);
            cmd.Parameters.AddWithValue("fouls", fouls);
            cmd.Parameters.AddWithValue("turnovers", turnovers);

            inserted += cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"\u2705 Player stats inserted: {inserted}");
    }
    static int GetPlayerIdByNameSurname(string name, string surname, NpgsqlConnection conn)
    {
        string fullName = name + " " + surname;
        using var cmd = new NpgsqlCommand("SELECT playerid FROM player WHERE name = @name LIMIT 1", conn);
        cmd.Parameters.AddWithValue("name", fullName ?? (object)DBNull.Value);
        var result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : 0;
    }
    static bool MatchIdExists(int matchId, NpgsqlConnection conn)
    {
        using var cmd = new NpgsqlCommand("SELECT 1 FROM match WHERE matchid = @matchid LIMIT 1", conn);
        cmd.Parameters.AddWithValue("matchid", matchId);
        using var reader = cmd.ExecuteReader();
        return reader.Read();
    }
}
