using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetriNetEngine.Migrations
{
    public partial class TokensList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Tokens",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "maxAge",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxTokenId",
                table: "PetriNets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "PetriNets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Arcs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "maxAge",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MaxTokenId",
                table: "PetriNets");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "PetriNets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Arcs");
        }
    }
}
