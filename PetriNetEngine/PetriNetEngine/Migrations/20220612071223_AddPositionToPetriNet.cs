using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetriNetEngine.Migrations
{
    public partial class AddPositionToPetriNet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "position_x",
                table: "Transitions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "position_y",
                table: "Transitions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "position_x",
                table: "Places",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "position_y",
                table: "Places",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "position_x",
                table: "Transitions");

            migrationBuilder.DropColumn(
                name: "position_y",
                table: "Transitions");

            migrationBuilder.DropColumn(
                name: "position_x",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "position_y",
                table: "Places");
        }
    }
}
