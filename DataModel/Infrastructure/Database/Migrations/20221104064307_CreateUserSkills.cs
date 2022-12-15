using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModel.Infrastructure.Database.Migrations
{
    public partial class CreateUserSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearExperience",
                table: "User",
                newName: "yearExperience");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "User",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "User",
                newName: "languages");

            migrationBuilder.RenameColumn(
                name: "Hobbies",
                table: "User",
                newName: "hobbies");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "User",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "User",
                newName: "birthday");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "User",
                newName: "IX_User_email");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "email",
                keyValue: null,
                column: "email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "User",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    domain = table.Column<int>(type: "int", nullable: false),
                    skillLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    skillId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => new { x.userId, x.skillId });
                    table.ForeignKey(
                        name: "FK_UserSkill_Skill_skillId",
                        column: x => x.skillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_User_createdById",
                        column: x => x.createdById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_createdById",
                table: "UserSkill",
                column: "createdById");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_skillId",
                table: "UserSkill",
                column: "skillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.RenameColumn(
                name: "yearExperience",
                table: "User",
                newName: "YearExperience");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "User",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "languages",
                table: "User",
                newName: "Languages");

            migrationBuilder.RenameColumn(
                name: "hobbies",
                table: "User",
                newName: "Hobbies");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "User",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "birthday",
                table: "User",
                newName: "Birthday");

            migrationBuilder.RenameIndex(
                name: "IX_User_email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
