using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coling.API.Afiliados.Migrations
{
    /// <inheritdoc />
    public partial class migracion6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afiliado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    FechaAfiliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoAfiliado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroTituloProvicional = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Afiliado_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoSocial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSocial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonaTipoSocial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoSocial = table.Column<int>(type: "int", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaTipoSocial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonaTipoSocial_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaTipoSocial_TipoSocial_IdTipoSocial",
                        column: x => x.IdTipoSocial,
                        principalTable: "TipoSocial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afiliado_IdPersona",
                table: "Afiliado",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTipoSocial_IdPersona",
                table: "PersonaTipoSocial",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTipoSocial_IdTipoSocial",
                table: "PersonaTipoSocial",
                column: "IdTipoSocial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Afiliado");

            migrationBuilder.DropTable(
                name: "PersonaTipoSocial");

            migrationBuilder.DropTable(
                name: "TipoSocial");
        }
    }
}
