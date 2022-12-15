using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModel.Infrastructure.Database.Migrations
{
    public partial class UpdateUserSkills_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "skillLevel",
                table: "Skill");

            migrationBuilder.AddColumn<int>(
                name: "skillLevel",
                table: "UserSkill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "skillLevel",
                table: "UserSkill");

            migrationBuilder.AddColumn<int>(
                name: "skillLevel",
                table: "Skill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
