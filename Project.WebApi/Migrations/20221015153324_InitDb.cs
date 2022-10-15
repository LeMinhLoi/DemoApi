using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.WebApi.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Long = table.Column<double>(type: "float", nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalkDifficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkDifficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalkDifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walks_WalkDifficulties_WalkDifficultyId",
                        column: x => x.WalkDifficultyId,
                        principalTable: "WalkDifficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Area", "Code", "Lat", "Long", "Name", "Population" },
                values: new object[,]
                {
                    { new Guid("a381075c-8c50-4151-826f-1593b0a5cfad"), 100.0, "ABC", 2.0, 100.0, "name1", 1000L },
                    { new Guid("a381075c-8c50-4151-826f-1593b0a5cfae"), 100.0, "XYZ", 2.0, 100.0, "name2", 1000L },
                    { new Guid("a381075c-8c50-4151-826f-1593b0a5cfaf"), 100.0, "123", 2.0, 100.0, "name3", 1000L }
                });

            migrationBuilder.InsertData(
                table: "WalkDifficulties",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { new Guid("d381075c-8c50-4151-826f-1593b0a5cfad"), "Medium" },
                    { new Guid("d381075c-8c50-4151-826f-1593b0a5cfae"), "Hard" },
                    { new Guid("d381075c-8c50-4151-826f-1593b0a5cfaf"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Length", "Name", "RegionId", "WalkDifficultyId" },
                values: new object[] { new Guid("d381075c-8c50-4151-826f-1593b0a5cfad"), 10.0, "Marathon", new Guid("a381075c-8c50-4151-826f-1593b0a5cfad"), new Guid("d381075c-8c50-4151-826f-1593b0a5cfad") });

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Length", "Name", "RegionId", "WalkDifficultyId" },
                values: new object[] { new Guid("d381075c-8c50-4151-826f-1593b0a5cfae"), 10.0, "hellothon", new Guid("a381075c-8c50-4151-826f-1593b0a5cfae"), new Guid("d381075c-8c50-4151-826f-1593b0a5cfae") });

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Length", "Name", "RegionId", "WalkDifficultyId" },
                values: new object[] { new Guid("d381075c-8c50-4151-826f-1593b0a5cfaf"), 10.0, "hithon", new Guid("a381075c-8c50-4151-826f-1593b0a5cfaf"), new Guid("d381075c-8c50-4151-826f-1593b0a5cfaf") });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "WalkDifficulties");
        }
    }
}
