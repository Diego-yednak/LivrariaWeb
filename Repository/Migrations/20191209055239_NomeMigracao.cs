using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class NomeMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDevolucao",
                table: "ItemEmprestimo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "alunoId",
                table: "ItemEmprestimo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "funcionarioId",
                table: "ItemEmprestimo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemEmprestimo_alunoId",
                table: "ItemEmprestimo",
                column: "alunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEmprestimo_funcionarioId",
                table: "ItemEmprestimo",
                column: "funcionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEmprestimo_Aluno_alunoId",
                table: "ItemEmprestimo",
                column: "alunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEmprestimo_Funcionario_funcionarioId",
                table: "ItemEmprestimo",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemEmprestimo_Aluno_alunoId",
                table: "ItemEmprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEmprestimo_Funcionario_funcionarioId",
                table: "ItemEmprestimo");

            migrationBuilder.DropIndex(
                name: "IX_ItemEmprestimo_alunoId",
                table: "ItemEmprestimo");

            migrationBuilder.DropIndex(
                name: "IX_ItemEmprestimo_funcionarioId",
                table: "ItemEmprestimo");

            migrationBuilder.DropColumn(
                name: "DataDevolucao",
                table: "ItemEmprestimo");

            migrationBuilder.DropColumn(
                name: "alunoId",
                table: "ItemEmprestimo");

            migrationBuilder.DropColumn(
                name: "funcionarioId",
                table: "ItemEmprestimo");
        }
    }
}
