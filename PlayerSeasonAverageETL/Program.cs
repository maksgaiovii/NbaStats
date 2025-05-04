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
                        SELECT playerstats.playerid, season.seasonid, 
                            COUNT(*) AS games,
                            AVG(playerstats.points) AS avg_points,
                            AVG(playerstats.assists) AS avg_assists,
                            AVG(playerstats.rebounds) AS avg_rebounds,
                            AVG(playerstats.steals) AS avg_steals,
                            AVG(playerstats.blocks) AS avg_blocks,
                            AVG(playerstats.turnovers) AS avg_turnovers,
                            AVG(playerstats.minutesplayed) AS avg_minutesplayed
                        FROM playerstats
                        JOIN match ON playerstats.matchid = match.matchid
                        JOIN season ON match.seasonid = season.seasonid
                        GROUP BY playerstats.playerid, season.seasonid
                    ", conn);
            
                    using var reader = cmd.ExecuteReader();
                    var averages = new List<(int playerId, int seasonId, int games, float avgPoints, float avgAssists, float avgRebounds, float avgSteals, float avgBlocks, float avgTurnovers, float avgMinutesPlayed)>();
            
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
                            reader.GetFloat(8),
                            reader.GetFloat(9)
                        ));
                    }
                    reader.Close();
            
                    // Insert or update player season averages
                    foreach (var avg in averages)
                    {
                        using var upsertCmd = new NpgsqlCommand(@"
                            INSERT INTO playerseasonaverages
                            (playerid, seasonid, avgpoints, avgassists, avgrebounds, avgsteals, avgblocks, avgturnovers, avgminutesplayed)
                            VALUES
                            (@playerid, @seasonid, @avg_points, @avg_assists, @avg_rebounds, @avg_steals, @avg_blocks, @avg_turnovers, @avg_minutesplayed)
                            ON CONFLICT (playerid, seasonid) DO UPDATE SET
                                avgpoints = EXCLUDED.avgpoints,
                                avgassists = EXCLUDED.avgassists,
                                avgrebounds = EXCLUDED.avgrebounds,
                                avgsteals = EXCLUDED.avgsteals,
                                avgblocks = EXCLUDED.avgblocks,
                                avgturnovers = EXCLUDED.avgturnovers,
                                avgminutesplayed = EXCLUDED.avgminutesplayed;
                        ", conn);
            
                        upsertCmd.Parameters.AddWithValue("playerid", avg.playerId);
                        upsertCmd.Parameters.AddWithValue("seasonid", avg.seasonId);
                        upsertCmd.Parameters.AddWithValue("avg_points", avg.avgPoints);
                        upsertCmd.Parameters.AddWithValue("avg_assists", avg.avgAssists);
                        upsertCmd.Parameters.AddWithValue("avg_rebounds", avg.avgRebounds);
                        upsertCmd.Parameters.AddWithValue("avg_steals", avg.avgSteals);
                        upsertCmd.Parameters.AddWithValue("avg_blocks", avg.avgBlocks);
                        upsertCmd.Parameters.AddWithValue("avg_turnovers", avg.avgTurnovers);
                        upsertCmd.Parameters.AddWithValue("avg_minutesplayed", avg.avgMinutesPlayed);
            
                        inserted += upsertCmd.ExecuteNonQuery();
                    }
            
                    Console.WriteLine($"\u2705 Player season averages upserted: {inserted}");
                }
            }