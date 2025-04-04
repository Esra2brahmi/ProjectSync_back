using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSync_back.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Projects");
        }
    }
}
