using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RKC.Cursos.Migrations
{
    public partial class AddedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuloId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CargaHoraria = table.Column<int>(type: "integer", nullable: false),
                    DataAcontecer = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
            
            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] {"Id", "UserId", "Email", "Password"},
                values: new object[]
                {
                    "8619d8fd-60e6-4682-8a98-2ce500531888",
                    "35265aa9-ca64-4923-b191-5a0d8e1c5c28",
                    "system@admin.com",
                    "7FCF4BA391C48784EDDE599889D6E3F1E47A27DB36ECC050CC92F259BFAC38AFAD2C68A1AE804D77075E8FB722503F3ECA2B2C1006EE6F6C7B7628CB45FFFD1D"
                });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] {"Id", "FirstName", "LastName", "UserName", "Role", "IsInactive", "Email"},
                values: new object[]
                {
                    "35265aa9-ca64-4923-b191-5a0d8e1c5c28",
                    "System",
                    "Admin",
                    "System Admin",
                    0,
                    false,
                    "system@admin.com"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
