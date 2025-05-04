using System;
using System.Collections.Generic;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=nbaStats";
        int inserted = 0;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand(@"
            SELECT teamstats.teamid, season.seasonid, 
                COUNT(*) AS games,
                AVG(teamstats.points) AS avg_teamscore,
                AVG(teamstats.assists) AS avg_assists,
                AVG(teamstats.rebounds) AS avg_rebounds,
                AVG(teamstats.steals) AS avg_steals,
                AVG(teamstats.blocks) AS avg_blocks,
                AVG(teamstats.turnovers) AS avg_turnovers
            FROM teamstats
            JOIN match ON teamstats.matchid = match.matchid
            JOIN season ON match.seasonid = season.seasonid
            GROUP BY teamstats.teamid, season.seasonid
        ", conn);

        using var reader = cmd.ExecuteReader();
        var averages = new List<(int teamId, int seasonId, int games, float avgTeamScore, float avgAssists, float avgRebounds, float avgSteals, float avgBlocks, float avgTurnovers)>();

        while (reader.Read())
        {
            averages.Add((
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetInt32(2),
                reader.GetFloat(3),
                reader.GetFloat(4),
                reader.GetFloat(5),
                reader.GetFloat(6),
                reader.GetFloat(7),
                reader.GetFloat(8)
            ));
        }
        reader.Close();

        // Insert or update team season averages
        foreach (var avg in averages)
        {
            using var upsertCmd = new NpgsqlCommand(@"
                INSERT INTO teamseasonaverages
                (teamid, seasonid, avgpoints, avgassists, avgrebounds, avgsteals, avgblocks, avgturnovers)
                VALUES
                (@teamid, @seasonid, @avg_teamscore, @avg_assists, @avg_rebounds, @avg_steals, @avg_blocks, @avg_turnovers)
                ON CONFLICT (teamid, seasonid) DO UPDATE SET
                    avgpoints = EXCLUDED.avgpoints,
                    avgassists = EXCLUDED.avgassists,
                    avgrebounds = EXCLUDED.avgrebounds,
                    avgsteals = EXCLUDED.avgsteals,
                    avgblocks = EXCLUDED.avgblocks,
                    avgturnovers = EXCLUDED.avgturnovers
            ", conn);

            upsertCmd.Parameters.AddWithValue("teamid", avg.teamId);
            upsertCmd.Parameters.AddWithValue("seasonid", avg.seasonId);
            upsertCmd.Parameters.AddWithValue("avg_teamscore", avg.avgTeamScore);
            upsertCmd.Parameters.AddWithValue("avg_assists", avg.avgAssists);
            upsertCmd.Parameters.AddWithValue("avg_rebounds", avg.avgRebounds);
            upsertCmd.Parameters.AddWithValue("avg_steals", avg.avgSteals);
            upsertCmd.Parameters.AddWithValue("avg_blocks", avg.avgBlocks);
            upsertCmd.Parameters.AddWithValue("avg_turnovers", avg.avgTurnovers);

            inserted += upsertCmd.ExecuteNonQuery();
        }

        Console.WriteLine($"\u2705 Team season averages upserted: {inserted}");
    }
}