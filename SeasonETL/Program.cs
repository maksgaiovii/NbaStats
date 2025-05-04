using System;
using System.Globalization;
using Npgsql;

class Program
{
    static void Main()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=nbaStats";

        var seasons = new[]
        {
            new { Year = 2020, StartDate = "2019-10-22", EndDate = "2020-10-11" },
            new { Year = 2021, StartDate = "2020-12-22", EndDate = "2021-07-20" },
            new { Year = 2022, StartDate = "2021-10-19", EndDate = "2022-06-16" },
            new { Year = 2023, StartDate = "2022-10-18", EndDate = "2023-06-12" },
            new { Year = 2024, StartDate = "2023-10-24", EndDate = "2024-06-17" }
        };

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        int inserted = 0;
        foreach (var s in seasons)
        {
            using var cmd = new NpgsqlCommand(@"
                INSERT INTO season (year, startdate, enddate)
                VALUES (@year, @startdate, @enddate)
                ON CONFLICT (year) DO NOTHING;", conn);

            cmd.Parameters.AddWithValue("year", s.Year);
            cmd.Parameters.AddWithValue("startdate", DateTime.ParseExact(s.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("enddate", DateTime.ParseExact(s.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));

            inserted += cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"\u2705 Seasons inserted: {inserted}");
    }
}