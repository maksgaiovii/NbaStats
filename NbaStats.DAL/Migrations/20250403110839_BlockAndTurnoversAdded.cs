using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NbaStats.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BlockAndTurnoversAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Blocks",
                table: "teamstats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Turnovers",
                table: "teamstats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgBlocks",
                table: "teamseasonaverages",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgSteals",
                table: "teamseasonaverages",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Blocks",
                table: "playerstats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Turnovers",
                table: "playerstats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgBlocks",
                table: "playerseasonaverages",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocks",
                table: "teamstats");

            migrationBuilder.DropColumn(
                name: "Turnovers",
                table: "teamstats");

            migrationBuilder.DropColumn(
                name: "AvgBlocks",
                table: "teamseasonaverages");

            migrationBuilder.DropColumn(
                name: "AvgSteals",
                table: "teamseasonaverages");

            migrationBuilder.DropColumn(
                name: "Blocks",
                table: "playerstats");

            migrationBuilder.DropColumn(
                name: "Turnovers",
                table: "playerstats");

            migrationBuilder.DropColumn(
                name: "AvgBlocks",
                table: "playerseasonaverages");
        }
    }
}
