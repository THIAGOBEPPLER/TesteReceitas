using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteReceitas.Migrations
{
    public partial class teste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Receitas_ReceitaId",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_ReceitaId",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "ReceitaId",
                table: "Ingredientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceitaId",
                table: "Ingredientes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_ReceitaId",
                table: "Ingredientes",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Receitas_ReceitaId",
                table: "Ingredientes",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
