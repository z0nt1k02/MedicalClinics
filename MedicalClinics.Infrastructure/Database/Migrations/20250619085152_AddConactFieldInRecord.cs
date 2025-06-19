using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalClinics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConactFieldInRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreeRecord");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "RecordOnClinics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FreeRecordEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CabinetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeRecordEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeRecordEntity_Cabinets_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreeRecordEntity_CabinetId",
                table: "FreeRecordEntity",
                column: "CabinetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreeRecordEntity");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "RecordOnClinics");

            migrationBuilder.CreateTable(
                name: "FreeRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CabinetId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeRecord_Cabinets_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreeRecord_CabinetId",
                table: "FreeRecord",
                column: "CabinetId");
        }
    }
}
