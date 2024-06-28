using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ESOF.WebApp.DBLayer.Migrations
{
    public partial class EmailTemplate : Migration
    {
        protected  void right(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Profiles_ProfileId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Profiles_ProfileId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSkill_Profiles_ProfileId",
                table: "ProfileSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSkill_Skill_SkillId",
                table: "ProfileSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileSkill",
                table: "ProfileSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experience",
                table: "Experience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "ProfileSkill",
                newName: "ProfileSkills");

            migrationBuilder.RenameTable(
                name: "Experience",
                newName: "Experiences");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileSkill_SkillId",
                table: "ProfileSkills",
                newName: "IX_ProfileSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_Experience_ProfileId",
                table: "Experiences",
                newName: "IX_Experiences_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ProfileId",
                table: "Educations",
                newName: "IX_Educations_ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "UrlProfile",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlBackground",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileSkills",
                table: "ProfileSkills",
                columns: new[] { "ProfileId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experiences",
                table: "Experiences",
                column: "ExperienceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "EducationId");

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Profiles_ProfileId",
                table: "Educations",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Profiles_ProfileId",
                table: "Experiences",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSkills_Profiles_ProfileId",
                table: "ProfileSkills",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSkills_Skills_SkillId",
                table: "ProfileSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }

        protected void left(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Profiles_ProfileId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Profiles_ProfileId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSkills_Profiles_ProfileId",
                table: "ProfileSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSkills_Skills_SkillId",
                table: "ProfileSkills");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileSkills",
                table: "ProfileSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experiences",
                table: "Experiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameTable(
                name: "ProfileSkills",
                newName: "ProfileSkill");

            migrationBuilder.RenameTable(
                name: "Experiences",
                newName: "Experience");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileSkills_SkillId",
                table: "ProfileSkill",
                newName: "IX_ProfileSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_ProfileId",
                table: "Experience",
                newName: "IX_Experience_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_ProfileId",
                table: "Education",
                newName: "IX_Education_ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "UrlProfile",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UrlBackground",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

                            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileSkill",
                table: "ProfileSkill",
                columns: new[] { "ProfileId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experience",
                table: "Experience",
                column: "ExperienceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Profiles_ProfileId",
                table: "Education",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Profiles_ProfileId",
                table: "Experience",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSkill_Profiles_ProfileId",
                table: "ProfileSkill",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSkill_Skill_SkillId",
                table: "ProfileSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
