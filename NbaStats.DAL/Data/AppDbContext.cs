using System;
using System.Collections.Generic;
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

    public virtual DbSet<Playerseasonaverage> Playerseasonaverages { get; set; }

    public virtual DbSet<Playerstat> Playerstats { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Teamseasonaverage> Teamseasonaverages { get; set; }

    public virtual DbSet<Teamstat> Teamstats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userpreference> Userpreferences { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Matchid).HasName("match_pkey");

            entity.ToTable("match");

            entity.Property(e => e.Matchid).HasColumnName("matchid");
            entity.Property(e => e.Awayscore).HasColumnName("awayscore");
            entity.Property(e => e.Awayteamid).HasColumnName("awayteamid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Homescore).HasColumnName("homescore");
            entity.Property(e => e.Hometeamid).HasColumnName("hometeamid");
            entity.Property(e => e.Seasonid).HasColumnName("seasonid");

            entity.HasOne(d => d.Awayteam).WithMany(p => p.MatchAwayteams)
                .HasForeignKey(d => d.Awayteamid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("match_awayteamid_fkey");

            entity.HasOne(d => d.Hometeam).WithMany(p => p.MatchHometeams)
                .HasForeignKey(d => d.Hometeamid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("match_hometeamid_fkey");

            entity.HasOne(d => d.Season).WithMany(p => p.Matches)
                .HasForeignKey(d => d.Seasonid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("match_seasonid_fkey");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Playerid).HasName("player_pkey");

            entity.ToTable("player");

            entity.Property(e => e.Playerid).HasColumnName("playerid");
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
            entity.Property(e => e.Teamid).HasColumnName("teamid");
            entity.Property(e => e.Weight)
                .HasPrecision(5, 2)
                .HasColumnName("weight");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("player_teamid_fkey");
        });

        modelBuilder.Entity<Playerseasonaverage>(entity =>
        {
            entity.HasKey(e => e.Playerseasonaveragesid).HasName("playerseasonaverages_pkey");

            entity.ToTable("playerseasonaverages");

            entity.Property(e => e.Playerseasonaveragesid).HasColumnName("playerseasonaveragesid");
            entity.Property(e => e.Avgassists)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgassists");
            entity.Property(e => e.Avgminutesplayed)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgminutesplayed");
            entity.Property(e => e.Avgpoints)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgpoints");
            entity.Property(e => e.Avgrebounds)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgrebounds");
            entity.Property(e => e.Avgsteals)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgsteals");
            entity.Property(e => e.Avgturnovers)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgturnovers");
            entity.Property(e => e.Playerid).HasColumnName("playerid");
            entity.Property(e => e.Seasonid).HasColumnName("seasonid");

            entity.HasOne(d => d.Player).WithMany(p => p.Playerseasonaverages)
                .HasForeignKey(d => d.Playerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerseasonaverages_playerid_fkey");

            entity.HasOne(d => d.Season).WithMany(p => p.Playerseasonaverages)
                .HasForeignKey(d => d.Seasonid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerseasonaverages_seasonid_fkey");
        });

        modelBuilder.Entity<Playerstat>(entity =>
        {
            entity.HasKey(e => e.Playerstatsid).HasName("playerstats_pkey");

            entity.ToTable("playerstats");

            entity.Property(e => e.Playerstatsid).HasColumnName("playerstatsid");
            entity.Property(e => e.Assists)
                .HasDefaultValue(0)
                .HasColumnName("assists");
            entity.Property(e => e.Fgattempted)
                .HasDefaultValue(0)
                .HasColumnName("fgattempted");
            entity.Property(e => e.Fgmade)
                .HasDefaultValue(0)
                .HasColumnName("fgmade");
            entity.Property(e => e.Freethrowsattempted)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsattempted");
            entity.Property(e => e.Freethrowsmade)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsmade");
            entity.Property(e => e.Matchid).HasColumnName("matchid");
            entity.Property(e => e.Minutesplayed)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("minutesplayed");
            entity.Property(e => e.Playerid).HasColumnName("playerid");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Rebounds)
                .HasDefaultValue(0)
                .HasColumnName("rebounds");
            entity.Property(e => e.Steals)
                .HasDefaultValue(0)
                .HasColumnName("steals");
            entity.Property(e => e.Threepointersattempted)
                .HasDefaultValue(0)
                .HasColumnName("threepointersattempted");
            entity.Property(e => e.Threepointersmade)
                .HasDefaultValue(0)
                .HasColumnName("threepointersmade");

            entity.HasOne(d => d.Match).WithMany(p => p.Playerstats)
                .HasForeignKey(d => d.Matchid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerstats_matchid_fkey");

            entity.HasOne(d => d.Player).WithMany(p => p.Playerstats)
                .HasForeignKey(d => d.Playerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playerstats_playerid_fkey");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Seasonid).HasName("season_pkey");

            entity.ToTable("season");

            entity.HasIndex(e => e.Year, "season_year_key").IsUnique();

            entity.Property(e => e.Seasonid).HasColumnName("seasonid");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Teamid).HasName("team_pkey");

            entity.ToTable("team");

            entity.Property(e => e.Teamid).HasColumnName("teamid");
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

        modelBuilder.Entity<Teamseasonaverage>(entity =>
        {
            entity.HasKey(e => e.Teamseasonaveragesid).HasName("teamseasonaverages_pkey");

            entity.ToTable("teamseasonaverages");

            entity.Property(e => e.Teamseasonaveragesid).HasColumnName("teamseasonaveragesid");
            entity.Property(e => e.Avgassists)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgassists");
            entity.Property(e => e.Avgpoints)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgpoints");
            entity.Property(e => e.Avgrebounds)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgrebounds");
            entity.Property(e => e.Avgturnovers)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgturnovers");
            entity.Property(e => e.Seasonid).HasColumnName("seasonid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");

            entity.HasOne(d => d.Season).WithMany(p => p.Teamseasonaverages)
                .HasForeignKey(d => d.Seasonid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamseasonaverages_seasonid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Teamseasonaverages)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamseasonaverages_teamid_fkey");
        });

        modelBuilder.Entity<Teamstat>(entity =>
        {
            entity.HasKey(e => e.Teamstatsid).HasName("teamstats_pkey");

            entity.ToTable("teamstats");

            entity.Property(e => e.Teamstatsid).HasColumnName("teamstatsid");
            entity.Property(e => e.Assists)
                .HasDefaultValue(0)
                .HasColumnName("assists");
            entity.Property(e => e.Fgattempted)
                .HasDefaultValue(0)
                .HasColumnName("fgattempted");
            entity.Property(e => e.Fgmade)
                .HasDefaultValue(0)
                .HasColumnName("fgmade");
            entity.Property(e => e.Freethrowsattempted)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsattempted");
            entity.Property(e => e.Freethrowsmade)
                .HasDefaultValue(0)
                .HasColumnName("freethrowsmade");
            entity.Property(e => e.Losses)
                .HasDefaultValue(0)
                .HasColumnName("losses");
            entity.Property(e => e.Matchid).HasColumnName("matchid");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Rebounds)
                .HasDefaultValue(0)
                .HasColumnName("rebounds");
            entity.Property(e => e.Steals)
                .HasDefaultValue(0)
                .HasColumnName("steals");
            entity.Property(e => e.Teamid).HasColumnName("teamid");
            entity.Property(e => e.Threepointersattempted)
                .HasDefaultValue(0)
                .HasColumnName("threepointersattempted");
            entity.Property(e => e.Threepointersmade)
                .HasDefaultValue(0)
                .HasColumnName("threepointersmade");
            entity.Property(e => e.Wins)
                .HasDefaultValue(0)
                .HasColumnName("wins");

            entity.HasOne(d => d.Match).WithMany(p => p.Teamstats)
                .HasForeignKey(d => d.Matchid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamstats_matchid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Teamstats)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamstats_teamid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
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

        modelBuilder.Entity<Userpreference>(entity =>
        {
            entity.HasKey(e => e.Userpreferencesid).HasName("userpreferences_pkey");

            entity.ToTable("userpreferences");

            entity.Property(e => e.Userpreferencesid).HasColumnName("userpreferencesid");
            entity.Property(e => e.Playerid).HasColumnName("playerid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Player).WithMany(p => p.Userpreferences)
                .HasForeignKey(d => d.Playerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userpreferences_playerid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Userpreferences)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userpreferences_teamid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Userpreferences)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userpreferences_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
