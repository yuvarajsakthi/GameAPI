using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GameCompanies",
                columns: table => new
                {
                    GameCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoundedYear = table.Column<int>(type: "int", nullable: false),
                    HeadQuarter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCompanies", x => x.GameCompanyId);
                    table.ForeignKey(
                        name: "FK_GameCompanies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformId);
                    table.ForeignKey(
                        name: "FK_Platforms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                    table.ForeignKey(
                        name: "FK_Publishers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameCompanyId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_GameCompanies_GameCompanyId",
                        column: x => x.GameCompanyId,
                        principalTable: "GameCompanies",
                        principalColumn: "GameCompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameDetails",
                columns: table => new
                {
                    GameDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDetails", x => x.GameDetailId);
                    table.ForeignKey(
                        name: "FK_GameDetails_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    PlatformsPlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GamesGameId, x.PlatformsPlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Platforms_PlatformsPlatformId",
                        column: x => x.PlatformsPlatformId,
                        principalTable: "Platforms",
                        principalColumn: "PlatformId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@gameapi.com", "admin123", "Admin", "admin" },
                    { 2, "company@gameapi.com", "company123", "Company", "companyuser" },
                    { 3, "viewer@gameapi.com", "viewer123", "Viewer", "vieweruser" }
                });

            migrationBuilder.InsertData(
                table: "GameCompanies",
                columns: new[] { "GameCompanyId", "FoundedYear", "HeadQuarter", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, 1984, "California, USA", "Naughty Dog", 2 },
                    { 2, 1994, "Warsaw, Poland", "CD Projekt Red", 2 },
                    { 3, 1998, "New York, USA", "Rockstar Games", 2 },
                    { 4, 1986, "Montreuil, France", "Ubisoft", 2 },
                    { 5, 2007, "Washington, USA", "343 Industries", 2 }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "PlatformId", "Name", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "PlayStation 5", "Console", 1 },
                    { 2, "Xbox Series X", "Console", 1 },
                    { 3, "PC", "Desktop", 1 },
                    { 4, "Nintendo Switch", "Console", 1 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "Country", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Japan", "Sony Interactive Entertainment", 2 },
                    { 2, "USA", "Warner Bros. Games", 2 },
                    { 3, "USA", "Microsoft Studios", 2 },
                    { 4, "France", "Ubisoft Entertainment", 2 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "GameCompanyId", "PublisherId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, "The Last of Us Part II", 2 },
                    { 2, 2, 2, "Cyberpunk 2077", 2 },
                    { 3, 3, 2, "GTA V", 2 },
                    { 4, 4, 4, "Assassin’s Creed Valhalla", 2 },
                    { 5, 5, 3, "Halo Infinite", 2 },
                    { 6, 1, 1, "Uncharted 4", 2 },
                    { 7, 2, 2, "The Witcher 3: Wild Hunt", 2 },
                    { 8, 4, 4, "Far Cry 6", 2 }
                });

            migrationBuilder.InsertData(
                table: "GameDetails",
                columns: new[] { "GameDetailId", "Description", "GameId", "Genre", "ReleaseDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Survival story set in a post-apocalyptic world.", 1, "Action-Adventure", new DateTime(2020, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, "Futuristic open-world RPG set in Night City.", 2, "RPG", new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Open-world action crime game.", 3, "Action-Adventure", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, "Viking-era open-world RPG.", 4, "Action-RPG", new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, "Master Chief returns to battle the Banished.", 5, "FPS", new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, "Nathan Drake’s final treasure-hunting adventure.", 6, "Adventure", new DateTime(2016, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, "Monster-hunting in a rich fantasy open world.", 7, "RPG", new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, "Cuba-inspired open-world revolution story.", 8, "Shooter", new DateTime(2021, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCompanies_UserId",
                table: "GameCompanies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetails_GameId",
                table: "GameDetails",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameDetails_UserId",
                table: "GameDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformsPlatformId",
                table: "GamePlatforms",
                column: "PlatformsPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameCompanyId",
                table: "Games",
                column: "GameCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_UserId",
                table: "Platforms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_UserId",
                table: "Publishers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDetails");

            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "GameCompanies");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
