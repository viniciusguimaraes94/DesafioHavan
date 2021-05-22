using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class CriacaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Moeda",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(70)", nullable: false),
                    valor = table.Column<decimal>(type: "Decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moeda", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valorOriginal = table.Column<decimal>(type: "Decimal(15,2)", nullable: false),
                    valorConvertido = table.Column<decimal>(type: "Decimal(15,2)", nullable: false),
                    taxa = table.Column<decimal>(type: "Decimal(15,2)", nullable: false),
                    dataOperacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    moedaOrigemId = table.Column<int>(type: "int", nullable: false),
                    moedaDestinoId = table.Column<int>(type: "int", nullable: false),
                    clienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_Operacao_Cliente_clienteId",
                        column: x => x.clienteId,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Operacao_Moeda_moedaDestinoId",
                        column: x => x.moedaDestinoId,
                        principalTable: "Moeda",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Operacao_Moeda_moedaOrigemId",
                        column: x => x.moedaOrigemId,
                        principalTable: "Moeda",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_clienteId",
                table: "Operacao",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_moedaDestinoId",
                table: "Operacao",
                column: "moedaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_moedaOrigemId",
                table: "Operacao",
                column: "moedaOrigemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operacao");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Moeda");
        }
    }
}
