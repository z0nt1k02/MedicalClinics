using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalClinics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecordOnClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CabinetName",
                table: "RecordOnClinics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClinicName",
                table: "RecordOnClinics",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CabinetName",
                table: "RecordOnClinics");

            migrationBuilder.DropColumn(
                name: "ClinicName",
                table: "RecordOnClinics");
        }
    }
}
