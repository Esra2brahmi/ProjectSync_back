using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSync_back.Migrations
{
    /// <inheritdoc />
    public partial class AcademicTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicTitle",
                table: "Supervisors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicTitle",
                table: "Supervisors");
        }
    }
}
