using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tributacao_Singular.Infra.Migrations
{
    public partial class MudancaMapeamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produtos");
        }
    }
}
