using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEmployeeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBenefitsAndEmployeeBenefits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeBenefits");

            migrationBuilder.CreateTable(
                name: "Benefit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    BaseCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    BenefitId = table.Column<int>(type: "INTEGER", nullable: false),
                    CostToEmployee = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefit_Benefit_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Benefit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefit_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefit_BenefitId",
                table: "EmployeeBenefit",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefit_EmployeeId",
                table: "EmployeeBenefit",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeBenefit");

            migrationBuilder.DropTable(
                name: "Benefit");

            migrationBuilder.CreateTable(
                name: "EmployeeBenefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    BenefitType = table.Column<int>(type: "INTEGER", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefits_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefits_EmployeeId",
                table: "EmployeeBenefits",
                column: "EmployeeId");
        }
    }
}
