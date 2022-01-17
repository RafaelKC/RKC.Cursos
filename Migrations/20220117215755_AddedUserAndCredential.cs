using System;
using Microsoft.EntityFrameworkCore.Migrations;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Migrations
{
    public partial class AddedUserAndCredential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
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
                    IsInactive = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
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
                    "admin123"
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
                    UserRole.SystemAdmin,
                    false,
                    "system@admin.com"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
