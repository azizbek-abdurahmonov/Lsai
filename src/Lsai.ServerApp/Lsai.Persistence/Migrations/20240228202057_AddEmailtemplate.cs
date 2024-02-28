using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lsai.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailtemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_resetPasswordVerificationCodes",
                table: "resetPasswordVerificationCodes");

            migrationBuilder.RenameTable(
                name: "resetPasswordVerificationCodes",
                newName: "ResetPasswordVerificationCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswordVerificationCodes",
                table: "ResetPasswordVerificationCodes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_Type",
                table: "EmailTemplates",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswordVerificationCodes",
                table: "ResetPasswordVerificationCodes");

            migrationBuilder.RenameTable(
                name: "ResetPasswordVerificationCodes",
                newName: "resetPasswordVerificationCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_resetPasswordVerificationCodes",
                table: "resetPasswordVerificationCodes",
                column: "Id");
        }
    }
}
