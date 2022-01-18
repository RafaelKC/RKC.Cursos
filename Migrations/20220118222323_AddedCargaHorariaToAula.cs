using Microsoft.EntityFrameworkCore.Migrations;

namespace RKC.Cursos.Migrations
{
    public partial class AddedCargaHorariaToAula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdModulo",
                table: "Aulas",
                newName: "ModuloId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Aulas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CargaHoraria",
                table: "Aulas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "ModuloId",
                table: "Aulas",
                newName: "IdModulo");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Aulas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
