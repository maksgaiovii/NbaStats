using Microsoft.EntityFrameworkCore;

namespace NbaStats.DAL.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerSeasonAverage> Playerseasonaverages { get; set; }

    public virtual DbSet<PlayerStat> Playerstats { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamSeasonAverage> Teamseasonaverages { get; set; }

    public virtual DbSet<TeamStat> Teamstats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPreference> Userpreferences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=nbaStats;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("match_pkey");

            entity.ToTable("match");

            entity.Property(e => e.MatchId).HasColumnName("matchid");
            entity.Property(e => e.AwayScore).HasColumnName("awayscore");
            entity.Property(e => e.AwayTeamId).HasColumnName("awayteamid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.HomeScore).HasColumnName("homescore");
            entity.Property(e => e.HomeTeamId).HasColumnName("hometeamid");
            entity.Property(e => e.SeasonId).HasColumnName("seasonid");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("match_awayteamid_fkey");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("match_hometeamid_fkey");

            entity.HasOne(d => d.Season).WithMany(p => p.Matches)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("match_seasonid_fkey");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("player_pkey");

            entity.ToTable("player");

            entity.Property(e => e.PlayerId).HasColumnName("playerid");
            entity.Property(e => e.Height)
                .HasPrecision(5, 2)
                .HasColumnName("height");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .HasColumnName("position");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.TeamId).HasColumnName("teamid");
            entity.Property(e => e.Weight)
                .HasPrecision(5, 2)
                .HasColumnName("weight");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("player_teamid_fkey");
        });

        modelBuilder.Entity<PlayerSeasonAverage>(entity =>
        {
            entity.HasKey(e => e.PlayerSeasonAveragesId).HasName("playerseasonaverages_pkey");

            entity.ToTable("playerseasonaverages");

            entity.Property(e => e.PlayerSeasonAveragesId).HasColumnName("playerseasonaveragesid");
            entity.Property(e => e.AvgAssists)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgassists");
            entity.Property(e => e.AvgMinutesPlayed)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgminutesplayed");
            entity.Property(e => e.AvgPoints)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgpoints");
            entity.Property(e => e.AvgRebounds)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgrebounds");
            entity.Property(e => e.AvgSteals)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgsteals");
            entity.Property(e => e.AvgTurnovers)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgturnovers");
            entity.Property(e => e.PlayerId).HasColumnName("playerid");
            entity.Property(e => e.SeasonId).HasColumnName("seasonid");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerSeasonAverages)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerseasonaverages_playerid_fkey");

            entity.HasOne(d => d.Season).WithMany(p => p.PlayerSeasonAverages)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerseasonaverages_seasonid_fkey");
        });

        modelBuilder.Entity<PlayerStat>(entity =>
        {
            entity.HasKey(e => e.PlayerStatsId).HasName("playerstats_pkey");

            entity.ToTable("playerstats");

            entity.Property(e => e.PlayerStatsId).HasColumnName("playerstatsid");
            entity.Property(e => e.Assists)
                .HasDefaultValue(0)
                .HasColumnName("assists");
            entity.Property(e => e.FgAttempted)
                .HasDefaultValue(0)
                .HasColumnName("fgattempted");
            entity.Property(e => e.FgMade)
                .HasDefaultValue(0)
                .HasColumnName("fgmade");
            entity.Property(e => e.FreeThrowsAttempted)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsattempted");
            entity.Property(e => e.FreeThrowsMade)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsmade");
            entity.Property(e => e.MatchId).HasColumnName("matchid");
            entity.Property(e => e.MinutesPlayed)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("minutesplayed");
            entity.Property(e => e.PlayerId).HasColumnName("playerid");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Rebounds)
                .HasDefaultValue(0)
                .HasColumnName("rebounds");
            entity.Property(e => e.Steals)
                .HasDefaultValue(0)
                .HasColumnName("steals");
            entity.Property(e => e.ThreePointersAttempted)
                .HasDefaultValue(0)
                .HasColumnName("threepointersattempted");
            entity.Property(e => e.ThreePointersMade)
                .HasDefaultValue(0)
                .HasColumnName("threepointersmade");

            entity.HasOne(d => d.Match).WithMany(p => p.PlayerStats)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerstats_matchid_fkey");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerStats)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerstats_playerid_fkey");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.SeasonId).HasName("season_pkey");

            entity.ToTable("season");

            entity.HasIndex(e => e.Year, "season_year_key").IsUnique();

            entity.Property(e => e.SeasonId).HasColumnName("seasonid");
            entity.Property(e => e.EndDate).HasColumnName("enddate");
            entity.Property(e => e.StartDate).HasColumnName("startdate");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("team_pkey");

            entity.ToTable("team");

            entity.Property(e => e.TeamId).HasColumnName("teamid");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Conference)
                .HasMaxLength(50)
                .HasColumnName("conference");
            entity.Property(e => e.Division)
                .HasMaxLength(50)
                .HasColumnName("division");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TeamSeasonAverage>(entity =>
        {
            entity.HasKey(e => e.TeamSeasonAveragesId).HasName("teamseasonaverages_pkey");

            entity.ToTable("teamseasonaverages");

            entity.Property(e => e.TeamSeasonAveragesId).HasColumnName("teamseasonaveragesid");
            entity.Property(e => e.AvgAssists)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgassists");
            entity.Property(e => e.AvgPoints)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgpoints");
            entity.Property(e => e.AvgRebounds)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgrebounds");
            entity.Property(e => e.AvgTurnovers)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgturnovers");
            entity.Property(e => e.SeasonId).HasColumnName("seasonid");
            entity.Property(e => e.TeamId).HasColumnName("teamid");

            entity.HasOne(d => d.Season).WithMany(p => p.TeamSeasonAverages)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamseasonaverages_seasonid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamSeasonAverages)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamseasonaverages_teamid_fkey");
        });

        modelBuilder.Entity<TeamStat>(entity =>
        {
            entity.HasKey(e => e.TeamStatsId).HasName("teamstats_pkey");

            entity.ToTable("teamstats");

            entity.Property(e => e.TeamStatsId).HasColumnName("teamstatsid");
            entity.Property(e => e.Assists)
                .HasDefaultValue(0)
                .HasColumnName("assists");
            entity.Property(e => e.FgAttempted)
                .HasDefaultValue(0)
                .HasColumnName("fgattempted");
            entity.Property(e => e.FgMade)
                .HasDefaultValue(0)
                .HasColumnName("fgmade");
            entity.Property(e => e.FreeThrowsAttempted)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsattempted");
            entity.Property(e => e.FreeThrowsMade)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsmade");
            entity.Property(e => e.Losses)
                .HasDefaultValue(0)
                .HasColumnName("losses");
            entity.Property(e => e.MatchId).HasColumnName("matchid");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Rebounds)
                .HasDefaultValue(0)
                .HasColumnName("rebounds");
            entity.Property(e => e.Steals)
                .HasDefaultValue(0)
                .HasColumnName("steals");
            entity.Property(e => e.TeamId).HasColumnName("teamid");
            entity.Property(e => e.ThreePointersAttempted)
                .HasDefaultValue(0)
                .HasColumnName("threepointersattempted");
            entity.Property(e => e.ThreePointersMade)
                .HasDefaultValue(0)
                .HasColumnName("threepointersmade");
            entity.Property(e => e.Wins)
                .HasDefaultValue(0)
                .HasColumnName("wins");

            entity.HasOne(d => d.Match).WithMany(p => p.TeamStats)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamstats_matchid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamStats)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamstats_teamid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasKey(e => e.UserPreferencesId).HasName("userpreferences_pkey");

            entity.ToTable("userpreferences");

            entity.Property(e => e.UserPreferencesId).HasColumnName("userpreferencesid");
            entity.Property(e => e.PlayerId).HasColumnName("playerid");
            entity.Property(e => e.TeamId).HasColumnName("teamid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Player).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userpreferences_playerid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userpreferences_teamid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userpreferences_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
