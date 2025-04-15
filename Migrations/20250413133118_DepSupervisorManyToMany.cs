using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectSync_back.Migrations
{
    /// <inheritdoc />
    public partial class DepSupervisorManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepSupervisors",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    SupervisorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepSupervisors", x => new { x.DepartmentId, x.SupervisorId });
                    table.ForeignKey(
                        name: "FK_DepSupervisors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepSupervisors_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepSupervisors_SupervisorId",
                table: "DepSupervisors",
                column: "SupervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepSupervisors");
        }
    }
}
