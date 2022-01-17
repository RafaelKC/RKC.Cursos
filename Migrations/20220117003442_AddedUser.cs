using System;
using Microsoft.EntityFrameworkCore.Migrations;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Migrations
{
    public partial class AddedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] {"Id", "FirstName", "LastName", "UserName", "Role", "IsInactive"},
                values: new object[]
                {
                    "35265aa9-ca64-4923-b191-5a0d8e1c5c28",
                    "System",
                    "Admin",
                    "System Admin",
                    UserRole.SystemAdmin,
                    false
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
