using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AliensStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class DealersAndPlanet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DealerId",
                table: "Alien",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dealer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alien_DealerId",
                table: "Alien",
                column: "DealerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alien_Dealer_DealerId",
                table: "Alien",
                column: "DealerId",
                principalTable: "Dealer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alien_Dealer_DealerId",
                table: "Alien");

            migrationBuilder.DropTable(
                name: "Dealer");

            migrationBuilder.DropIndex(
                name: "IX_Alien_DealerId",
                table: "Alien");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "Alien");
        }
    }
}
