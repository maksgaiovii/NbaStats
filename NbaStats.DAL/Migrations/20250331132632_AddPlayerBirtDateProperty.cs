using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NbaStats.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerBirtDateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "season",
                columns: table => new
                {
                    seasonid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    startdate = table.Column<DateOnly>(type: "date", nullable: false),
                    enddate = table.Column<DateOnly>(type: "date", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("season_pkey", x => x.seasonid);
                });

            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    teamid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    conference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    division = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("team_pkey", x => x.teamid);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "match",
                columns: table => new
                {
                    matchid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    hometeamid = table.Column<int>(type: "integer", nullable: true),
                    awayteamid = table.Column<int>(type: "integer", nullable: true),
                    homescore = table.Column<int>(type: "integer", nullable: true),
                    awayscore = table.Column<int>(type: "integer", nullable: true),
                    seasonid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("match_pkey", x => x.matchid);
                    table.ForeignKey(
                        name: "match_awayteamid_fkey",
                        column: x => x.awayteamid,
                        principalTable: "team",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "match_hometeamid_fkey",
                        column: x => x.hometeamid,
                        principalTable: "team",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "match_seasonid_fkey",
                        column: x => x.seasonid,
                        principalTable: "season",
                        principalColumn: "seasonid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    playerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    position = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    teamid = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    weight = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_pkey", x => x.playerid);
                    table.ForeignKey(
                        name: "player_teamid_fkey",
                        column: x => x.teamid,
                        principalTable: "team",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "teamseasonaverages",
                columns: table => new
                {
                    teamseasonaveragesid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    teamid = table.Column<int>(type: "integer", nullable: true),
                    seasonid = table.Column<int>(type: "integer", nullable: true),
                    avgpoints = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgassists = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgrebounds = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgturnovers = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("teamseasonaverages_pkey", x => x.teamseasonaveragesid);
                    table.ForeignKey(
                        name: "teamseasonaverages_seasonid_fkey",
                        column: x => x.seasonid,
                        principalTable: "season",
                        principalColumn: "seasonid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "teamseasonaverages_teamid_fkey",
                        column: x => x.teamid,
                        principalTable: "team",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teamstats",
                columns: table => new
                {
                    teamstatsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    matchid = table.Column<int>(type: "integer", nullable: true),
                    teamid = table.Column<int>(type: "integer", nullable: true),
                    wins = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    losses = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    fgmade = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    fgattempted = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    freethrowsmade = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    freethrowsattempted = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    threepointersmade = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    threepointersattempted = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    points = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    assists = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    rebounds = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    steals = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("teamstats_pkey", x => x.teamstatsid);
                    table.ForeignKey(
                        name: "teamstats_matchid_fkey",
                        column: x => x.matchid,
                        principalTable: "match",
                        principalColumn: "matchid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "teamstats_teamid_fkey",
                        column: x => x.teamid,
                        principalTable: "team",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playerseasonaverages",
                columns: table => new
                {
                    playerseasonaveragesid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    playerid = table.Column<int>(type: "integer", nullable: true),
                    seasonid = table.Column<int>(type: "integer", nullable: true),
                    avgpoints = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgassists = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgsteals = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgrebounds = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgturnovers = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0"),
                    avgminutesplayed = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("playerseasonaverages_pkey", x => x.playerseasonaveragesid);
                    table.ForeignKey(
                        name: "playerseasonaverages_playerid_fkey",
                        column: x => x.playerid,
                        principalTable: "player",
                        principalColumn: "playerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "playerseasonaverages_seasonid_fkey",
                        column: x => x.seasonid,
                        principalTable: "season",
                        principalColumn: "seasonid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playerstats",
                columns: table => new
                {
                    playerstatsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    playerid = table.Column<int>(type: "integer", nullable: true),
                    matchid = table.Column<int>(type: "integer", nullable: true),
                    fgmade = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    fgattempted = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    freethrowsmade = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    freethrowsattempted = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    threepointersmade = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    threepointersattempted = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    points = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    assists = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    rebounds = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    steals = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    minutesplayed = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("playerstats_pkey", x => x.playerstatsid);
                    table.ForeignKey(
                        name: "playerstats_matchid_fkey",
                        column: x => x.matchid,
                        principalTable: "match",
                        principalColumn: "matchid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "playerstats_playerid_fkey",
                        column: x => x.playerid,
                        principalTable: "player",
                        principalColumn: "playerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userpreferences",
                columns: table => new
                {
                    userpreferencesid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: true),
                    teamid = table.Column<int>(type: "integer", nullable: true),
                    playerid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userpreferences_pkey", x => x.userpreferencesid);
                    table.ForeignKey(
                        name: "userpreferences_playerid_fkey",
                        column: x => x.playerid,
                        principalTable: "player",
                        principalColumn: "playerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userpreferences_teamid_fkey",
                        column: x => x.teamid,
                        principalTable: "team",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userpreferences_userid_fkey",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_match_awayteamid",
                table: "match",
                column: "awayteamid");

            migrationBuilder.CreateIndex(
                name: "IX_match_hometeamid",
                table: "match",
                column: "hometeamid");

            migrationBuilder.CreateIndex(
                name: "IX_match_seasonid",
                table: "match",
                column: "seasonid");

            migrationBuilder.CreateIndex(
                name: "IX_player_teamid",
                table: "player",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_playerseasonaverages_playerid",
                table: "playerseasonaverages",
                column: "playerid");

            migrationBuilder.CreateIndex(
                name: "IX_playerseasonaverages_seasonid",
                table: "playerseasonaverages",
                column: "seasonid");

            migrationBuilder.CreateIndex(
                name: "IX_playerstats_matchid",
                table: "playerstats",
                column: "matchid");

            migrationBuilder.CreateIndex(
                name: "IX_playerstats_playerid",
                table: "playerstats",
                column: "playerid");

            migrationBuilder.CreateIndex(
                name: "season_year_key",
                table: "season",
                column: "year",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teamseasonaverages_seasonid",
                table: "teamseasonaverages",
                column: "seasonid");

            migrationBuilder.CreateIndex(
                name: "IX_teamseasonaverages_teamid",
                table: "teamseasonaverages",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_teamstats_matchid",
                table: "teamstats",
                column: "matchid");

            migrationBuilder.CreateIndex(
                name: "IX_teamstats_teamid",
                table: "teamstats",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "user_email_key",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userpreferences_playerid",
                table: "userpreferences",
                column: "playerid");

            migrationBuilder.CreateIndex(
                name: "IX_userpreferences_teamid",
                table: "userpreferences",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_userpreferences_userid",
                table: "userpreferences",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playerseasonaverages");

            migrationBuilder.DropTable(
                name: "playerstats");

            migrationBuilder.DropTable(
                name: "teamseasonaverages");

            migrationBuilder.DropTable(
                name: "teamstats");

            migrationBuilder.DropTable(
                name: "userpreferences");

            migrationBuilder.DropTable(
                name: "match");

            migrationBuilder.DropTable(
                name: "player");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "season");

            migrationBuilder.DropTable(
                name: "team");
        }
    }
}
