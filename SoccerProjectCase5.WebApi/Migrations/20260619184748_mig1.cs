using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerProjectCase5.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    FixtureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    StadiumName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    HalfTimeHomeScore = table.Column<int>(type: "int", nullable: false),
                    HalfTimeAwayScore = table.Column<int>(type: "int", nullable: false),
                    FullTimeHomeScore = table.Column<int>(type: "int", nullable: false),
                    FullTimeAwayScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.FixtureId);
                    table.ForeignKey(
                        name: "FK_Fixtures_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchEvents",
                columns: table => new
                {
                    MatchEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvents", x => x.MatchEventId);
                    table.ForeignKey(
                        name: "FK_MatchEvents_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "FixtureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchStatistics",
                columns: table => new
                {
                    MatchStatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureId = table.Column<int>(type: "int", nullable: false),
                    PossessionHome = table.Column<int>(type: "int", nullable: false),
                    PossessionAway = table.Column<int>(type: "int", nullable: false),
                    ShotsHome = table.Column<int>(type: "int", nullable: false),
                    ShotsAway = table.Column<int>(type: "int", nullable: false),
                    ShotsOnTargetHome = table.Column<int>(type: "int", nullable: false),
                    ShotsOnTargetAway = table.Column<int>(type: "int", nullable: false),
                    PassesHome = table.Column<int>(type: "int", nullable: false),
                    PassesAway = table.Column<int>(type: "int", nullable: false),
                    PassAccuracyHome = table.Column<int>(type: "int", nullable: false),
                    PassAccuracyAway = table.Column<int>(type: "int", nullable: false),
                    CornersHome = table.Column<int>(type: "int", nullable: false),
                    CornersAway = table.Column<int>(type: "int", nullable: false),
                    FoulsHome = table.Column<int>(type: "int", nullable: false),
                    FoulsAway = table.Column<int>(type: "int", nullable: false),
                    OffsidesHome = table.Column<int>(type: "int", nullable: false),
                    OffsidesAway = table.Column<int>(type: "int", nullable: false),
                    YellowCardsHome = table.Column<int>(type: "int", nullable: false),
                    YellowCardsAway = table.Column<int>(type: "int", nullable: false),
                    RedCardsHome = table.Column<int>(type: "int", nullable: false),
                    RedCardsAway = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStatistics", x => x.MatchStatisticId);
                    table.ForeignKey(
                        name: "FK_MatchStatistics_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "FixtureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_AwayTeamId",
                table: "Fixtures",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_HomeTeamId",
                table: "Fixtures",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvents_FixtureId",
                table: "MatchEvents",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_FixtureId",
                table: "MatchStatistics",
                column: "FixtureId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchEvents");

            migrationBuilder.DropTable(
                name: "MatchStatistics");

            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
