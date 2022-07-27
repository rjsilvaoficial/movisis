using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovisisCadastro.Migrations
{
    public partial class Tabelas_CLIENTE_e_CIDADE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CIDADE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(maxLength: 60, nullable: false),
                    UF = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIDADE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(maxLength: 60, nullable: false),
                    TELEFONE = table.Column<string>(maxLength: 13, nullable: false),
                    ID_CIDADE = table.Column<int>(nullable: false),
                    APELIDO = table.Column<string>(nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CLIENTE_CIDADE_ID_CIDADE",
                        column: x => x.ID_CIDADE,
                        principalTable: "CIDADE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_CIDADE",
                table: "CLIENTE",
                column: "ID_CIDADE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "CIDADE");
        }
    }
}
