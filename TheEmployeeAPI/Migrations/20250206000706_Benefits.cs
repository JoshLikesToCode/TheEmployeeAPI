using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEmployeeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Benefits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBenefit_Benefit_BenefitId",
                table: "EmployeeBenefit");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBenefit_Employees_EmployeeId",
                table: "EmployeeBenefit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeBenefit",
                table: "EmployeeBenefit");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeBenefit_EmployeeId",
                table: "EmployeeBenefit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Benefit",
                table: "Benefit");

            migrationBuilder.RenameTable(
                name: "EmployeeBenefit",
                newName: "EmployeeBenefits");

            migrationBuilder.RenameTable(
                name: "Benefit",
                newName: "Benefits");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeBenefit_BenefitId",
                table: "EmployeeBenefits",
                newName: "IX_EmployeeBenefits_BenefitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeBenefits",
                table: "EmployeeBenefits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Benefits",
                table: "Benefits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefits_EmployeeId_BenefitId",
                table: "EmployeeBenefits",
                columns: new[] { "EmployeeId", "BenefitId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBenefits_Benefits_BenefitId",
                table: "EmployeeBenefits",
                column: "BenefitId",
                principalTable: "Benefits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBenefits_Employees_EmployeeId",
                table: "EmployeeBenefits",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBenefits_Benefits_BenefitId",
                table: "EmployeeBenefits");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBenefits_Employees_EmployeeId",
                table: "EmployeeBenefits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeBenefits",
                table: "EmployeeBenefits");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeBenefits_EmployeeId_BenefitId",
                table: "EmployeeBenefits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Benefits",
                table: "Benefits");

            migrationBuilder.RenameTable(
                name: "EmployeeBenefits",
                newName: "EmployeeBenefit");

            migrationBuilder.RenameTable(
                name: "Benefits",
                newName: "Benefit");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeBenefits_BenefitId",
                table: "EmployeeBenefit",
                newName: "IX_EmployeeBenefit_BenefitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeBenefit",
                table: "EmployeeBenefit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Benefit",
                table: "Benefit",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefit_EmployeeId",
                table: "EmployeeBenefit",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBenefit_Benefit_BenefitId",
                table: "EmployeeBenefit",
                column: "BenefitId",
                principalTable: "Benefit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBenefit_Employees_EmployeeId",
                table: "EmployeeBenefit",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
