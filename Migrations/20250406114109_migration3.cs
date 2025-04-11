using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSync_back.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Class",
                table: "Users",
                newName: "Classe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Classe",
                table: "Users",
                newName: "Class");
        }
    }
}
