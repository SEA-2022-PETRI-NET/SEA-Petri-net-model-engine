using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetriNetEngine.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetriNets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetriNets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arcs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceNode = table.Column<int>(type: "integer", nullable: false),
                    TargetNode = table.Column<int>(type: "integer", nullable: false),
                    PetriNetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arcs_PetriNets_PetriNetId",
                        column: x => x.PetriNetId,
                        principalTable: "PetriNets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaceId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NumberOfTokens = table.Column<int>(type: "integer", nullable: true),
                    PetriNetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_PetriNets_PetriNetId",
                        column: x => x.PetriNetId,
                        principalTable: "PetriNets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransitionId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PetriNetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transitions_PetriNets_PetriNetId",
                        column: x => x.PetriNetId,
                        principalTable: "PetriNets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arcs_PetriNetId",
                table: "Arcs",
                column: "PetriNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_PetriNetId",
                table: "Places",
                column: "PetriNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transitions_PetriNetId",
                table: "Transitions",
                column: "PetriNetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arcs");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Transitions");

            migrationBuilder.DropTable(
                name: "PetriNets");
        }
    }
}
