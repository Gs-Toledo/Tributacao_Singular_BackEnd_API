using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tributacao_Singular.Infra.Migrations
{
    public partial class TrocadeMapeamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteProduto");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Produtos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ClienteId",
                table: "Produtos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Clientes_ClienteId",
                table: "Produtos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Clientes_ClienteId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ClienteId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Produtos");

            migrationBuilder.CreateTable(
                name: "ClienteProduto",
                columns: table => new
                {
                    ClientesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProduto", x => new { x.ClientesId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_ClienteProduto_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteProduto_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteProduto_ProdutosId",
                table: "ClienteProduto",
                column: "ProdutosId");
        }
    }
}
