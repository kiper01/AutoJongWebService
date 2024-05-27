using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoJongWebService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    StartYear = table.Column<long>(type: "bigint", nullable: false),
                    StartPrice = table.Column<long>(type: "bigint", nullable: false),
                    EndPrice = table.Column<long>(type: "bigint", nullable: false),
                    Fuel = table.Column<int>(type: "integer", nullable: false),
                    StartEngineVolume = table.Column<double>(type: "double precision", nullable: false),
                    Gearbox = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
