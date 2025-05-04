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
        string csvPath = @"C:\Users\Legion\Downloads\nba2k-full.csv";

        var teamAbbreviationToId = new Dictionary<string, int>
        {
            { "Atlanta Hawks", 1610612737 }, { "Boston Celtics", 1610612738 }, { "Cleveland Cavaliers", 1610612739 },
            { "New Orleans Pelicans", 1610612740 },
            { "Chicago Bulls", 1610612741 }, { "Dallas Mavericks", 1610612742 }, { "Denver Nuggets", 1610612743 },
            { "Golden State Warriors", 1610612744 },
            { "Houston Rockets", 1610612745 }, { "Los Angeles Clippers", 1610612746 },
            { "Los Angeles Lakers", 1610612747 }, { "Miami Heat", 1610612748 },
            { "Milwaukee Bucks", 1610612749 }, { "Minnesota Timberwolves", 1610612750 },
            { "Brooklyn Nets", 1610612751 }, { "New York Knicks", 1610612752 },
            { "Orlando Magic", 1610612753 }, { "Indiana Pacers", 1610612754 }, { "Philadelphia 76ers", 1610612755 },
            { "Phoenix Suns", 1610612756 },
            { "Portland Trail Blazers", 1610612757 }, { "Sacramento Kings", 1610612758 },
            { "San Antonio Spurs", 1610612759 }, { "Oklahoma City Thunder", 1610612760 },
            { "Toronto Raptors", 1610612761 }, { "Utah Jazz", 1610612762 }, { "Memphis Grizzlies", 1610612763 },
            { "Washington Wizards", 1610612764 },
            { "Detroit Pistons", 1610612765 }, { "Charlotte Hornets", 1610612766 }
        };

        int updated = 0;

        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<dynamic>();

        foreach (var r in records)
        {
            string fullName = r.full_name;
            string teamName = r.team;
            string position = r.position;
           int draftYear = 0;
           if (r.draft_year != null)
               int.TryParse(r.draft_year, out draftYear);
           string birthDateStr = draftYear != 0
               ? $"{draftYear - 19}-01-01"
               : r.b_day;
            int height = r.height?.ToString().Split('/').Length > 1
                ? int.Parse(r.height.ToString().Split('/')[1].Trim().Replace(".", ""))
                : int.Parse(r.height.ToString().Replace(".", ""));
            int weight = 0;
            if (r.weight != null)
            {
                var weightStr = r.weight.ToString();
                if (weightStr.Contains("/"))
                    weightStr = weightStr.Split('/')[1].Trim();
                // Extract the integer part before the decimal and before "kg"
                var kgPart = weightStr.Split(' ')[0];
                if (kgPart.Contains('.'))
                    kgPart = kgPart.Split('.')[0];
                int.TryParse(kgPart, out weight);
            }
            

            int? teamId = null;
            if (string.IsNullOrWhiteSpace(teamName) || !teamAbbreviationToId.TryGetValue(teamName, out int id) || id == 0)
            {
                continue;
            }
            teamId = id;

            DateTime? birthDate = null;
            if (DateTime.TryParseExact(birthDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out var parsedDate))
            {
                birthDate = parsedDate;
            }

            string surname = fullName?.Contains(" ") == true ? fullName.Split('^', ' ', '\'')[^1] : fullName;

            using var insertCmd = new NpgsqlCommand(@"
                INSERT INTO player (name, surname, position, teamid, height, weight, ""BirthDate"")
                VALUES (@name, @surname, @position, @teamid, @height, @weight, @birthDate)", conn);

            insertCmd.Parameters.AddWithValue("name", (object?)fullName ?? DBNull.Value);
            insertCmd.Parameters.AddWithValue("surname", (object?)surname ?? DBNull.Value);
            insertCmd.Parameters.AddWithValue("position", (object?)position ?? DBNull.Value);
            insertCmd.Parameters.AddWithValue("teamid", (object?)teamId ?? DBNull.Value);
            insertCmd.Parameters.AddWithValue("height", height == 0 ? DBNull.Value : (object)height);
            insertCmd.Parameters.AddWithValue("weight", weight == 0 ? DBNull.Value : (object)weight);
            insertCmd.Parameters.AddWithValue("birthDate", (object?)birthDate ?? DBNull.Value);

            insertCmd.ExecuteNonQuery();

            updated++;
        }

        Console.WriteLine($"✅ Гравців оновлено або вставлено: {updated}");
    }
}